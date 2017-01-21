using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStructure : BaseStructure {

    [SerializeField]
    private GameObject visualGameObject;

    [SerializeField]
    private ParticleSystem shockParticles;

    public override void OnCollisionWithVictim(Collision collision, Victim victim) {
        victim.GetComponent<Rigidbody>().AddForce(visualGameObject.transform.up * 8, ForceMode.VelocityChange);

        shockParticles.Play();
    }
    
}
