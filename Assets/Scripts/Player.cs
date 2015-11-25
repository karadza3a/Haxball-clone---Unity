using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float startX = 5f;
	public Team team;
	public string username;

	private PressedKey pressedKeys = 0;

	public bool isPressed (PressedKey key)
	{
		return (pressedKeys & key) != Player.PressedKey.None;
	}

	public void SetPressedKeys (PressedKey keys)
	{
		pressedKeys = keys;
	}
	
	void Start ()
	{		
		// disable collisions with walls
		GameObject[] walls = GameObject.FindGameObjectsWithTag ("Wall");
		foreach (GameObject wall in walls) {
			Physics2D.IgnoreCollision (wall.GetComponent<BoxCollider2D> (), GetComponent<CircleCollider2D> ());
		}
		GetComponent<PlayerMovement> ().reset ();
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
