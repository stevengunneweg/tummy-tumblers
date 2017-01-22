using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceAboveParent : MonoBehaviour {

    public float scale = 0.1f;
    public float height = 1f;

	public void LateUpdate() {
        if (transform.parent == null)
            return;
        
        transform.position = transform.parent.position + Vector3.up * height;
        Transform lookat = Camera.main.transform;
        if (lookat != null) {
            transform.LookAt(lookat);
            transform.localScale = (lookat.position - transform.position).magnitude * scale * Vector3.one;
        }
    }
}
