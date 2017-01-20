using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Victim : MonoBehaviour {

	public Action OnVictimKill;
    public Action OnVictimFinished;
	private float CollisionMagnitude;

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
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Victims")) {
			this.CollisionMagnitude = GetComponent<Rigidbody>().velocity.magnitude;
		}

        if(collision.gameObject.GetComponent<Deadzone>() != null){
            Kill();
        }else if(collision.gameObject.GetComponent<Finish>() != null){
            Finish();
        }
    }

	private void OnCollisionExit(Collision collision) {
		if (this.CollisionMagnitude != 0) {
			GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * (this.CollisionMagnitude * 3);
			this.CollisionMagnitude = 0;
		}
	}

    public void Finish(){
        GameObject.Destroy(gameObject);

        if(OnVictimFinished != null){
            OnVictimFinished.Invoke();
        }
    }

    public void Kill(){
        GameObject.Destroy(gameObject);

        if(OnVictimKill != null){
            OnVictimKill.Invoke();
        }
    }
}
