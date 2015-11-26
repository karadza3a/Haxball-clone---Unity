using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OfflinePlayer : MonoBehaviour
{
    public string username;
    public Pair[] keys;

    private Player player;

    void Start()
    {
        Messager.receiveMessage(string.Format("l;{0}", username));
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            foreach (Player pl in GlobalState.awayPlayers)
            {
                if (pl.username == username)
                {
                    player = pl;
                    break;
                }
            }
            foreach (Player pl in GlobalState.homePlayers)
            {
                if (pl.username == username)
                {
                    player = pl;
                    break;
                }
            }
            //not initialized yet - return
            return;
        }
        Player.PressedKey pressed = 0;
        foreach (Pair entry in keys)
        {
            if (Input.GetKey("" + entry.Key))
            {
                pressed |= entry.Value;
            }
        }
        player.GetComponent<Player>().SetPressedKeys(pressed);
    }

    [System.Serializable]
    public struct Pair
    {
        public string Key;
        public Player.PressedKey Value;
    }
}
