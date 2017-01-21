using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    private Player _player;
    private Rigidbody _rigidBody;
    private string _prefix = "";

    public float speed = 15;
    
    protected void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        Victim victim = GetComponent<Victim>();
        if (victim != null)
        {
            _player = victim.player;

            switch (_player.index)
            {
                case 0: _prefix = "P1"; break;
                case 1: _prefix = "P2"; break;
                case 2: _prefix = "P3"; break;
                case 3: _prefix = "P4"; break;
                default: _prefix = "P1"; break;
            }
        }
    }
    
    protected void Update()
    {
        if (_rigidBody != null)
        {
            if (Input.GetAxis(_prefix + "_Xaxis") > 0.05)
                _rigidBody.AddForce((Vector3.right * speed)*((Vector3.right * speed).x- _rigidBody.velocity.x));
            if (Input.GetAxis(_prefix + "_Xaxis") < -0.05)
                _rigidBody.AddForce((-Vector3.right * speed) * (_rigidBody.velocity.x- (-Vector3.right * speed).x));
        }
    }
}
