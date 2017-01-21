using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineStructure : BaseStructure {

    [SerializeField]
    private GameObject particlesGameObject;

    [SerializeField]
    private GameObject visualGameObject;

    public override void OnCollisionWithVictim(Collision collision, Victim victim){
        particlesGameObject.SetActive(true);
        visualGameObject.SetActive(false);

        victim.GetComponent<Rigidbody>().AddForce(collision.impulse * 2, ForceMode.VelocityChange);

        Destroy(gameObject, 1.5f);
        Destroy(victim.gameObject, 1f);
    }
}
