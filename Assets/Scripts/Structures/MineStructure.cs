﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineStructure : BaseStructure {

    [SerializeField]
    private GameObject particlesGameObject;

    [SerializeField]
    private GameObject visualGameObject;

    public override void OnCollisionWithVictim(Collision collision, Victim victim){
		GetComponent<AudioSource> ().Play ();

        FindObjectOfType<Mountain>().Increase(transform.position, 3, AnimationCurve.EaseInOut(0, 1, 0, 1), -0.6f);

        particlesGameObject.SetActive(true);
        visualGameObject.SetActive(false);

        Vector3 shock = (collision.impulse * 2).normalized * 15f;
        victim.GetComponent<Rigidbody>().AddForce(shock, ForceMode.VelocityChange);
        GetComponent<SphereCollider>().enabled = false;

        Destroy(gameObject, 1.5f);
        Destroy(victim.gameObject, 1f);
    }
}
