using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildmodeUI : MonoBehaviour {

    [SerializeField]
    private PlayerBar[] bars;

    private GameFlow gameFlow;

    private void Start(){
        gameFlow = FindObjectOfType<GameFlow>();
        bars = GetComponentsInChildren<PlayerBar>();

        foreach(PlayerBar bar in bars){
            bar.gameObject.SetActive(false);
        }

        for(int i = 0; i < gameFlow.Players.Count; i++){
            Player player = gameFlow.Players[i];

            bars[i].SetPlayer(player);
            bars[i].gameObject.SetActive(true);
        }
    }

    public void Show(){
    }

    public void Hide(){
    }   

}
