using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OfflinePlayer : MonoBehaviour
{
	public Player.Team team;
	public string username;

	[System.Serializable]
	public struct Pair
	{
		public char Key;
		public Player.PressedKey Value;
	}
	public Pair[] keys;

	public GameObject playerPrefab;

	private Player player;

	void Start ()
	{
		player = Instantiate (playerPrefab).GetComponent<Player> ();
		player.team = team;
		player.username = username;
	}

	void FixedUpdate ()
	{
		Player.PressedKey pressed = 0;
		foreach (Pair entry in keys) {
			if (Input.GetKey ("" + entry.Key)) {
				pressed |= entry.Value;
			}
		}
		player.GetComponent<Player> ().SetPressedKeys (pressed);
	}
}
