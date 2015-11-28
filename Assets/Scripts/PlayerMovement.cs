using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float acceleration = 500;
	public float shootDecceleration = .8f;
	private Rigidbody2D rigidb;
	private Player player;

	/**
     * Resets position, velocity and similar
     */
	public void reset ()
	{
		Vector2 pos = new Vector2 (((int)player.team) * Random.Range (5, 10), Random.Range (-10, 10));
		transform.position = pos;
		rigidb.velocity = Vector2.zero;
	}

	void Start ()
	{
		rigidb = gameObject.GetComponent<Rigidbody2D> ();
		player = gameObject.GetComponent<Player> ();
	}

	void FixedUpdate ()
	{
		if (GlobalState.gameStarted)
			Move ();
	}

	void Move ()
	{
		Vector2 v = new Vector2 (0, 0);
		if (player.isPressed (Player.PressedKey.Up)) {
			v += new Vector2 (0, 1);
		}
		if (player.isPressed (Player.PressedKey.Down)) {
			v += new Vector2 (0, -1);
		}
		if (player.isPressed (Player.PressedKey.Left)) {
			v += new Vector2 (-1, 0);
		}
		if (player.isPressed (Player.PressedKey.Right)) {
			v += new Vector2 (1, 0);
		}
		AddLimitedForce (v);
	}

	void AddLimitedForce (Vector2 force)
	{
		force = force.normalized;
		force = force * acceleration * Time.deltaTime;
		if (player.isPressed (Player.PressedKey.Shoot)) {
			force *= shootDecceleration;
		}
		rigidb.AddForce (force);
	}
}
