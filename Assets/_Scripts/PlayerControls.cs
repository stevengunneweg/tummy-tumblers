using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    private int _playerIndex = 0;
    private Rigidbody _rigidBody;
    public float speed =15;
	// Use this for initialization
	void Start () {
        if (GetComponent<Victim>()!=null)
        {
            //_playerIndex = this.GetComponent<Victim>().playerIndex;
        }
        if (GetComponent<Rigidbody>() != null)
        {
            _rigidBody = GetComponent<Rigidbody>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (_rigidBody != null)
        {
            if (Input.GetAxis("P1_Xaxis") > 0.05)
                _rigidBody.AddForce( Vector3.right* speed);
            if (Input.GetAxis("P1_Xaxis") < -0.05)
                _rigidBody.AddForce(-Vector3.right* speed);
        }
    }
}
