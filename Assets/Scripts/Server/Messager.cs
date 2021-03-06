﻿using UnityEngine;
using System.Collections;
using System.Threading;

public class Messager : MonoBehaviour
{
	// login args = l;username
	// play args  = p;username;number(bitmask)
	// play back args = result;ball position and velocity;player1;player2...

	public static void receiveMessage (string msg)
	{
		if (msg [0] == 'l') {
			string[] words = msg.Split (';');
			GlobalState.newPlayers.Push (words [1]);
		} else {
			string[] words = msg.Split (';');
			foreach (Player pl in GlobalState.awayPlayers) {
				if (pl.username == words [1]) {
					pl.SetPressedKeys ((Player.PressedKey)int.Parse (words [2]));
					return;
				}
			}

			foreach (Player pl in GlobalState.homePlayers) {
				if (pl.username == words [1]) {
					pl.SetPressedKeys ((Player.PressedKey)int.Parse (words [2]));
					return;
				}
			}
		}
	}

}
