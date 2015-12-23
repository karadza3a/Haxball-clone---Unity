using UnityEngine;
using System.Collections;

public class Kicking : MonoBehaviour
{
	public float power = 20f;

	private CircleCollider2D colider;
	private CircleCollider2D ballColider;
	private Rigidbody2D ballBody;
	private Player player;
	private int lastShot = 0;

	void Start ()
	{
		colider = gameObject.GetComponent<CircleCollider2D> ();
		player = GetComponentInParent<Player> ();
		GameObject ball = GameObject.FindGameObjectsWithTag ("Ball") [0];
		ballColider = ball.GetComponent<CircleCollider2D> ();
		ballBody = ball.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		if (player.isPressed (Player.PressedKey.Shoot) 
			&& colider.IsTouching (ballColider)
			&& Time.frameCount - lastShot > 2) {

			lastShot = Time.frameCount;
			Vector2 v = ballBody.gameObject.transform.position - transform.position;
			v = v / v.magnitude;
			v = v * power * 100;
			ballBody.AddForce (v);
		}
	}
}
