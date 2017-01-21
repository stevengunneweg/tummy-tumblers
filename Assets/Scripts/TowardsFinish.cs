using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLinq;

public class TowardsFinish : MonoBehaviour {

    public Transform finish;
    public Transform victimParent;
    public float distance = 4f;

    protected void Update() {
        if (victimParent.childCount == 0)
            return;

        Vector3 average = victimParent.GetComponentsInChildren<Transform>().Skip(1).Average(t => t.position);
        transform.position = Vector3.MoveTowards(average, finish.position, distance);
    }

    protected void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
	
}
