using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bowlingable : MonoBehaviour {

    public float forceStrength = 1f;

    protected void OnCollisionEnter(Collision collision) {
        if (collision.transform.GetComponent<Victim>() != null) {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForceAtPosition(collision.relativeVelocity * forceStrength, collision.contacts[0].point, ForceMode.Impulse);
            gameObject.layer = LayerMask.NameToLayer("NonCollidingProp");

            AlignToMountain atm = GetComponent<AlignToMountain>();
            if (atm != null) {
                atm.enabled = false;
            }
        }
    }
}
