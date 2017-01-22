using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {
    float timer = 2;
	// Use this for initialization
	void Start () {
        timer = 2;
    }
	
	// Update is called once per frame
	void Update () {
        if (timer <= 0)
        {
            if (transform.position.y > -1000)
                transform.position += -Vector3.up * 15;
        }
        else
            timer -= Time.deltaTime;

    }
}
