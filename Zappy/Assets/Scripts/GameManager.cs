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

	[Header("Panel State")]
	[SerializeField] private bool panelGameActive = false;
	[SerializeField] private bool panelConnectionActive = true;

	// Socket client connected to server
	private SocketClientAsync sockClient = null;

	void Awake()
	{
		sockClient = GetComponent<SocketClientAsync> ();
	}

	void Start()
	{
		RefreshPanels ();
	}

	void Update()
	{
		if (sockClient != null && sockClient.Select())
		{
			sockClient.Receive ();
		}
		RefreshPanels ();
	}

	void RefreshPanels()
	{
		panelConnection.SetActive(panelConnectionActive);
		panelGame.SetActive(panelGameActive);	
	}

	/*public void ConnectToServer()
	{
		if (textIp.text == "" || textPort.text == "")
			return;

		sockClient = new SocketClient(textIp.text, Convert.ToInt32(textPort.text));

		// On connection we notify the server and tell the user where we connected
		sockClient.connectDelegates += delegate(object[] p)
		{
			foreach (object obj in p)
			{
				//textIp.text += "Connected to host: " + obj.ToString();
			}

			sockClient.Send ("GRAPHIC\n", 0);

			panelGameActive = true;
			panelConnectionActive = false;
		};

		// Writes the received data to the console log
		sockClient.receiveDelegates += delegate(object[] p)
		{
			foreach (object obj in p)
			{
				//textConsoleOutput.text += "< " + obj.ToString ();
			}
		};

		// Writes the data sent to the console log
		sockClient.sendDelegates += delegate(object[] p)
		{
			foreach (object obj in p)
			{
				//textConsoleOutput.text += "> " + obj.ToString ();
			}
		};

		sockClient.Connect ();
	}*/

	public void ConnectToServer()
	{
		if (textIp.text == "" || textPort.text == "")
			return;

		//sockClient = new SocketClientAsync(textIp.text, Convert.ToInt32(textPort.text));

		// On connection we notify the server and tell the user where we connected
		sockClient.connectDelegates += delegate(object[] p)
		{
			foreach (object obj in p)
			{
				textConsoleOutput.text += "Connected to host: " + obj.ToString() + "\n";
			}

			sockClient.Send ("GRAPHIC\n", 0);

			panelGameActive = true;
			panelConnectionActive = false;
		};

		// Writes the received data to the console log
		sockClient.receiveDelegates += delegate(object[] p)
		{
			foreach (object obj in p)
			{
				textConsoleOutput.text += "< " + obj.ToString ();
			}
		};

		// Writes the data sent to the console log
		sockClient.sendDelegates += delegate(object[] p)
		{
			foreach (object obj in p)
			{
				textConsoleOutput.text += "> " + obj.ToString ();
			}
		};

		sockClient.Connect (textIp.text, Convert.ToInt32(textPort.text));
	}
}
