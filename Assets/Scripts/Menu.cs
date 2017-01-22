using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    GameObject gameData;
    void Start()
    {
        gameData = GameObject.Find("gameData");
    }

	// Update is called once per frame
	private void Update () {
        SpawnArea[] areas = FindObjectsOfType<SpawnArea>();

        if(areas.Count(a => a.IsReady) == areas.Count(a => a.Started) && areas.Count(a => a.IsReady) != 0){
            if (areas.Count(a => a.IsReady) > 1)
            {
                gameData.GetComponent<gameData>().NRPlayer = areas.Count(a => a.IsReady);
                SceneManager.LoadScene("waves_main");
            }

        }
	}
}
