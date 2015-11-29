using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

public class Server : MonoBehaviour
{
	public int LISTENING_PORT = 15000;
	public int BROADCAST_PORT = 16000;

	AsyncUDP udp;

	void Start ()
	{
		udp = new AsyncUDP (LISTENING_PORT, BROADCAST_PORT);
		udp.Start ();
	}

	void FixedUpdate ()
	{
		string msg = GlobalState.GetMessage ();
		if (msg != null)
			udp.Send (msg);
	}
}

class AsyncUDP
{
	Thread thread = null;
	int LISTENING_PORT;
	int BROADCAST_PORT;
	readonly UdpClient udp;

	public AsyncUDP (int LISTENING_PORT, int BROADCAST_PORT)
	{
		this.LISTENING_PORT = LISTENING_PORT;
		this.BROADCAST_PORT = BROADCAST_PORT;
		udp = new UdpClient (LISTENING_PORT);
	}

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

	private void StartListening ()
	{
		udp.BeginReceive (Receive, new object ());
	}

	private void Receive (IAsyncResult asyncResult)
	{
		IPEndPoint ip = new IPEndPoint (IPAddress.Any, LISTENING_PORT);
		byte[] bytes = udp.EndReceive (asyncResult, ref ip);
		string message = Encoding.ASCII.GetString (bytes);
		Messager.receiveMessage (message);
		StartListening ();
	}

	public void Send (string message)
	{
		byte[] bytes = Encoding.ASCII.GetBytes (message);
		udp.Send (bytes, bytes.Length, "255.255.255.255", BROADCAST_PORT);
	}
}


