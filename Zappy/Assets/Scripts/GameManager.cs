using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	[Header("UI")]
	[SerializeField] private GameObject panelConnection;
	[SerializeField] private GameObject panelGame;
	[SerializeField] private Text textIp;
	[SerializeField] private Text textPort;
	[SerializeField] private Text textConsoleOutput;

	// Socket client connected to server
	private SocketClient sockClient = null;

	void Start()
	{
		panelGame.SetActive (false);
		panelConnection.SetActive (true);
	}

	void Update()
	{
		if (sockClient != null && sockClient.Select())
		{
			textConsoleOutput.text += sockClient.Receive ();
		}
	}

	public void ConnectToServer()
	{
		if (textIp.text == "" || textPort.text == "")
			return;

		sockClient = new SocketClient(textIp.text, Convert.ToInt32(textPort));
		sockClient.Connect ();

		if (!sockClient.IsConnected())
		{
			Debug.LogWarning ("Failed to connect to host");
		}
		else
		{
			sockClient.Send ("NICK tamer\nUSER toto toto toto toto\n", 500);
			panelConnection.SetActive (false);
			panelGame.SetActive (true);
		}
	}
}
