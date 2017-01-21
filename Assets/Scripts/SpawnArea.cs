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

    private MenuVictim victim;

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
        cylinderRenderer.material.SetColor("_ObjectColor", victim.player.realColor);
    }

    private void Update(){
    }
}
