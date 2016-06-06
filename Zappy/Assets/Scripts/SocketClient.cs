using UnityEngine;
using System.Collections;
using System.Net;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class SocketClient {

	// ManualResetEvent instances signal completion.
	private ManualResetEvent connectDone = new ManualResetEvent(false);
	private ManualResetEvent sendDone = new ManualResetEvent(false);
	private ManualResetEvent receiveDone = new ManualResetEvent(false);

	// The response from the remote device.
	private String response = String.Empty;

	// Socket attributes
	private IPEndPoint remoteEP;
	private Socket client;

	// State object for receiving data from remote device.
	public class StateObject
	{
		// Client socket.
		public Socket workSocket = null;
		// Size of receive buffer.
		public const int BufferSize = 256;
		// Receive buffer.
		public byte[] buffer = new byte[BufferSize];
		// Received data string.
		public StringBuilder sb = new StringBuilder();
	}

	// CTOR
	public SocketClient(string ip, int port, AddressFamily family = AddressFamily.InterNetwork, SocketType type = SocketType.Stream, ProtocolType protocol = ProtocolType.Tcp)
	{
		remoteEP = new IPEndPoint(IPAddress.Parse("10.10.253.253"), 6667);

		// Create a TCP/IP socket.
		client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	}

	public void Disconnect()
	{
		if (client.Connected)
		{
			// Release the socket.
			client.Shutdown (SocketShutdown.Both);
			client.Close ();
		}
	}

	void ConnectCallback(IAsyncResult ar)
	{
		try
		{
			// Retrieve the socket from the state object.
			Socket client = (Socket) ar.AsyncState;

			// Complete the connection.
			client.EndConnect(ar);

			Debug.Log("Socket connected to " + client.RemoteEndPoint.ToString());

			// Signal that the connection has been made.
			connectDone.Set();
		}
		catch (Exception e)
		{
			Debug.LogWarning("Connection callback error: " + e.ToString());
		}
	}

	public void Connect(int milliseconds = 1000)
	{
		try
		{
			// Connect to the remote endpoint.
			client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
			connectDone.WaitOne(milliseconds);
			connectDone.Reset();
		}
		catch (Exception e)
		{
			Debug.LogWarning ("Socket connection failed. Reason: " + e.Message);
		}
	}

	// Send test data to the remote device.
	public void Send(string msg, int waitMilliseconds = -1)
	{
		Send (client, msg);
		sendDone.WaitOne (waitMilliseconds);
		sendDone.Reset ();
	}

	// Asynchronous sending
	private void Send(Socket client, String data)
	{
		try
		{
			// Convert the string data to byte data using ASCII encoding.
			byte[] byteData = Encoding.ASCII.GetBytes(data);

			// Begin sending the data to the remote device.
			client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
		}
		catch (Exception e)
		{
			Debug.LogWarning ("Send error: " + e.Message);
		}
	}

	private void SendCallback(IAsyncResult ar)
	{
		try
		{
			// Retrieve the socket from the state object.
			Socket client = (Socket) ar.AsyncState;

			// Complete sending the data to the remote device.
			/*int bytesSent = */client.EndSend(ar);

			// Signal that all bytes have been sent.
			sendDone.Set();
		}
		catch (Exception e)
		{
			Debug.LogWarning(e.ToString());
		}
	}

	// If socket is available for reading
	public bool Select()
	{
		if (client.Connected && client.Poll (0, SelectMode.SelectRead))
		{
			return true;
		}
		return false;
	}

	public string Receive(int milliseconds = -1)
	{
		response = "";
		Receive(client);
		receiveDone.WaitOne (milliseconds);
		receiveDone.Reset ();
		return response;
	}

	private void Receive(Socket client)
	{
		try
		{
			// Create the state object.
			StateObject state = new StateObject();
			state.workSocket = client;

			// Begin receiving the data from the remote device.
			client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
		}
		catch (Exception e)
		{
			Console.WriteLine("Receive error: " + e.ToString());
		}
	}

	private void ReceiveCallback(IAsyncResult ar)
	{
		try
		{
			// Retrieve the state object and the client socket 
			// from the asynchronous state object.
			StateObject state = (StateObject) ar.AsyncState;
			Socket client = state.workSocket;

			// Read data from the remote device.
			int bytesRead = client.EndReceive(ar);

			if (bytesRead > 0)
			{
				// There might be more data, so store the data received so far.
				state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

				// Get the rest of the data.
				if (bytesRead == 256)
					client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
				else
				{
					response = state.sb.ToString();
					receiveDone.Set();
				}
			}
			else
			{
				// All the data has arrived; put it in response.
				if (state.sb.Length > 1)
				{
					response = state.sb.ToString();
				}
				// Signal that all bytes have been received.
				receiveDone.Set();
			}
		}
		catch (Exception e)
		{
			Debug.LogWarning("Receive callback error: " + e.ToString());
		}
	}

	public bool IsConnected()
	{
		return client.Connected;
	}
}
