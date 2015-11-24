using UnityEngine;
using System.Collections;

public class Kicking : MonoBehaviour
{
	public float power = 20f;

	private CircleCollider2D colider;
	private CircleCollider2D ballColider;
	private Rigidbody2D ballBody;
	private int lastKick = 0;

	// Use this for initialization
	void Start ()
	{
		colider = gameObject.GetComponent<CircleCollider2D> ();
		GameObject ball = GameObject.FindGameObjectsWithTag ("Ball") [0];
		ballColider = ball.GetComponent<CircleCollider2D> ();
		ballBody = ball.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey ("x") 
			&& lastKick <= Time.frameCount - 3
			&& colider.IsTouching (ballColider)) {

			Vector2 v = ballBody.gameObject.transform.position - transform.position;
			v = v / v.magnitude;
			Debug.Log (v.magnitude);
			ballBody.AddForce (power * v);
			lastKick = Time.frameCount;
		}
	}
}
