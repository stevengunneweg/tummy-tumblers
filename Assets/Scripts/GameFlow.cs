using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SLinq;

public class GameFlow : MonoBehaviour {
    
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
    public Transform mountainOverviewPointParent;
    public Mountain mountain;
    public BuildModeUI buildModeUI;

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

            while (victimParent.transform.childCount > 0) {
                yield return null;
            }

            Transform[] mountainFocusGroup = mountainOverviewPointParent.GetComponentsInChildren<Transform>().Skip(1).ToArray();
            cameraController.Focus(mountainFocusGroup);

            yield return new WaitForSeconds(2f);

            buildModeUI.ShowBuildTime();

            builderParent.gameObject.SetActive(true);
            List<Builder> builders = builderParent.GetComponentsInChildren<Builder>().ToList();
            foreach (Builder builder in builders) {
                builder.StructurePrefab = structurePrefabs[Random.Range(0, structurePrefabs.Count)];
                builder.Reset();
            }

            cameraController.Focus(builderParent.GetComponentsInChildren<Transform>().Skip(1).ToArray());

            yield return new WaitForSeconds(2f);

            buildModeUI.ShowBuildOverlays(builders);

            while(builders.Exists(b => b.HasBuild() == false)){
                yield return new WaitForSeconds(1);
            }

            foreach(Builder builder in builders){
                buildModeUI.HideBuildOverlay(builder);
            }

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
            builderInstance.transform.localPosition = Vector3.zero;

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
		List<int> randomOrder = new List<int>{0, 1, 2, 3};
		for (int i = 0; i < randomOrder.Count; i++) {
			int temp = randomOrder [i];
			int randomIndex = UnityEngine.Random.Range (i, randomOrder.Count);
			randomOrder [i] = randomOrder [randomIndex];
			randomOrder [randomIndex] = temp;
		}
		return randomOrder;
	}

}
