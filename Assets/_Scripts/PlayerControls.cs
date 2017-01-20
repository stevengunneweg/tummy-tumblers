using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    private int _playerIndex = 0;
    private Rigidbody _rigidBody;
    public float speed =15;
    private string _prefix = "";
    // Use this for initialization
    void Start () {
        if (GetComponent<Victim>()!=null)
        {
            _playerIndex = this.GetComponent<Victim>().Player.Index;
            switch (_playerIndex)
            {
                case 0: _prefix = "P1"; break;
                case 1: _prefix = "P2"; break;
                case 2: _prefix = "P3"; break;
                case 3: _prefix = "P4"; break;
                default: _prefix = "P1"; break;
            }
        }
        if (GetComponent<Rigidbody>() != null)
        {
            _rigidBody = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
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
