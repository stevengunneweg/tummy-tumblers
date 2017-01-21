using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bowlingable : MonoBehaviour {

    protected void OnCollisionEnter(Collision collision) {
        if (collision.transform.GetComponent<Victim>() != null) {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(collision.relativeVelocity, ForceMode.Impulse);
            gameObject.layer = LayerMask.NameToLayer("NonCollidingProp");
        }
    }
}
