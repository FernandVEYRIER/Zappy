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
	private SocketClient sockClient = null;

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

	public void ConnectToServer()
	{
		if (textIp.text == "" || textPort.text == "")
			return;

		sockClient = new SocketClient(textIp.text, Convert.ToInt32(textPort.text));

		sockClient.connectDelegates += delegate(object[] p)
		{
			sockClient.Send ("GRAPHIC\n", 0);

			panelGameActive = true;
			panelConnectionActive = false;
		};

		sockClient.receiveDelegates += delegate(object[] p)
		{
			foreach (object obj in p)
			{
				textConsoleOutput.text += obj.ToString ();
			}
		};

		sockClient.Connect ();
	}
}
