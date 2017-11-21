using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Client : MonoBehaviour {
    private const int MAX_CONNECTIONS = 10;
    private int port = 25000;
    private int hostId;
    private int reliableChannel;
    private int unreliableChannel;

    private int connectedId;

    private bool isConnected = false;
    private bool isStarted = false;
    private float connectionTime;
    private byte error;

    private string playerName;

    // Use this for initialization
    void Start () {
		
	}
	public void Connect()
    {
        string name = GameObject.Find("NameInput").GetComponent<InputField>().text;
        if (name == "")
        {
            Debug.Log("Type in a name, plz");
            return;
        }
        playerName = name;



        NetworkTransport.Init();
        ConnectionConfig cc = new ConnectionConfig();

        reliableChannel = cc.AddChannel(QosType.Reliable);
        unreliableChannel = cc.AddChannel(QosType.Unreliable);

        HostTopology topo = new HostTopology(cc, MAX_CONNECTIONS);
        hostId = NetworkTransport.AddHost(topo, port, null);
        connectedId = NetworkTransport.Connect(hostId, "LOCALHOST", port, 0, out error);

        connectionTime = Time.time;
        isConnected = true;
    }
	
    private void Update()
    {
        if (!isConnected)
            return;


        int recHostId;
        int connectionId;
        int channelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
        switch (recData)
        {
            case NetworkEventType.DataEvent:       //3
                break;
            case NetworkEventType.DisconnectEvent: //4
                break;
        }

    }


}
