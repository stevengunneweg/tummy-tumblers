using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    public bool Started;

    private void Start(){
        readyText.gameObject.SetActive(false);
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
        cylinderRenderer.material.DOColor(victim.player.realColor, "_ObjectColor", 0.5f).OnComplete(delegate() {
            Started = true; 
        });
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
        if(!Started && Input.GetButtonDown(GetComponent<Player>().GetAxisPrefix() + "_Abutton")){
            Spawn();
        }

        if(Started && Input.GetButtonDown(GetComponent<Player>().GetAxisPrefix() + "_Abutton")){
            ReadyUp();
        }

        if(Started && Input.GetButtonDown(GetComponent<Player>().GetAxisPrefix() + "_Bbutton")){
            Unready();
        }
    }
}
