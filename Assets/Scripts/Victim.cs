using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Victim : MonoBehaviour {
    
    [HideInInspector]
    public Player player;
    private const string deadzoneTag = "Deadzone";
    private const string finishTag = "Finish";

	private Vector3 collisionVector;

    private void OnCollisionEnter(Collision collision) {
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Victims")) {
			Rigidbody objectBody = collision.collider.gameObject.GetComponent<Rigidbody> ();
			collisionVector = objectBody.transform.position - transform.position;
		}

        if(collision.gameObject.tag == deadzoneTag){
            Kill();
        }else if(collision.gameObject.tag == finishTag){
            Finish();
        }
    }

	private void OnCollisionExit(Collision collision) {
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Victims")) {
			if (this.collisionVector != Vector3.zero) {
				Rigidbody objectBody = collision.collider.gameObject.GetComponent<Rigidbody> ();
				objectBody.AddForce (this.collisionVector.normalized * 500);
				objectBody.AddForce (Vector3.up * 1000);
				this.collisionVector = Vector3.zero;
			}
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
