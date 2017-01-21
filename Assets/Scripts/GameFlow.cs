﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SLinq;

public class GameFlow : MonoBehaviour {
    
    [Header("Prefabs")]
    public GameObject victimPrefab;
    public GameObject playerPrefab;
    public GameObject builderPrefab;

    [Header("References")]
    public CameraController cameraController;
    public SpawnPointGroup spawnPointGroup;
    public Transform towardsFinishTransform;
    public Transform victimParent;
    public Transform playerParent;
    public Transform builderParent;
    public Transform mountainOverviewPointParent;
    public Mountain mountain;

    [Header("Game Play")]
    public int amountOfPlayers = 4;
    private int maxAmountOfPlayers = 4;

    private void Start() {
        builderParent.gameObject.SetActive(false);
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
            Transform[] victimFocusGroup = victimParent.GetComponentsInChildren<Transform>().Skip(1).Include(towardsFinishTransform).ToArray();
            cameraController.Focus(victimFocusGroup);
            while (victimParent.transform.childCount > 0) yield return null;

            Transform[] mountainFocusGroup = mountainOverviewPointParent.GetComponentsInChildren<Transform>().Skip(1).ToArray();
            cameraController.Focus(mountainFocusGroup);
            yield return new WaitForSeconds(5f);

            builderParent.gameObject.SetActive(true);
            foreach (Transform builderChild in builderParent) {
                Builder builder = builderChild.GetComponent<Builder>();
                builder.numberOfPresses = builder.player.score;
                builder.Reset();
            }
            yield return new WaitForSeconds(1000f);
            builderParent.gameObject.SetActive(false);
        }
    }

    private void CreatePlayers() {
        int amountOfPlayers = Mathf.Max(2, Mathf.Min(maxAmountOfPlayers, this.amountOfPlayers));
        for (int i = 0; i < amountOfPlayers; i++) {
            // Create Player
            GameObject instance = Instantiate(playerPrefab);
            instance.name = playerPrefab.name + " " + i.ToString("00");
            instance.transform.parent = playerParent;

            Player player = instance.GetComponent<Player>();
            player.color = Player.COLORS[i];
            player.index = i;
            player.controllerIndex = i;

            // Create Builder
            GameObject builderInstance = Instantiate(builderPrefab);
            builderInstance.name = builderPrefab.name + " (Player " + i.ToString("00") + ")";
            builderInstance.transform.parent = builderParent;

            Builder builder = builderInstance.GetComponent<Builder>();
            builder.player = player;
            builder.mountain = mountain;
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