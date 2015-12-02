using UnityEngine;
using System.Collections;

public class GlobalState : MonoBehaviour
{
	
	public GameObject playerPrefab;
	public GameObject homeLeftPost;
	public GameObject topWall;

	public Sprite homeKit;
	public Sprite awayKit;
	
	public static string constants;
	public static bool gameStarted;
	public static bool goalScored;
	public static int homeScore;
	public static int awayScore;
	public static ArrayList homePlayers = ArrayList.Synchronized (new ArrayList ());
	public static ArrayList awayPlayers = ArrayList.Synchronized (new ArrayList ());
	public static float time;
	public static Stack newPlayers = Stack.Synchronized (new Stack ()); 
	
	public static string GetMessage ()
	{
		if (gameStarted) {
			if (goalScored) {
				goalScored = false;
				return GoalState ();
			}
			if (Input.GetKeyDown ("r")) {
				ResetAll ();
				gameStarted = false;
				goalScored = false;
				return null;
			} else
				return GameState ();
		} else if (Input.GetKeyDown ("p")) {
			// start the game now
			gameStarted = true;
			return PreKickoffState ();
		} else {
			return null;
		}
	}

	public static void ResetAll ()
	{
		CircleCollider2D ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<CircleCollider2D> ();
		ball.gameObject.transform.position = new Vector2 (0, 0);
		ball.attachedRigidbody.velocity = new Vector2 (0, 0);
		
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			player.GetComponent<PlayerMovement> ().reset ();
		}
		homeScore = 0;
		awayScore = 0;
	}
	
	public static string GameState ()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();

		sb.Append ("p;");
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

	public static string GoalState ()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		
		sb.Append ("g;");
		//result x-y
		sb.Append (getScore ());
		sb.Append (GameState ().Substring (1));

		return sb.ToString ();
	}
	
	public static string PreKickoffState ()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		//result
		sb.Append ("k;");

		sb.Append (GlobalState.constants);

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

	public void initConstants ()
	{
		GameObject ball = GameObject.FindGameObjectWithTag ("Ball");
		GameObject kicker = playerPrefab.transform.Find ("Kicker").gameObject;

		float xMax = Mathf.Abs (homeLeftPost.transform.position.x);
		float yMax = Mathf.Abs (topWall.GetComponent<BoxCollider2D> ().bounds.min.y);
		float goalPostY = Mathf.Abs (homeLeftPost.transform.position.y);
		float goalPostRadius = homeLeftPost.GetComponent<CircleCollider2D> ().radius * homeLeftPost.transform.localScale.x;
		float ballRadius = ball.GetComponent<CircleCollider2D> ().radius * ball.transform.localScale.x;
		float playerRadius = playerPrefab.GetComponent<CircleCollider2D> ().radius * playerPrefab.transform.localScale.x;
		float kickerRadius = kicker.GetComponent<CircleCollider2D> ().radius * kicker.transform.localScale.x * playerPrefab.transform.localScale.x;

		constants =
			xMax.ToString ("N6") + ","
			+ yMax.ToString ("N6") + ","
			+ goalPostY.ToString ("N6") + ","
			+ goalPostRadius.ToString ("N6") + ","
			+ ballRadius.ToString ("N6") + ","
			+ playerRadius.ToString ("N6") + ","
			+ kickerRadius.ToString ("N6");
		Debug.Log (constants);
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
		initConstants ();
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

	public static void TeamScored (Player.Team team)
	{
		if (team == Player.Team.Away) {
			awayScore++;
		} else {
			homeScore++;
		}
		goalScored = true;
	}
}
