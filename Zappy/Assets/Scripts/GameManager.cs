using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.IO;
using System.Diagnostics;

// TODO: coller les oeufs sur la map, éclosion etc
// TODO: déplacements ? anim de ramassage ?
public class GameManager : UnityTcpClientAsync {

    private SendCommands sendCommands;
    private ReceiveCommands receiveCommands;
    public GameObject playerPrefab;
    public GameObject egg;
    public static GameManager instance = null;

    [SerializeField] private InfinitTerrain terrain;
    [Header("UI")]
    [SerializeField] private int maxLinesConsole;
    [SerializeField] private DisplayCharac displayCharac;
    [SerializeField] private Slider timeButton;
    [SerializeField] private GameObject panelConnection;
	[SerializeField] private GameObject panelGame;
	[SerializeField] private GameObject panelVictory;
	[SerializeField] private GameObject panelConsole;
	[SerializeField] private Text textIp;
	[SerializeField] private Text textPort;
	[SerializeField] private Text textConsoleOutput;
	[SerializeField] private Scrollbar scrollbarConsole;
    private Dictionary<string, List<Character>> teams = new Dictionary<string, List<Character>>();
    private List<Egg> eggs = new List<Egg>();
    private int timeScale;
    private bool pause = false;
    private bool console = false;
    public enum CMD { msz, bct, tna, pnw, ppo, plv, pin, sgt };
    private static readonly string[] cmdNames = { "msz", "bct", "tna", "pnw", "ppo", "plv", "pin", "sgt" };
    int consoleLines = 1;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        Init();
    }

    public bool Pause
    {
        get
        {
            return pause;
        }
        set
        {
            pause = value;
        }
    }


    public int TimeScale
    {
        get
        {
            return timeScale;
        }
        set
        {
            timeScale = value;
            timeButton.value = timeScale;
        }
    }

    public Dictionary<string, List<Character>> getTeam()
    {
        return teams;
    }

    void Start()
	{
        panelConnection.SetActive (true);
		panelGame.SetActive (false);
		panelVictory.SetActive (false);
        sendCommands = GetComponent<SendCommands>();
        receiveCommands = GetComponent<ReceiveCommands>();
    }


    public void ConnectToServer()
	{
		if (textIp.text == "" || textPort.text == "")
        {
            textIp.transform.parent.GetComponent<InputField>().text = "";
            textPort.transform.parent.GetComponent<InputField>().text = "";
            return;
        }
        Connect(textIp.text, Convert.ToInt32(textPort.text));
    }

	public void DisconnectFromServer()
	{
        eggs.Clear();
        teams.Clear();
        displayCharac.Reset();
        Disconnect();
        if (terrain.status)
        {
            terrain.DeleteOnGame();
        }
        panelGame.SetActive(false);
		panelVictory.SetActive (false);
        panelConnection.SetActive(true);
    }

    public void StartGame()
    {
        panelConnection.SetActive(false);
        Send(sendCommands.CallCommand("time", null).ToString());
        panelGame.transform.parent.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        panelGame.SetActive(true);
    }

	public void ShowConsole()
	{
		panelConsole.SetActive (!panelConsole.activeSelf);
	}

    public InfinitTerrain getMap()
    {
        return GameObject.FindGameObjectWithTag("Terrain").GetComponent<InfinitTerrain>();
    }

    public void addTeam(string name)
    {
        teams.Add(name, new List<Character>());
    }

    public GameObject addPlayer(int id, Vector3 pos, int orientation, int level, string team)
    {
        GameObject player = (Instantiate(playerPrefab, new Vector3(pos.x, 0.1f, pos.z), Quaternion.identity) as GameObject);
        Character charac = player.GetComponent<Character>();
        charac.Init(id, pos.x, pos.z, orientation, level, team);
        teams[team].Add(charac);
        displayCharac.addCharacters(charac);
        return player;
    }

    public void removeCharacter(Character charac)
    {
        teams[charac._team].Remove(charac);
        displayCharac.removeCharacters(charac);
    }

	public void addEgg(int id_egg, Character character, int _x, int _y)
    {
        GameObject tmp = (Instantiate(egg, character._pos, egg.transform.rotation) as GameObject);
        tmp.GetComponent<Egg>().Init(id_egg, character);
        eggs.Add(tmp.GetComponent<Egg>());
		tmp.transform.SetParent(getMap ().getMapPos (_x, _y).transform);
    }

    public Character getCharacter(int id)
    {
        foreach (KeyValuePair<string, List<Character>> team in teams)
        {
            foreach (Character player in team.Value)
            {
                if (player._id == id)
                    return player;
            }
        }
        return null;
    }

    public Egg getEgg(int id)
    {
        foreach (Egg eg in eggs)
        {
            if (eg._id == id)
                return eg;
        }
        return null;
    }

    public void SetTimer(string time)
    {
        panelGame.transform.parent.GetComponent<CanvasManager>().SetTimer(time);
    }

    void checkConsole()
    {
        if (consoleLines == maxLinesConsole * (QualitySettings.GetQualityLevel() + 1))
        {
            textConsoleOutput.text = textConsoleOutput.text.Remove(0, textConsoleOutput.text.IndexOf('\n') + 1);
            --consoleLines;
        }
    }

    void updateConsole()
    {
        // Forces the canvas to update to scroll
        Canvas.ForceUpdateCanvases();
        scrollbarConsole.value = 0f;
        Canvas.ForceUpdateCanvases();
    }

    public void WriteConsole(string msg)
    {
        if (console && msg != "")
        {
            ++consoleLines;
            textConsoleOutput.text += "> " + msg + "\n";
            checkConsole();
            updateConsole();
        }
    }

    public void createNewPlayer(string team_name, string lua_path)
    {
        string filename = Application.dataPath + "/IA/Bin/zappy_ai";
        if (Application.platform == RuntimePlatform.WindowsPlayer)
            filename += ".exe";
        if (!File.Exists(filename))
            return;
        Process myProcess = new Process();
        myProcess.StartInfo.FileName = filename;
        myProcess.StartInfo.Arguments = "-n " + team_name + " -p " + Convert.ToString(tcpClient.Port()) + " -h " + tcpClient.IP() + " -s " + lua_path;
        myProcess.Start();
    }

	public void DisplayVictoryScreen(string team)
	{
		panelVictory.SetActive (true);
		panelVictory.transform.Find ("LabelText").GetComponent<Text> ().text = "Team " + team + " wins !";
	}

    public void TooglePause()
    {
        pause = !pause;
    }

    public void ToogleConsole()
    {
        console = !console;
    }

    #region Abstact TcpAsync
    // Call on Main thread after connection
    public override void OnConnect(params object[] p)
    {
        if (!tcpClient.getSocket().Connected)
        {
            textIp.text = "";
            textPort.text = "";
        }
        foreach (object obj in p)
        {
            textConsoleOutput.text += "Connected to host: " + obj.ToString() + "\n";
            ++consoleLines;
        }
        
        //sockClient.Send ("GRAPHIC\n", 0);
        Send("GRAPHIC\n");

        panelGame.SetActive(true);
        panelConnection.SetActive(false);
    }

    // Call on Main thread after receive
    public override void OnReceive(params object[] p)
    {
        foreach (object obj in p)
        {
            receiveCommands.PushQueue(obj.ToString());
        }
    }

    // Call on Main thread after send
    public override void OnSend(params object[] p)
    {
        foreach (object obj in p)
        {
            textConsoleOutput.text += "<color=lightblue>< " + obj.ToString() + "</color>";
            ++consoleLines;
            checkConsole();
            updateConsole();
        }
    }

    public void SendServer(CMD cmd, params object[] p)
    {
        string res = sendCommands.CallCommand(cmdNames[Convert.ToInt32(cmd)], p).ToString();
        Send(res);
    }

    public override void OnError(params object[] p)
    {
        textIp.transform.parent.GetComponent<InputField>().text = "";
        textPort.transform.parent.GetComponent<InputField>().text = "";
        tcpClient.Disconnect();
    }

    public override void OnDisconnect(params object[] p)
    {
        DisconnectFromServer();
    }
    #endregion
}
