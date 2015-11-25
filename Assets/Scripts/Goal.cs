using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{

	public Player.Team team;
	private BoxCollider2D goalBox;
	private EdgeCollider2D goalLine;
	private CircleCollider2D ball;

	void Start ()
	{
		goalBox = GetComponent<BoxCollider2D> ();
		goalLine = GetComponent<EdgeCollider2D> ();
		ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ball.IsTouching (goalBox) && !ball.IsTouching (goalLine)) {
			Debug.Log (team);
			ball.gameObject.transform.position = new Vector2 (0, 0);
			ball.attachedRigidbody.velocity = new Vector2 (0, 0);
			
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject player in players) {
				player.GetComponent<PlayerMovement> ().reset ();
			}
		}
	}
}
