using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainMouseEditor : MonoBehaviour {

	public AnimationCurve curve;
    public float radius = 3f;
    public float amount = 0.5f;

	private Mountain _mountain;
    
	void Start () {
		this._mountain = FindObjectOfType<Mountain> ();
	}
    
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

		if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Mountain"))) {
			this._mountain.Increase (hit.point, radius, curve, amount);
		}
	}
}
