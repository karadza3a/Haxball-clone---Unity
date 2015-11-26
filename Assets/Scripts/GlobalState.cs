using UnityEngine;
using System.Collections;

public class GlobalState : MonoBehaviour
{
	
	public GameObject playerPrefab;
	public static string msg;

	public static int homeScore;
	public static int awayScore;

	public static ArrayList homePlayers = ArrayList.Synchronized (new ArrayList ());
	public static ArrayList awayPlayers = ArrayList.Synchronized (new ArrayList ());
	public static float time;
	public static Stack newPlayers = Stack.Synchronized (new Stack ()); 

	public static Player.Team getTeam ()
	{
		if (homePlayers.Count > awayPlayers.Count) {
			return Player.Team.Away;
		} else {
			return Player.Team.Home;
		}
	}

	public static int getPlayersCount ()
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

		msg = "";
		//result
		msg += GlobalState.getScore () + ';';
		
		//ball x,y,velocityX,velocityY
		GameObject ball = GameObject.FindGameObjectWithTag ("Ball");
		msg += ball.transform.position.x.ToString ("N6") + ',' 
			+ ball.transform.position.y.ToString ("N6") + ',' 
			+ ball.GetComponent<Rigidbody2D> ().velocity.x.ToString ("N6") + ',' 
			+ ball.GetComponent<Rigidbody2D> ().velocity.y.ToString ("N6");
		
		//players username,tim,x,y,velocityX,velocityY
		foreach (Player pl in awayPlayers) {
			msg += ';'
				+ pl.username + ',' 
				+ pl.team + ',' 
				+ pl.gameObject.transform.position.x.ToString ("N6") + ',' 
				+ pl.gameObject.transform.position.y.ToString ("N6") + ',' 
				+ pl.gameObject.GetComponent<Rigidbody2D> ().velocity.x.ToString ("N6") + ',' 
				+ pl.gameObject.GetComponent<Rigidbody2D> ().velocity.y.ToString ("N6");
		}
		foreach (Player pl in homePlayers) {
			msg += ';'
				+ pl.username + ',' 
				+ pl.team + ',' 
				+ pl.gameObject.transform.position.x.ToString ("N6") + ',' 
				+ pl.gameObject.transform.position.y.ToString ("N6") + ',' 
				+ pl.gameObject.GetComponent<Rigidbody2D> ().velocity.x.ToString ("N6") + ',' 
				+ pl.gameObject.GetComponent<Rigidbody2D> ().velocity.y.ToString ("N6");
		}
	}

	void Start ()
	{
		time = 0.0f;
	}

	void FixedUpdate ()
	{
		while (newPlayers.Count > 0) {
			string username = (string)newPlayers.Pop ();

			Player player;
			player = Instantiate (playerPrefab).GetComponent<Player> ();
			player.team = GlobalState.getTeam ();
			player.username = username;

			if (player.team == Player.Team.Home) {
				homePlayers.Add (player.GetComponent<Player> ());
			} else {
				awayPlayers.Add (player.GetComponent<Player> ());
			}
		}
	}

}
