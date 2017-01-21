using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToParent : MonoBehaviour {

    private Vector3 lastPosition;
    public float speed = 1f;
    
    protected void Awake() {
        lastPosition = transform.parent.position;
    }

	protected void LateUpdate() {
        transform.position = Vector3.MoveTowards(lastPosition, transform.parent.position, speed * Time.deltaTime);
        lastPosition = transform.position;
    }
}
