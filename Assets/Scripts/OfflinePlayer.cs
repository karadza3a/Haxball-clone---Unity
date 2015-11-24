using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OfflinePlayer : MonoBehaviour
{
	public Player.Team team;

	[System.Serializable]
	public struct Pair
	{
		public char Key;
		public Player.PressedKey Value;
	}
	public Pair[] keys;

	public GameObject playerPrefab;

	private GameObject player;

	void Start ()
	{
		player = Instantiate (playerPrefab);
		player.GetComponent<Player> ().team = team;
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
