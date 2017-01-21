using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Victim : MonoBehaviour {

	public List<AudioClip> soundsMale;	
	public List<AudioClip> soundsFemale;
	public AudioClip soundScreamMale;	
	public AudioClip soundScreamFemale;
    
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Mountain mountain;
    [HideInInspector]
    public int index = 0;

    private Vector3 collisionVector;
    private const string deadzoneTag = "Deadzone";
    private const string finishTag = "Finish";
	private bool gender;
	private bool isFalling;

    [Header("Destroy")]
    public float destroyDuration = 1f;
    public float destroyDistanceThreshold = 1f;
    private float _currentDestoryTime;
    
    protected void Update() {
		if (GetComponent<Rigidbody> ().velocity.y < -8) {
			if (!this.isFalling) {
				this.isFalling = true;
				if (this.gender) {
					GetComponent<AudioSource> ().clip = soundScreamMale;
				} else {
					GetComponent<AudioSource> ().clip = soundScreamFemale;
				}
				GetComponent<AudioSource> ().Play ();
			}
		} else {
			this.isFalling = false;
		}

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

	protected void Start() {
		this.gender = UnityEngine.Random.value <= 0.5f;
	}

    private void OnCollisionEnter(Collision collision) {
		if (collision.collider.gameObject.layer != LayerMask.NameToLayer ("Mountain")) {
			if (this.gender) {
				GetComponent<AudioSource>().clip = this.soundsMale[UnityEngine.Random.Range(0, this.soundsMale.Count)];
			} else {
				GetComponent<AudioSource>().clip = this.soundsFemale[UnityEngine.Random.Range(0, this.soundsFemale.Count)];
			}
			GetComponent<AudioSource> ().Play ();
		}

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
        Effects.instance.Do(Effects.EffectType.FireWorks, transform.position);
        Destroy(gameObject);
    }

    public void Kill(){
        Effects.instance.Do(Effects.EffectType.Explosion, transform.position);
        Destroy(gameObject);
    }
}
