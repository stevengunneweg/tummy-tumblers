using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	
	// Update is called once per frame
	private void Update () {
        SpawnArea[] areas = FindObjectsOfType<SpawnArea>();

        if(areas.Count(a => a.IsReady) == areas.Count(a => a.Started) && areas.Count(a => a.IsReady) != 0){
            SceneManager.LoadScene("waves_main");
        }
	}
}
