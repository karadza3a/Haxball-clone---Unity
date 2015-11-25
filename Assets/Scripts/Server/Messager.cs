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
				if (pl.username == words[1]){
					pl.SetPressedKeys((Player.PressedKey)int.Parse(words[2]));
					return;
				}
			}

			foreach (Player pl in GlobalState.homePlayers){
				if (pl.username == words[1]){
					pl.SetPressedKeys((Player.PressedKey)int.Parse(words[2]));
					return;
				}
			}
		}
	}

	public static string getState ()
	{
		return GlobalState.msg;
	}

}
