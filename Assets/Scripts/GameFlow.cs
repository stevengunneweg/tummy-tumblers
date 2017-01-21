using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour {
    
    [Header("Prefabs")]
    public GameObject victimPrefab;
    public GameObject playerPrefab;

    [Header("References")]
    public CameraController cameraController;
    public SpawnPointGroup spawnPointGroup;
    public Transform victimParent;
    public Transform playerParent;
    public Transform mountainOverviewPointParent;

    [Header("Game Play")]
    public int amountOfPlayers = 4;
    private int maxAmountOfPlayers = 4;

    private void Start() {
        StartGameFlow();
    }

    public Coroutine StartGameFlow() {
        return StartCoroutine(DoGameFlow());
    }
    private IEnumerator DoGameFlow() {
        // Setup the game
        CreatePlayers();

        // Start the game
        while (true) {
            SpawnVictims();
            cameraController.Focus(victimParent);
            while (victimParent.transform.childCount > 0) yield return null;

            cameraController.Focus(mountainOverviewPointParent);
            yield return new WaitForSeconds(3f);

            // TODO add build round here
        }
    }

    private void CreatePlayers() {
        int amountOfPlayers = Mathf.Max(2, Mathf.Min(maxAmountOfPlayers, this.amountOfPlayers));
        for (int i = 0; i < amountOfPlayers; i++) {
            GameObject instance = Instantiate(playerPrefab);
            instance.name = playerPrefab.name + " " + i.ToString("00");
            instance.transform.parent = playerParent;

            Player player = instance.GetComponent<Player>();
            player.color = Player.COLORS[i];
            player.index = i;
        }
    }

    private void SpawnVictims() {
        const int numberOfVictims = 2;

        Transform[] spawnTransforms = spawnPointGroup.GetSpawnPoints(numberOfVictims * playerParent.childCount);

        for (int playerIndex = 0; playerIndex < playerParent.childCount; playerIndex++){
            Player player = playerParent.GetChild(playerIndex).GetComponent<Player>();

            for(int victimIndex = 0; victimIndex < numberOfVictims; victimIndex++){
                GameObject victimInstance = Instantiate(victimPrefab);
                victimInstance.name = victimPrefab.name + " (From player " + player.index.ToString("00") + ")";
                victimInstance.transform.parent = victimParent;
                victimInstance.transform.position = spawnTransforms[playerIndex * numberOfVictims + victimIndex].position;

                Victim victim = victimInstance.GetComponent<Victim>();
                victim.player = player;
            }
        }
    }

}
