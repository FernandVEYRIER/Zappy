using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TcpAsync;

public class GameManager : UnityTcpClientAsync {

    
    [SerializeField] private SendCommands commands;
    public GameObject playerPrefab;
    public GameObject egg;
    [Header("UI")]
    [SerializeField] private Slider timeButton;
    [SerializeField] private GameObject panelConnection;
	[SerializeField] private GameObject panelGame;
	[SerializeField] private GameObject panelConsole;
	[SerializeField] private Text textIp;
	[SerializeField] private Text textPort;
	[SerializeField] private Text textConsoleOutput;
    private Dictionary<string, List<Character>> teams = new Dictionary<string, List<Character>>();
    private List<Egg> eggs = new List<Egg>();
    private int timeScale;

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

	void Start()
	{
		panelConnection.SetActive (true);
		panelGame.SetActive (false);
    }

    public void ConnectToServer()
	{
		if (textIp.text == "" || textPort.text == "")
			return;
        Connect(textIp.text, Convert.ToInt32(textPort.text));
    }

	public void DisconnectFromServer()
	{
        Disconnect();
        panelGame.SetActive(false);
        panelConnection.SetActive(true);
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

    public void addPlayer(int id, Vector3 pos, int orientation, int level, string team)
    {
        GameObject player = (Instantiate(playerPrefab, new Vector3(pos.x, 1, pos.z), Quaternion.identity) as GameObject);
        player.GetComponent<Character>().Init(id, pos.x, pos.z, orientation, level, team);
        teams[team].Add(player.GetComponent<Character>());
    }

    public void addEgg(int id_egg, Character character)
    {
        GameObject tmp = (Instantiate(egg, character.Pos , Quaternion.identity) as GameObject);
        tmp.GetComponent<Egg>().Init(id_egg, character);
        eggs.Add(tmp.GetComponent<Egg>());
    }

    public Character getCharacter(int id)
    {
        foreach (KeyValuePair<string, List<Character>> team in teams)
        {
            foreach (Character player in team.Value)
            {
                if (player.ID == id)
                    return player;
            }
        }
        return null;
    }

    public Egg getEgg(int id)
    {
        foreach (Egg eg in eggs)
        {
            if (eg.ID == id)
                return eg;
        }
        return null;
    }

    #region Abstact TcpAsync
    // Call on Main thread after connect
    public override void OnConnect(params object[] p)
    {
        foreach (object obj in p)
        {
            textConsoleOutput.text += "Connected to host: " + obj.ToString() + "\n";
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
            textConsoleOutput.text += "< " + obj.ToString();
            commands.CallCommand(obj.ToString());
        }
    }

    // Call on Main thread after send
    public override void OnSend(params object[] p)
    {
        foreach (object obj in p)
        {
            textConsoleOutput.text += "> " + obj.ToString();
        }
    }
    #endregion
}
