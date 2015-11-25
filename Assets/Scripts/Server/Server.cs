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
	public GameObject playerPrefab;

	AsyncUDP udp;

	void Start ()
	{
		udp = new AsyncUDP (LISTENING_PORT, BROADCAST_PORT);
		udp.Start ();
	}

	void Update ()
	{
		if (Input.GetKey ("s")) {
			string msg = Messager.getState();
			udp.Send (msg);
		} else if (Input.GetKey ("x")) {
			udp.Stop ();
		}
	}

	void FixedUpdate(){
		if (GlobalState.newPlayers.Count>0){
			GlobalState.PlayerStruct newPlayer = (GlobalState.PlayerStruct) GlobalState.newPlayers.Pop ();
			GameObject player;
			player = Instantiate (playerPrefab);
			player.GetComponent<Player> ().team = newPlayer.team;
			player.GetComponent<Player> ().username = newPlayer.name;
		}
	}
}

class AsyncUDP
{			
	Thread thread = null;
	int LISTENING_PORT;
	int BROADCAST_PORT;

	public AsyncUDP(int LISTENING_PORT, int BROADCAST_PORT){
		this.LISTENING_PORT = LISTENING_PORT;
		this.BROADCAST_PORT = BROADCAST_PORT;
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
			
	private readonly UdpClient udp = new UdpClient (LISTENING_PORT);
			
	private void StartListening ()
	{
		udp.BeginReceive (Receive, new object ());
	}
	private void Receive (IAsyncResult asyncResult)
	{
		IPEndPoint ip = new IPEndPoint (IPAddress.Any, Server.LISTENING_PORT);
		byte[] bytes = udp.EndReceive (asyncResult, ref ip);
		string message = Encoding.ASCII.GetString (bytes);
		Messager.receiveMessage (message);
		Debug.Log (String.Format ("From {0} received: {1} ", ip.Address.ToString (), message));
		StartListening ();
	}
	public void Send (string message)
	{
		byte[] bytes = Encoding.ASCII.GetBytes (message);
		int k = udp.Send (bytes, bytes.Length, "255.255.255.255", Server.BROADCAST_PORT);
		Debug.Log (String.Format ("Sent: {0}, {1} ", message, k));
	}
}


