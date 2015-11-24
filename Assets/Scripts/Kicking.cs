using UnityEngine;
using System.Collections;

public class Kicking : MonoBehaviour
{
	public float power = 260f;

	private CircleCollider2D colider;
	private CircleCollider2D ballColider;
	private Rigidbody2D ballBody;
	private Player player;
	private int lastKick = 0;
	
	void Start ()
	{
		colider = gameObject.GetComponent<CircleCollider2D> ();
		player = gameObject.GetComponent<Player> ();
		GameObject ball = GameObject.FindGameObjectsWithTag ("Ball") [0];
		ballColider = ball.GetComponent<CircleCollider2D> ();
		ballBody = ball.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		if (player.isPressed (Player.PressedKey.Shoot) 
			&& lastKick <= Time.frameCount - 3
			&& colider.IsTouching (ballColider)) {

			Vector2 v = ballBody.gameObject.transform.position - transform.position;
			v = v / v.magnitude;
			v = v * power;
			ballBody.AddForce (v);
			lastKick = Time.frameCount;
		}
	}
}
