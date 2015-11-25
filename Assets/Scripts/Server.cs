using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

public class Server : MonoBehaviour
{
	AsyncUDP udp;

	void Start ()
	{
		udp = new AsyncUDP ();
		udp.Start ();
	}

	void Update ()
	{
		if (Input.GetKey ("s")) {
			udp.Send ("123456789");
		} else if (Input.GetKey ("x")) {
			udp.Stop ();
		}
	}
}

class AsyncUDP
{
	const int LISTENING_PORT = 15000;
	const int BROADCAST_PORT = 16000;
			
	Thread thread = null;
	public void Start ()
	{
		if (thread != null) {
			throw new Exception ("Thread is already running");
		}
		StartListening ();
		Debug.Log ("Listening started");
	}
	public void Stop ()
	{
		try {
			udp.Close ();
			Debug.Log ("Listening stopped");
		} catch (Exception e) { 
			Debug.LogError (e.StackTrace);
		}
	}
			
	private readonly UdpClient udp = new UdpClient (LISTENING_PORT);
			
	private void StartListening ()
	{
		udp.BeginReceive (Receive, new object ());
	}
	private void Receive (IAsyncResult asyncResult)
	{
		IPEndPoint ip = new IPEndPoint (IPAddress.Any, LISTENING_PORT);
		byte[] bytes = udp.EndReceive (asyncResult, ref ip);
		string message = Encoding.ASCII.GetString (bytes);
		Debug.Log (String.Format ("From {0} received: {1} ", ip.Address.ToString (), message));
		StartListening ();
	}
	public void Send (string message)
	{
		byte[] bytes = Encoding.ASCII.GetBytes (message);
		int k = udp.Send (bytes, bytes.Length, "255.255.255.255", BROADCAST_PORT);
		Debug.Log (String.Format ("Sent: {0}, {1} ", message, k));
	}
}


