using UnityEngine;
using System.Collections;

public class GlobalState : MonoBehaviour
{
	
	public GameObject playerPrefab;
	public Sprite homeKit;
	public Sprite awayKit;

	public static bool gameStarted;
	public static int homeScore;
	public static int awayScore;
	public static ArrayList homePlayers = ArrayList.Synchronized (new ArrayList ());
	public static ArrayList awayPlayers = ArrayList.Synchronized (new ArrayList ());
	public static float time;
	public static Stack newPlayers = Stack.Synchronized (new Stack ()); 
	
	public static string GetMessage ()
	{
		if (gameStarted) {
			return GameState ();
		} else if (Input.GetKeyDown ("p")) {
			// start the game now
			gameStarted = true;
			return PreKickoffState ();
		} else {
			return null;
		}
	}
	
	public static string GameState ()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();

		//ball x,y,velocityX,velocityY
		GameObject ball = GameObject.FindGameObjectWithTag ("Ball");
		sb.Append (GameObjectString (ball));
		
		//players team,id,username,x,y,velocityX,velocityY
		foreach (Player pl in awayPlayers) {
			sb.Append (";"
				+ pl.id + ',' 
				+ GameObjectString (pl.gameObject));
		}
		foreach (Player pl in homePlayers) {
			sb.Append (";"
				+ pl.id + ',' 
				+ GameObjectString (pl.gameObject));
		}
		return sb.ToString ();
	}
	
	public static string PreKickoffState ()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		//result
		sb.Append (GlobalState.getScore ());

		//players team,id,username
		foreach (Player pl in awayPlayers) {
			sb.Append (";"
				+ pl.team + ',' 
				+ pl.id + ',' 
				+ pl.username);
		}
		foreach (Player pl in homePlayers) {
			sb.Append (";"
				+ pl.team + ',' 
				+ pl.id + ',' 
				+ pl.username);
		}
		return sb.ToString ();
	}

	public static Player.Team GetTeam ()
	{
		if (homePlayers.Count > awayPlayers.Count) {
			return Player.Team.Away;
		} else {
			return Player.Team.Home;
		}
	}

	public static int GetPlayersCount ()
	{
		return homePlayers.Count + awayPlayers.Count;
	}

	public static string getScore ()
	{
		return homeScore.ToString () + ':' + awayScore.ToString ();
	}

	void Update ()
	{
		time = time + Time.deltaTime;
	}

	void Start ()
	{
		time = 0.0f;
	}

	void FixedUpdate ()
	{
		while (newPlayers.Count > 0) {
			string username = (string)newPlayers.Pop ();

			if (UsernameExists (username))
				continue;

			Player player;
			player = Instantiate (playerPrefab).GetComponent<Player> ();
			player.team = GlobalState.GetTeam ();

			if (player.team == Player.Team.Home) {
				homePlayers.Add (player.GetComponent<Player> ());
			} else {
				awayPlayers.Add (player.GetComponent<Player> ());
			}

			player.username = username;
			player.id = GlobalState.GetPlayersCount ();
			player.GetComponent<SpriteRenderer> ().sprite = (player.team == Player.Team.Home) ? homeKit : awayKit;

		}
	}

	bool UsernameExists (string name)
	{
		foreach (Player pl in awayPlayers) {
			if (pl.username == name)
				return true;
		}
		foreach (Player pl in homePlayers) {
			if (pl.username == name)
				return true;
		}
		return false;

	}
	
	static string GameObjectString (GameObject go)
	{
		return go.transform.position.x.ToString ("N6") + ',' 
			+ go.transform.position.y.ToString ("N6") + ',' 
			+ go.GetComponent<Rigidbody2D> ().velocity.x.ToString ("N6") + ',' 
			+ go.GetComponent<Rigidbody2D> ().velocity.y.ToString ("N6");
	}
}
