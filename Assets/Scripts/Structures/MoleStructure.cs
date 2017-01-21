using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleStructure : BaseStructure {

    [SerializeField]
    private GameObject particlesGameObject;

    [SerializeField]
    private GameObject visualGameObject;

    public override void OnCollisionWithVictim(Collision collision, Victim victim) {
        FindObjectOfType<Mountain>().Increase(transform.position, 3, AnimationCurve.EaseInOut(0, 1, 0, 1), 0.25f);

        particlesGameObject.SetActive(true);
        visualGameObject.SetActive(false);

        victim.GetComponent<Rigidbody>().AddForce(collision.impulse * 2, ForceMode.VelocityChange);

        Destroy(gameObject, 1.5f);
        Destroy(victim.gameObject, 1f);
    }
}
