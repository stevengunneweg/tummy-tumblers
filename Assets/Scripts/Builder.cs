﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Builder : MonoBehaviour {

	[SerializeField]
	private GameObject _minePrefab;
	[SerializeField]
	private GameObject _jumpPrefab;

	private bool _buildingAllowed = false;

    public Player player;
    public Mountain mountain;
    public float moveSpeed = 1f;
    public float radius = 3f;
    public float impact = 0.5f;
    public AnimationCurve impactCurve;
    public BaseStructure StructurePrefab;

    public BaseStructure tempPrefab;

    public void Reset() {
        tempPrefab = Instantiate(StructurePrefab);
        tempPrefab.SetEditMode(true);
    }

    protected void Update() {
        if (player == null)
            return;

        if(tempPrefab == null){
            return;
        }

        tempPrefab.transform.position = transform.position;

        string prefix = player.GetAxisPrefix();

        string xAxis = prefix + "_Xaxis";
        string yAxis = prefix + "_Yaxis";
        string arrowXAxis = prefix + "_ArrowXaxis";
        string arrowYAxis = prefix + "_ArrowYaxis";
        string aButton = prefix + "_Abutton";

        float xvalue = Input.GetAxis(xAxis);
        if(Input.GetAxis(arrowXAxis) >= 0.07f || Input.GetAxis(arrowXAxis) <= -0.07f)
            xvalue = Input.GetAxis(arrowXAxis);

        float yValue = Input.GetAxis(yAxis);
        if (Input.GetAxis(arrowYAxis) >= 0.07f || Input.GetAxis(arrowYAxis) <= -0.07f)
            yValue = -Input.GetAxis(arrowYAxis);

		if (xvalue > 0.02 || xvalue < -0.02) {
			if ((transform.position.x < 17 || xvalue > 0) && (transform.position.x > -17 || xvalue < 0)) {
				transform.position += transform.right * -xvalue * moveSpeed;
			}
		}
		if (yValue > 0.02 || yValue < -0.02) {
			if ((transform.position.z < 52.5f || yValue < 0) && (transform.position.z > 15 || yValue > 0)) {
				transform.position += transform.forward * yValue * moveSpeed;
			}
		}

        //player.index == 0 && 
        if ((Input.GetButtonDown (aButton) || (Input.GetKeyDown(KeyCode.Space))) && _buildingAllowed) {
            BuildStructure();
        }
    }


	protected void FixedUpdate() {
		if (player == null)
			return;

		if(tempPrefab == null){
			return;
		}

		BaseStructure[] structures = FindObjectsOfType<BaseStructure>();
		bool canBuild = true;
		for (int i = 0; i < structures.Count (); i++) {
			if (tempPrefab != structures [i]) {
				if (tempPrefab.GetComponent<Collider> ().bounds.Intersects (structures [i].GetComponent<Collider> ().bounds) && !structures[i].isEditing) {
					canBuild = false;
				}
			}
		}
		tempPrefab.SetBlocked (!canBuild);
		_buildingAllowed = canBuild;
	}

    public bool HasBuild(){
        return tempPrefab == null;
    }

    private void BuildStructure(){
        BaseStructure structure = Instantiate<BaseStructure> (StructurePrefab);
        structure.transform.position = transform.position;
        Destroy(tempPrefab.gameObject);
        tempPrefab = null;

        BuildModeOverlay overlay = FindObjectsOfType<BuildModeOverlay>().FirstOrDefault(b => b.Builder == this);
        if (overlay != null) {
            overlay.Hide();
        } else {
            Debug.Log("Cannot find overlay for the builder!", this);
        }
    }
}
