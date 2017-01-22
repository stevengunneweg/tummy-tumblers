using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStructure : BaseStructure {

    [SerializeField]
    private int uses = 3;

    [SerializeField]
    private GameObject visualGameObject;

    [SerializeField]
    private ParticleSystem shockParticles;

	public override void OnCollisionWithVictim(Collision collision, Victim victim) {
		GetComponent<AudioSource> ().Play ();

        victim.GetComponent<Rigidbody>().AddForce(visualGameObject.transform.forward * 8, ForceMode.VelocityChange);

        shockParticles.Play();
        uses--;
        if (uses <= 0)
            Destroy(gameObject, 0.5f);

    }
    
}
