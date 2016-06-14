﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

    [Header("UI")]
    public SendCommands commands;
	[SerializeField] private GameObject panelConnection;
	[SerializeField] private GameObject panelGame;
	[SerializeField] private GameObject panelConsole;
	[SerializeField] private Text textIp;
	[SerializeField] private Text textPort;
	[SerializeField] private Text textConsoleOutput;

	// Socket client connected to server
	private SocketClientAsync sockClient = null;

	void Awake()
	{
		sockClient = GetComponent<SocketClientAsync> ();
	}

	void Start()
	{
		panelConnection.SetActive (true);
		panelGame.SetActive (false);
        commands.CallCommand("msz 10 10");
    }

	void Update()
	{
		if (sockClient != null && sockClient.Select())
		{
			sockClient.Receive ();
		}
	}

	public void ConnectToServer()
	{
		if (textIp.text == "" || textPort.text == "")
			return;

		//sockClient = new SocketClientAsync(textIp.text, Convert.ToInt32(textPort.text));

		// On connection we notify the server and tell the user where we connected
		sockClient.connectDelegates -= OnConnection;
		sockClient.connectDelegates += OnConnection;

		// Writes the received data to the console log
		sockClient.receiveDelegates -= OnReceive;
		sockClient.receiveDelegates += OnReceive;

		// Writes the data sent to the console log
		sockClient.sendDelegates -= OnSend;
		sockClient.sendDelegates += OnSend;

		// Handles disconnection
		sockClient.disconnectDelegates -= OnDisconnect;
		sockClient.disconnectDelegates += OnDisconnect;
        print(textPort.text);
		sockClient.Connect (textIp.text, Convert.ToInt32(textPort.text));
	}

	public void DisconnectFromServer()
	{
		sockClient.Disconnect ();
	}

	void OnConnection(params object[] p)
	{
		foreach (object obj in p)
		{
			textConsoleOutput.text += "Connected to host: " + obj.ToString() + "\n";
		}

		sockClient.Send ("GRAPHIC\n", 0);

		panelGame.SetActive (true);
		panelConnection.SetActive (false);
	}

	void OnReceive(params object[] p)
	{
		foreach (object obj in p)
		{
			textConsoleOutput.text += "< " + obj.ToString ();
            commands.CallCommand(obj.ToString());
		}
	}

	void OnSend(params object[] p)
	{
		foreach (object obj in p)
		{
			textConsoleOutput.text += "> " + obj.ToString ();
		}		
	}

	void OnDisconnect(params object[] p)
	{
		panelGame.SetActive (false);
		panelConnection.SetActive (true);
	}

	public void ShowConsole()
	{
		panelConsole.SetActive (!panelConsole.activeSelf);
	}

    public InfinitTerrain getMap()
    {
        return GameObject.FindGameObjectWithTag("Terrain").GetComponent<InfinitTerrain>();
    }
}
