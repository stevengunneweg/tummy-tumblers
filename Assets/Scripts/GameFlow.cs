﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SLinq;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameFlow : MonoBehaviour {
    
    public static GameFlow instance {
        get {
            return GameObject.FindGameObjectsWithTag("GameFlow").Select(g => g.GetComponent<GameFlow>()).Where(g => g != null).FirstOrDefault();
        }
    }

    [Header("Prefabs")]
    public GameObject victimPrefab;
    public GameObject playerPrefab;
    public GameObject builderPrefab;
    public List<BaseStructure> structurePrefabs;

    [Header("References")]
    public CameraController cameraController;
    public SpawnPointGroup spawnPointGroup;
    public Transform towardsFinishTransform;
    public Transform victimParent;
    public Transform playerParent;
    public Transform builderParent;
    public GameEndedCanvas gameEndedCanvas;
    public Transform mountainOverviewPointParent;
    public Mountain mountain;
    public BuildModeUI buildModeUI;
    public Text goText;
    public AudioSource tuuuutSound;
    public AudioSource tuuuut2Sound;
    public AudioSource musicSound;

    [Header("Game Play")]
    public int amountOfPlayers = 4;
    public const int maxAmountOfPlayers = 4;
    public int maxScore = 10;

	private gameData data;

    private void Start() {
		if (GameObject.Find ("gameData") != null) {
			data = GameObject.Find ("gameData").GetComponent<gameData> ();
			amountOfPlayers = data.NRPlayer;
		}
        builderParent.gameObject.SetActive(false);
        StartGameFlow();
    }

    protected void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }
    }

    public Coroutine StartGameFlow() {
        return StartCoroutine(DoGameFlow());
    }
    private IEnumerator DoGameFlow() {
        // Setup the game
        CreatePlayers();

        // Start the game
        while (true) {

            yield return new WaitForSeconds(1f);

            // Spawn new round of victims and focus camera on them
            SpawnVictims();
            Transform[] victimFocusGroup = victimParent.GetComponentsInChildren<Transform>().Skip(1).Include(towardsFinishTransform).ToArray();
            cameraController.Focus(victimFocusGroup);

            foreach(Victim victim in victimParent.GetComponentsInChildren<Victim>()){
                victim.GetComponent<Rigidbody>().isKinematic = true;
                victim.GetComponent<Tracker>().enabled = false;
            }

            // DO intro sequenceeeeee
            goText.gameObject.SetActive(true);
            goText.transform.localScale = Vector3.zero;
            goText.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
            for(int i = 3; i != 0; i--){
                goText.transform.localScale = Vector3.zero;
                goText.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
                goText.text = i.ToString();
                tuuuut2Sound.Play();
                yield return new WaitForSeconds(1);
            }

            FindObjectOfType<Timetrail>().OnRoundStarted();

            tuuuutSound.Play();
            musicSound.Play();

            goText.transform.localScale = Vector3.zero;
            goText.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
            goText.text = "GOOOOO!";
            goText.transform.DOShakePosition(0.9f, 10, 80).OnComplete(delegate() {
                goText.gameObject.SetActive(false); 
            });

            foreach(Victim victim in victimParent.GetComponentsInChildren<Victim>()){
                victim.GetComponent<Rigidbody>().isKinematic = false;
                victim.GetComponent<Tracker>().enabled = true;
            }
            // lets play

            // Wait on the victims to all get destroyed (kill or finish)
            while (victimParent.transform.childCount > 0) {
                yield return null;
            }

            // Focus on the mountain
            Transform[] mountainFocusGroup = mountainOverviewPointParent.GetComponentsInChildren<Transform>().Skip(1).ToArray();
            cameraController.Focus(mountainFocusGroup);

            FindObjectOfType<Timetrail>().OnEverybodyFinished();

            yield return new WaitForSeconds(1);

            FindObjectOfType<Timetrail>().OnDoPointsCalculation(8);

            // Wait for the mountain to get into view
            yield return new WaitForSeconds(4f);

            // Check wincondition
            Player[] players = playerParent.GetComponentsInChildren<Player>();
            if (players.Any(p => p.score >= maxScore)) {
                gameEndedCanvas.Show(players);
                yield break;
            }
            FindObjectOfType<Timetrail>().OnRoundEnded();

            // Start building
            buildModeUI.ShowBuildTime();
            builderParent.gameObject.SetActive(true);
            List<Builder> builders = builderParent.GetComponentsInChildren<Builder>().ToList();
            foreach (Builder builder in builders) {
                builder.StructurePrefab = structurePrefabs[Random.Range(0, structurePrefabs.Count)];
                builder.Reset();
            }
            cameraController.Focus(builderParent.GetComponentsInChildren<Transform>().Skip(1).ToArray());

            // Wait for camera to focus on builders
            yield return new WaitForSeconds(2f);

            // Show the build overlay and wait for them to build, then hide builder
            buildModeUI.ShowPlacingHelpMessage();
            buildModeUI.ShowBuildOverlays(builders);
            while(builders.Exists(b => b.HasBuild() == false)){ yield return null; }
            foreach(Builder builder in builders){
                buildModeUI.HideBuildOverlay(builder);
            }
            buildModeUI.HidePlacingHelpMessage();
            builderParent.gameObject.SetActive(false);
        }
    }

    private void CreatePlayers() {
		for (int i = 0; i < maxAmountOfPlayers; i++) {
			if (data != null) {
				if (!data.ReadyPlayers [i]) {
					continue;
				}
			}

            // Create Player
            GameObject instance = Instantiate(playerPrefab);
            instance.name = playerPrefab.name + " " + i.ToString("00");
            instance.transform.parent = playerParent;

            Player player = instance.GetComponent<Player>();
            player.index = i;
            player.controllerIndex = i;

            // Create Builder
            GameObject builderInstance = Instantiate(builderPrefab);
            builderInstance.name = builderPrefab.name + " (Player " + i.ToString("00") + ")";
            builderInstance.transform.parent = builderParent;
            builderInstance.transform.localPosition = Vector3.forward * 10 * (i - amountOfPlayers / 2.0f);

            Builder builder = builderInstance.GetComponent<Builder>();
            builder.player = player;
            builder.mountain = mountain;
        }
    }

    private void SpawnVictims() {
        const int numberOfVictims = 2;
        Transform[] spawnTransforms = spawnPointGroup.GetSpawnPoints(numberOfVictims * playerParent.childCount);

		List<List<int>> randomOrders = new List<List<int>>(numberOfVictims);
		for (int i = 0; i < numberOfVictims; i++) {
			randomOrders.Add (GetRandomSpawnArray());
		}

        // Spawn the Victims
        for (int playerIndex = 0; playerIndex < playerParent.childCount; playerIndex++){
            Player player = playerParent.GetChild(playerIndex).GetComponent<Player>();

            for(int victimIndex = 0; victimIndex < numberOfVictims; victimIndex++){
                GameObject victimInstance = Instantiate(victimPrefab);
                victimInstance.name = victimPrefab.name + " (From player " + player.index.ToString("00") + ")";
                victimInstance.transform.parent = victimParent;

				victimInstance.transform.position = spawnTransforms[randomOrders[victimIndex][playerIndex] * numberOfVictims + victimIndex].position;
				victimInstance.transform.position += Vector3.forward * UnityEngine.Random.Range (0, 3);
				victimInstance.transform.position += Vector3.up * UnityEngine.Random.Range (0, 3);

                Victim victim = victimInstance.GetComponent<Victim>();
                victim.player = player;
                victim.mountain = mountain;
                victim.index = victimIndex;
            }
        }
    }

	private List<int> GetRandomSpawnArray() {
		List<int> randomOrder = new List<int>(new int[this.amountOfPlayers]);
		for (int i = 0; i < randomOrder.Count; i++) {
			randomOrder [i] = i;
		}
		for (int i = 0; i < randomOrder.Count; i++) {
			int temp = randomOrder [i];
			int randomIndex = UnityEngine.Random.Range (i, randomOrder.Count);
			randomOrder [i] = randomOrder [randomIndex];
			randomOrder [randomIndex] = temp;
		}
		return randomOrder;
	}

}
