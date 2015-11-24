using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float startX = 5f;
	public Team team;

	private PressedKey pressedKeys = 0;

	public Player (Team team)
	{
		this.team = team;
	}

	public bool isPressed (PressedKey key)
	{
		return (pressedKeys & key) != Player.PressedKey.None;
	}
	
	void Start ()
	{
		Vector2 pos = new Vector2 ((((int)team) * 5), 0);
		transform.position = pos;
	}
	
	public enum PressedKey
	{
		None = 0,
		Up = 1,
		Right = 2,
		Down = 4,
		Left = 8,
		Shoot = 16
	}
	public enum Team
	{
		Home = -1,
		Away = 1
	}
}
