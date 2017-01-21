using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    private Player _player;
    private Rigidbody _rigidBody;

    public float speed = 15;
    
    protected void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        _player = GetComponent<Victim>().player;
    }
    
    protected void Update()
    {
        if (_rigidBody != null)
        {
            string _prefix = _player.GetAxisPrefix();
            if (Input.GetAxis(_prefix + "_Xaxis") > 0.05)
                _rigidBody.AddForce((Vector3.left * speed)*((Vector3.left * speed).x- _rigidBody.velocity.x));
            if (Input.GetAxis(_prefix + "_Xaxis") < -0.05)
                _rigidBody.AddForce((-Vector3.left * speed) * (_rigidBody.velocity.x- (-Vector3.left * speed).x));
        }
    }
}
