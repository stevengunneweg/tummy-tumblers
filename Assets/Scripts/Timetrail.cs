using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timetrail : MonoBehaviour {

    [SerializeField]
    private AudioSource ahwSoundEffect;

    [SerializeField]
    private AudioSource dingSound;

    private TimetrailUI ui;

    private float roundStartedTime;
    private List<Player> rankings;
    private List<Player> diedPlayers;

    private void Start(){
        ui = FindObjectOfType<TimetrailUI>();
        rankings = new List<Player>();
        diedPlayers = new List<Player>();
    }

    public void OnRoundStarted(){
        rankings.Clear();
        diedPlayers.Clear();
        roundStartedTime = Time.timeSinceLevelLoad;
    }

    public void OnVictimFinished(Victim victim, Player player){
        float time = Time.timeSinceLevelLoad - roundStartedTime;
        ui.SpawnElement(player, time, rankings.Count + 1);

        rankings.Add(player);

        dingSound.Play();
    }

    public void OnVictimDied(Player player){
        diedPlayers.Add(player);
    }

    public void OnEverybodyFinished(){
        foreach(Player player in diedPlayers){
            ui.SpawnElement(player, float.MaxValue, int.MaxValue, true);
        }

        if(diedPlayers.Count != 0){
            ahwSoundEffect.Play();
            dingSound.Play();
        }
    }

    public void OnRoundEnded(){
        ui.Hide();
    }

}
