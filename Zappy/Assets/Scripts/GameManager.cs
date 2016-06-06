using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private SocketClient sockClient;

	// Use this for initialization
	void Start ()
	{
		// Starts connection thread
		sockClient = new SocketClient ("10.10.253.253", 6667);
		sockClient.Connect ();
		sockClient.Send ("NICK tamer\nUSER toto toto toto toto\n");
		//sockClient.Disconnect ();
	}

	void Update()
	{
		if (sockClient.Select())
		{
			Debug.Log("Received = " + sockClient.Receive (-1));
			//sockClient.Send ("JOIN #toto\nPRIVMSG #toto penis\nusers\n");
		}
	}
}
