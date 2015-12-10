using UnityEngine;
using System.Collections;
using System.IO;

// attach to kicker

public class ShootingRecorder : MonoBehaviour
{
	public float power = 1300f;
	
	public int numberOfFramesToRecord = 10;
	public int recordCounter = 0;
	public int phase = 0;
	public string outputPath = "/tmp/out.txt";
		
	private CircleCollider2D colider;
	private CircleCollider2D ballColider;
	private Rigidbody2D ballBody;
	private Player player;
	
	void Start ()
	{
		colider = gameObject.GetComponent<CircleCollider2D> ();
		player = GetComponentInParent<Player> ();
		GameObject ball = GameObject.FindGameObjectsWithTag ("Ball") [0];
		ballColider = ball.GetComponent<CircleCollider2D> ();
		ballBody = ball.GetComponent<Rigidbody2D> ();
		File.WriteAllText (outputPath, "");
	}

	string strrr;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.C)) {
			File.AppendAllText (outputPath, strrr);
		}
	}

	void FixedUpdate ()
	{
		if (phase == 1) {
			if (ballColider.IsTouchingLayers ()) {
				recordCounter = numberOfFramesToRecord;
				phase = 2;
			}
			return;
		}
		if (player.isPressed (Player.PressedKey.Shoot) 
			&& colider.IsTouching (ballColider)) {
			
			recordCounter = numberOfFramesToRecord;
			phase = 0;
			
			Vector2 bv = ballBody.velocity;
			Vector2 bp = ballBody.gameObject.transform.position;
			Vector2 pp = transform.position;
			Vector2 pv = player.gameObject.GetComponent<Rigidbody2D> ().velocity;

			strrr = "--," + pp.x.ToString () + "," + pp.y.ToString () + ","
				+ bp.x.ToString () + "," + bp.y.ToString () + "," + pv.x.ToString () + "," + pv.y.ToString () 
				+ "," + bv.x.ToString () + "," + bv.y.ToString () + "," + "\n";
		}
		
		if (recordCounter > 0) {
			recordCounter --;
			if (recordCounter == 0) {
				phase++;
			}
			Vector3 pos = ballBody.gameObject.transform.position;
			strrr += pos.x.ToString () + "," + pos.y.ToString () + "\n";

		}
	}
}