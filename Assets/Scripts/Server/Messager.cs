using UnityEngine;
using System.Collections;
using System.Threading;

public class Messager : MonoBehaviour
{
	// login args = l;username
	// play args  = p;username;number(bitmask) 
	// login back args = tim;velicinaterena;stative
	// play back args = result;ball;allPlayers

	public static void receiveMessage (string msg)
	{
		if (msg [0] == 'l') {
			string[] words = msg.Split (';');
			GlobalState.newPlayers.Push (new GlobalState.PlayerStruct (words [1], GlobalState.getTeam ()));
		} else {
			string[] words = msg.Split (';');
			foreach (Player pl in GlobalState.awayPlayers){
				if (pl.GetComponent<Player> ().username == words[1]){
					pl.GetComponent<Player> ().SetPressedKeys((Player.PressedKey)int.Parse(words[2]));
					return;
				}
			}
			foreach (Player pl in GlobalState.homePlayers){
				if (pl.GetComponent<Player> ().username == words[1]){
					pl.GetComponent<Player> ().SetPressedKeys((Player.PressedKey)int.Parse(words[2]));
					return;
				}
			}
		}
	}

	public static string getState ()
	{
		string msg = "";
		//result
		msg += "result:" + GlobalState.getScore () + ';';

		//ball x,y,velocityX,velocityY
		GameObject ball = GameObject.FindGameObjectWithTag ("Ball");
		msg += "ball:" + ball.transform.position.x.ToString("N6") + ',' + ball.transform.position.y.ToString("N6") + ',' + ball.GetComponent<Rigidbody2D> ().velocity.x.ToString("N6") + ',' + ball.GetComponent<Rigidbody2D> ().velocity.y.ToString("N6") + ';';

		//players username,tim,x,y,velocityX,velocityY
		foreach (GameObject pl in GlobalState.awayPlayers){
			msg += "player:" + pl.GetComponent<Player> ().username + ',' + pl.GetComponent<Player> ().team + ',' + pl.transform.position.x.ToString("N6") + ',' + pl.transform.position.y.ToString("N6") + ',' + pl.GetComponent<Rigidbody2D> ().velocity.x.ToString("N6") + ',' + pl.GetComponent<Rigidbody2D> ().velocity.y.ToString("N6") + ';';
		}
		foreach (GameObject pl in GlobalState.homePlayers){
			msg += "player:" + pl.GetComponent<Player> ().username + ',' + pl.GetComponent<Player> ().team + ',' + pl.transform.position.x.ToString("N6") + ',' + pl.transform.position.y.ToString("N6") + ',' + pl.GetComponent<Rigidbody2D> ().velocity.x.ToString("N6") + ',' + pl.GetComponent<Rigidbody2D> ().velocity.y.ToString("N6") + ';';
		}

		return msg;
	}

}
