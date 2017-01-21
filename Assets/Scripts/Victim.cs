using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Victim : MonoBehaviour {

    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Mountain mountain;
    private Vector3 collisionVector;

    private const string deadzoneTag = "Deadzone";
    private const string finishTag = "Finish";

    [Header("Destroy")]
    public float destroyDuration = 1f;
    public float destroyDistanceThreshold = 1f;
    private float _currentDestoryTime;
    
    protected void Update() {
        // Find current and oldest position in Mountain space
        Tracker tracker = GetComponent<Tracker>();
        Vector3 oldestPosition = mountain.transform.InverseTransformPoint(tracker.GetOldestTrackedPosition());
        Vector3 currentPosition = mountain.transform.InverseTransformPoint(transform.position);

        // Check if moved forward more than the destroy distance threshold
        if (Mathf.Abs(oldestPosition.z - currentPosition.z) < destroyDistanceThreshold) {
            _currentDestoryTime += Time.deltaTime;
            if (_currentDestoryTime >= destroyDuration) {
                Kill();
            }
        } else {
            _currentDestoryTime = 0f;
        }
    }

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
