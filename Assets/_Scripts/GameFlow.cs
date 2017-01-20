using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour {

    [SerializeField]
    private Victim victimPrefab;

    [SerializeField]
    private Transform victimSpawnPoint;

    private List<Player> players;

    public bool IsBuildingAllowed;
    public int Credits;

    private float currentVictimAmount;
    private float victimsAlive;
    private float victimsFinished;

    private void Start(){
        players = new List<Player>();
        players.Add(new Player(0));
        players.Add(new Player(1));
        players.Add(new Player(2));
        StartWave();
    }

    public void StartWave(){
        StartCoroutine(StartWaveRoutine());
    }

    private IEnumerator StartWaveRoutine(){
        yield return new WaitForSeconds(1);

        currentVictimAmount = 2;
        victimsAlive = currentVictimAmount;
        victimsFinished = 0;

        foreach(Player player in players){
            for(int i = 0; i < currentVictimAmount; i++){
                Victim victim = Instantiate(victimPrefab);
                victim.Player = player;
                victim.transform.position = victimSpawnPoint.position;
                victim.transform.position += new Vector3((-currentVictimAmount / 2) * victim.Width + i * victim.Width, 0, 0);
                victim.OnVictimKill += OnVictimKilled;
                victim.OnVictimFinished += OnVictimFinished;
            }
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
