using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainCaster : MonoBehaviour {

	[SerializeField]
	private AnimationCurve _increaseCurve;

	private Mountain _mountain;

	// Use this for initialization
	void Start () {
		this._mountain = FindObjectOfType<Mountain> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			UpdateTerrain(3);
    	}
		if (Input.GetMouseButtonDown(1)) {
			UpdateTerrain(-3);
		}
    }

	void UpdateTerrain(float amount) {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, 9999999999, LayerMask.GetMask("Mountain"))) {
			this._mountain.Increase (hit.point, amount, this._increaseCurve, 0.5f);
		}
	}
}
