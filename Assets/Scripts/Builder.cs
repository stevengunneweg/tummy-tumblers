using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {

    public Player player;
    public float speed = 1f;

    protected void Update() {
        if (player == null)
            return;

        string _prefix = player.GetAxisPrefix();

        string xAxis = player.GetAxisPrefix() + "_Xaxis";
        string yAxis = player.GetAxisPrefix() + "_Yaxis";
        string aButton = player.GetAxisPrefix() + "_Abutton";

        transform.position += transform.right * Input.GetAxis(xAxis);
        //transform.position += transform.forward * Input.GetAxis(yAxis);
        //transform.position += transform.up * Input.GetAxis(aButton);
    }
}
