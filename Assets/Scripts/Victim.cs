using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Victim : MonoBehaviour {
    
    [HideInInspector]
    public Player player;
    private const string deadzoneTag = "Deadzone";
    private const string finishTag = "Finish";

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == deadzoneTag){
            Kill();
        }else if(collision.gameObject.tag == finishTag){
            Finish();
        }
    }

    public void Finish(){
        player.score++;
        Kill();
    }

    public void Kill(){
        Destroy(gameObject);
    }
}
