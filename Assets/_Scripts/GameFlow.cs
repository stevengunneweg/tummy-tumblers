using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour {

    [SerializeField]
    private Victim victimPrefab;

    [SerializeField]
    private Transform victimSpawnPoint;

    [SerializeField]
    private Text victimAmountText;

    public bool IsBuildingAllowed;
    public int Credits;


    private float currentVictimAmount;
    private float victimsAlive;
    private float victimsFinished;

    private void Start(){
        StartWave();
    }

    public void StartWave(){
        currentVictimAmount = 10;
        victimsAlive = currentVictimAmount;
        victimsFinished = 0;

        for(int i = 0; i < currentVictimAmount; i++){
            Victim victim = Instantiate(victimPrefab);
            victim.transform.position = victimSpawnPoint.position;
            victim.transform.position += new Vector3((-currentVictimAmount / 2) * victim.Width + i * victim.Width, 0, 0);
            victim.OnVictimKill += OnVictimKilled;
            victim.OnVictimFinished += OnVictimFinished;
        }
    }

    public void EndRound(){
        StartWave();
    }

    private void OnVictimKilled(){
        Credits--;
        victimsAlive--;
    }

    private void OnVictimFinished(){
        Credits++;
        victimsAlive--;
        victimsFinished++;
    }

    private void Update(){
        if(victimsAlive <= 0){
            EndRound();
        }

        victimAmountText.text = victimsFinished + "/" + currentVictimAmount;
    }

}
