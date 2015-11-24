using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float playerSpeed;
	public float maxSpeed;

	private Rigidbody2D rigidb;

	// Use this for initialization
	void Start ()
	{
		rigidb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		MoveForward ();
		rigidb.velocity = Vector2.ClampMagnitude (rigidb.velocity, maxSpeed);
	}
	
	void MoveForward ()
	{	
		if (Input.GetKey ("up")) {//Press up arrow key to move forward on the Y AXIS
			rigidb.AddForce (new Vector2 (0, playerSpeed * Time.deltaTime));
		}
		if (Input.GetKey ("down")) {//Press up arrow key to move forward on the Y AXIS
			rigidb.AddForce (new Vector2 (0, -playerSpeed * Time.deltaTime));
		}
		if (Input.GetKey ("left")) {//Press up arrow key to move forward on the Y AXIS
			rigidb.AddForce (new Vector2 (-playerSpeed * Time.deltaTime, 0));
		}
		if (Input.GetKey ("right")) {//Press up arrow key to move forward on the Y AXIS
			rigidb.AddForce (new Vector2 (playerSpeed * Time.deltaTime, 0));
		}
	}
}
