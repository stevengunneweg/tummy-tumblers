using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Victim : MonoBehaviour {

	public Action OnVictimKill;
    public Action OnVictimFinished;

    public Player Player;

    private SphereCollider sphereCollider;

    public float Width {
        get {
            return sphereCollider.radius * 2 + 0.5f;
        }
    }

	private void Awake(){
        sphereCollider = GetComponent<SphereCollider>();
	}

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.GetComponent<Deadzone>() != null){
            Kill();
        }else if(collision.gameObject.GetComponent<Finish>() != null){
            Finish();
        }
    }

    public void Finish(){
        if(OnVictimFinished != null){
            OnVictimFinished.Invoke();
        }

        GameObject.Destroy(gameObject);
    }

    public void Kill(){
        if(OnVictimKill != null){
            OnVictimKill.Invoke();
        }

        GameObject.Destroy(gameObject);
    }
}
