using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class Victim : MonoBehaviour {

	public List<AudioClip> soundsMale;	
	public List<AudioClip> soundsFemale;
	public AudioClip soundScreamMale;	
	public AudioClip soundScreamFemale;
	public AudioSource finishSound;
	public AudioSource explosionSound;
    
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Mountain mountain;
    [HideInInspector]
    public int index = 0;

    private bool hasEnded = false;

    private Vector3 collisionVector;
    private const string deadzoneTag = "Deadzone";
    private const string finishTag = "Finish";
	private bool gender;
	private bool isFalling;

    [Header("BIEM")]
    public GameObject gfx;
    public AnimationCurve biemCurve;
    public float biemCurveIntensity = 1f;

    public GameObject biemCanvas;
    public Text biemText;
    public float biemCanvasIntensity = 1f;
    public float biemShowCanvasTimeThreshold = 1f;

    [Header("Destroy")]
    public float destroyDuration = 1f;
    public float destroyDistanceThreshold = 1f;
    private float _currentDestroyTime;
    
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

        if(!GetComponent<Rigidbody>().isKinematic){
            // Find current and oldest position in Mountain space
            Tracker tracker = GetComponent<Tracker>();
            Vector3 oldestPosition = mountain.transform.InverseTransformPoint(tracker.GetOldestTrackedPosition());
            Vector3 currentPosition = mountain.transform.InverseTransformPoint(transform.position);

            // Check if moved forward more than the destroy distance threshold
            //float distance = Mathf.Abs(oldestPosition.z - currentPosition.z);
            float distance = Vector3.Distance(oldestPosition, currentPosition);
            if (distance < destroyDistanceThreshold) {
                _currentDestroyTime += Time.deltaTime;
                if (_currentDestroyTime >= destroyDuration) {
                    Kill();
                }
            } else {
                _currentDestroyTime = 0f;
            }
        }

        // Scale Gfx based on destroy time curve
        float biemEvaluate = biemCurve.Evaluate(_currentDestroyTime / destroyDuration);
        float biemCurveValue = hasEnded ? 1 : ((biemEvaluate - 1) * biemCurveIntensity + 1);
        float biemCanvasValue = hasEnded ? 1 : ((biemEvaluate - 1) * biemCanvasIntensity + 1);
        gfx.transform.localScale = Vector3.one * biemCurveValue;
        biemCanvas.gameObject.SetActive(!hasEnded && _currentDestroyTime > biemShowCanvasTimeThreshold * destroyDuration);
        biemText.transform.localScale = Vector3.one * biemCanvasValue;
        biemText.color = player.color;
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
				Rigidbody objectBody = collision.collider.gameObject.GetComponent<Rigidbody>();
				objectBody.AddForce (this.collisionVector.normalized * 500);
				objectBody.AddForce (Vector3.up * 1000);
				this.collisionVector = Vector3.zero;
			}
		}
	}

    public void Finish(){
        FindObjectOfType<Timetrail>().OnVictimFinished(this, player);
        player.score++;
		Effects.instance.Do(Effects.EffectType.FireWorks, transform.position);
		finishSound.Play();
		gfx.gameObject.SetActive(false);
		Destroy(gameObject, 2);
        hasEnded = true;
    }

    public void Kill(){
        if(!gfx.activeSelf){
            return;
        }
        FindObjectOfType<Timetrail>().OnVictimDied(player);

        hasEnded = true;
        Effects.instance.Do(Effects.EffectType.Explosion, transform.position);
        explosionSound.Play();
        gfx.gameObject.SetActive(false);
        Destroy(gameObject, 2);
    }
}
