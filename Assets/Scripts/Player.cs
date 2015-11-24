using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float startX = 5f;
	public Team team;

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
