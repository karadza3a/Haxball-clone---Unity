using UnityEngine;
using System.Collections;

public class GlobalState : MonoBehaviour
{

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

	public static ArrayList homePlayers;
	public static ArrayList awayPlayers;
	public static float time;
	public static Stack newPlayers = new Stack (); 

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
	}

	void Start ()
	{
		time = 0.0f;
		homePlayers = ArrayList.Synchronized (new ArrayList ());
		awayPlayers = ArrayList.Synchronized (new ArrayList ());
	}

}
