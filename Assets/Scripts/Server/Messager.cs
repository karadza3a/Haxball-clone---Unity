using UnityEngine;
using System.Collections;
using System.Threading;

public class Messager : MonoBehaviour {
	// login args = l;username;<EOM>
	// play args  = p;username;number(bitmask);<EOM> 
	// login back args = tim;velicinaterena;stative;<EOM>
	// play back args = result;ball;allPlayers;<EOM>

	public static void receiveMessage(string msg){
		if (msg [0] == 'l') {
			string[] words = msg.Split(';');
			GlobalState.newPlayers.Push(new GlobalState.PlayerStruct(words[1], GlobalState.getTeam()));
		} else {


		}
	}


}
