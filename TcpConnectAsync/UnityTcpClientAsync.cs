using UnityEngine;
using TcpAsync;

public abstract class UnityTcpClientAsync : MonoBehaviour
{
    protected TcpClientAsync tcpClient = null;
    private bool run = false;

    #region Unity
    void Awake()
    {
        
    }

    protected void Init()
    {
        tcpClient = new TcpClientAsync();
    }


    void Update()
    {
        if (run)
        {
            if (tcpClient.receiveStatus)
            {
                tcpClient.receiveStatus = false;
                OnReceive(tcpClient.getData());
                tcpClient.Receive();
            }
            if (tcpClient.sendStatus)
            {
                tcpClient.sendStatus = false;
                OnSend(tcpClient.getSend());
            }
        }
        else if (!run && tcpClient.connectStatus)
        {
            run = true;
            tcpClient.Receive();
            OnConnect();
        }
    }
    #endregion

    #region TcpAsync
    public void Connect(string ip, int port)
    {
        tcpClient.Connect(ip, port);
    }

    public void Send(string data)
    {
        tcpClient.Send(data);
    }

    public void Disconnect()
    {
        tcpClient.Disconnect();
    }

    // !! Theses three functions must be implement !!

    // This function is automatically call after a Connect
    abstract public void OnConnect(params object[] p);

    // This function is automatically call after a Receive
    abstract public void OnReceive(params object[] p);

    // This function is automatically call after a Send
    abstract public void OnSend(params object[] p);

    #endregion
}
