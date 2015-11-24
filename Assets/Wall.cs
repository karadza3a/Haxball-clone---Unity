using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		foreach (GameObject player in players) {
			Physics2D.IgnoreCollision (player.GetComponent<CircleCollider2D> (), GetComponent<EdgeCollider2D> ());
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
