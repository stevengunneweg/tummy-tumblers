using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnArea : MonoBehaviour {

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private MenuVictim victimPrefab;

    [SerializeField]
    private int playerIndex;

    [SerializeField]
    private Renderer cylinderRenderer;

    [SerializeField]
    private Text startText;

    [SerializeField]
    private Text readyText;

    private MenuVictim victim;

    public bool IsReady;

    private void Start(){
        Spawn();
    }

    public void Spawn(){
        victim = Instantiate<MenuVictim>(victimPrefab);
        victim.player = GetComponent<Player>();
        victim.transform.position = spawnPoint.position;
        victim.spawnArea = this;
        victim.transform.rotation = Random.rotation;
        startText.gameObject.SetActive(false);
        readyText.gameObject.SetActive(true);
        cylinderRenderer.material.SetColor("_ObjectColor", victim.player.realColor);
    }

    public void ReadyUp(){
        IsReady = true;
        readyText.gameObject.SetActive(false);
    }

    public void Unready(){
        IsReady = false;
        readyText.gameObject.SetActive(true);
    }

    private void Update(){
        if(Input.GetButtonDown(victim.player.GetAxisPrefix() + "_Abutton")){
            ReadyUp();
        }

        if(Input.GetButtonDown(victim.player.GetAxisPrefix() + "_Bbutton")){
            Unready();
        }
    }
}
