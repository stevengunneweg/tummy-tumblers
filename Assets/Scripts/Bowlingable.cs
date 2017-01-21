using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bowlingable : MonoBehaviour {

    protected void OnCollisionEnter(Collision collision) {
        if (collision.transform.GetComponent<Victim>() != null)
            GetComponent<Rigidbody>().isKinematic = false;
    }
}
