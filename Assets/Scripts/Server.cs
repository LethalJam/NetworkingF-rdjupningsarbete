﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour {

    private const int MAX_CONNECTIONS = 10;
    private int port = 25000;
    private int hostId;
    private int reliableChannel;
    private int unreliableChannel;

    private bool isStarted = false;
    private byte error;



    // Use this for initialization
    void Start () {
        NetworkTransport.Init();
        ConnectionConfig cc = new ConnectionConfig();

        reliableChannel = cc.AddChannel(QosType.Reliable);
        unreliableChannel = cc.AddChannel(QosType.Unreliable);

        HostTopology topo = new HostTopology(cc, MAX_CONNECTIONS);
        hostId = NetworkTransport.AddHost(topo, port, null);

        isStarted = true;
	}
	
	private void Update()
    {
        if (!isStarted)
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
            case NetworkEventType.Nothing:         //1
                break;
            case NetworkEventType.ConnectEvent:    //2
                break;
            case NetworkEventType.DataEvent:       //3
                break;
            case NetworkEventType.DisconnectEvent: //4
                break;
        }

    }


}
