using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameEndedCanvas : MonoBehaviour {

    public PlayerEndUI[] playerEndUIs;

    public void Show(Player[] players) {
        gameObject.SetActive(true);

        players = players.OrderByDescending(p => p.score).ToArray();
        for (int i = 0; i < playerEndUIs.Length; i++) {
            if (i >= players.Length) {
                playerEndUIs[i].gameObject.SetActive(false);
                continue;
            }

            playerEndUIs[i].gameObject.SetActive(true);
            playerEndUIs[i].SetFrom(players[i]);
        }
    }
}
