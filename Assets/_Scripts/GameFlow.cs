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
    private BuildmodeUI buildmodeUI;

    public List<Player> Players { get; private set; }
    public bool IsInBuildingMode;
    public int Credits;

    private float currentVictimAmount;
    private float victimsAlive;

    private Color[] playerColors = new Color[]{
        Color.red, Color.yellow, Color.green, Color.blue
    };

    private void Awake(){
        Players = new List<Player>();
        for(int i = 0; i < 4; i++){
            Players.Add(new Player(0, playerColors[i]));
        }
    }

    private void Start(){
        StartWave();
    }

    public void StartWave(){
        IsInBuildingMode = false;
        StartCoroutine(StartWaveRoutine());
    }

    private IEnumerator StartWaveRoutine(){
        yield return null;

        currentVictimAmount = 2;

        int ballPosition = 0;
        for(int j = 0; j < Players.Count; j++){
            Player player = Players[j];
            for(int i = 0; i < currentVictimAmount; i++){
                Victim victim = Instantiate(victimPrefab);
                victim.Player = player;
                victim.transform.position = victimSpawnPoint.position;
                victim.transform.position += new Vector3((-currentVictimAmount * Players.Count / 2) * victim.Width + ballPosition * victim.Width, 0, 0);
                victim.OnVictimKill += OnVictimKilled;
                victim.OnVictimFinished += OnVictimFinished;
                ballPosition++;
                victimsAlive++;
            }
        }
    }

    public void EndRound(){
        buildmodeUI.Show();

        StartBuildMode();

        buildmodeUI.Hide();
    }

    public void StartBuildMode(){
        StartWave();
    }

    private void OnVictimKilled(){
        Credits--;
        victimsAlive--;

        if(victimsAlive <= 0){
            EndRound();
        }
    }

    private void OnVictimFinished(){
        Credits++;
        victimsAlive--;

        if(victimsAlive <= 0){
            EndRound();
        }
    }

}
