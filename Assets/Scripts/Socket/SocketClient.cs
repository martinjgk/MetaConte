using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class PersistentSocketClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private Thread clientThread;
    private bool running = true;

	private InputSignLang inputSignLang;

    void Start()
    {
		inputSignLang = GetComponent<InputSignLang>();
        client = new TcpClient("localhost", 1398);
        stream = client.GetStream();
        clientThread = new Thread(new ThreadStart(ReceiveData));
        clientThread.Start();
    }

    void ReceiveData()
    {
        try
        {
            byte[] receivedBytes = new byte[1024];
            while (running)
            {
                if (stream.DataAvailable)
                {
                    int bytesRead = stream.Read(receivedBytes, 0, receivedBytes.Length);
                    string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes, 0, bytesRead);
                    Debug.Log("Received from server: " + receivedMessage);
					inputSignLang.inputSign = receivedMessage;
                    if(receivedMessage == "water"){
                        //monster.callMonster();
                    }
                    else if(receivedMessage == "down"){
                        //monster.callMonster();
                    }
                }
                //Thread.Sleep(100);  // Slight delay to reduce CPU usage
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Socket error: " + e.Message);
        }
    }

    void OnApplicationQuit()
    {
        running = false;  // Stop the thread loop
        stream.Close();
        client.Close();
    }

    void OnDestroy()
    {
        running = false;  // Ensure the thread is stopped when the object is destroyed
        stream.Close();
        client.Close();
    }
}
