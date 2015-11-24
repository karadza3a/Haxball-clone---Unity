using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
	void Start ()
	{
		// Disable collision with players
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			Physics2D.IgnoreCollision (player.GetComponent<CircleCollider2D> (), GetComponent<EdgeCollider2D> ());
		}
	}
}
