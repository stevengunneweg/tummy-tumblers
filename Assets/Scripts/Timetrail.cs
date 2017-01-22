using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timetrail : MonoBehaviour {

    private TimetrailUI ui;

    private float roundStartedTime;
    private List<Player> rankings;

    private void Start(){
        ui = FindObjectOfType<TimetrailUI>();
        rankings = new List<Player>();
    }

    public void OnRoundStarted(){
        rankings.Clear();
        roundStartedTime = Time.timeSinceLevelLoad;
    }

    public void OnVictimFinished(Victim victim, Player player){
        float time = Time.timeSinceLevelLoad - roundStartedTime;
        ui.SpawnElement(player, time, rankings.Count + 1);

        rankings.Add(player);
    }

    public void OnRoundEnded(){
        ui.Hide();
    }

}
