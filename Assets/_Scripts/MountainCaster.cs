using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainCaster : MonoBehaviour {

	[SerializeField]
	private AnimationCurve _increaseCurve;

    [SerializeField]
    public float radius = 3f;
    [SerializeField]
    public float amount = 0.5f;

	private Mountain _mountain;

	// Use this for initialization
	void Start () {
		this._mountain = FindObjectOfType<Mountain> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			UpdateTerrain(amount);
    	}
		if (Input.GetMouseButtonDown(1)) {
			UpdateTerrain(-amount);
		}
    }

	void UpdateTerrain(float amount) {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, 9999999999, LayerMask.GetMask("Mountain"))) {
			this._mountain.Increase (hit.point, radius, this._increaseCurve, amount);
		}
	}
}
