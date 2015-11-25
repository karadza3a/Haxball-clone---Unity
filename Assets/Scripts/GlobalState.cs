using UnityEngine;
using System.Collections;

public class GlobalState : MonoBehaviour
{
	
	public GameObject playerPrefab;

	public struct PlayerStruct
	{
		public string name;
		public Player.Team team;
		
		public PlayerStruct (string name, Player.Team team)
		{
			this.name = name;
			this.team = team;
		}
	}

	public static int homeScore;
	public static int awayScore;

	public static ArrayList homePlayers = ArrayList.Synchronized (new ArrayList ());
	public static ArrayList awayPlayers = ArrayList.Synchronized (new ArrayList ());
	public static float time;
	public static Stack newPlayers = Stack.Synchronized(new Stack()); 

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

	void Update (){
		time = time + Time.deltaTime;
	}

	void Start ()
	{
		time = 0.0f;
	}

	void FixedUpdate ()
	{
		if (newPlayers.Count > 0) {
			PlayerStruct newPlayer = (PlayerStruct)newPlayers.Pop ();
			GameObject player;
			player = Instantiate (playerPrefab);
			player.GetComponent<Player> ().team = newPlayer.team;
			player.GetComponent<Player> ().username = newPlayer.name;
			if (player.GetComponent<Player> ().team == Player.Team.Home){
				homePlayers.Add(player);
			}else{
				awayPlayers.Add(player);
			}
		}
	}

}
