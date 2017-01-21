using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {

    public Player player;
    public Mountain mountain;
    public float moveSpeed = 1f;
    public float radius = 3f;
    public float impact = 0.5f;
    public AnimationCurve impactCurve;

    public int numberOfPresses = 3;
    private int _currentNumberOfPresses = 0;

    public void Reset() {
        _currentNumberOfPresses = numberOfPresses;
    }

    protected void Update() {
        if (player == null)
            return;

        string prefix = player.GetAxisPrefix();

        string xAxis = prefix + "_Xaxis";
        string yAxis = prefix + "_Yaxis";
        string aButton = prefix + "_Abutton";
        string bButton = prefix + "_Bbutton";

        transform.position += transform.right * Input.GetAxis(xAxis);
        transform.position += transform.forward * Input.GetAxis(yAxis);

        if (_currentNumberOfPresses > 0) {
            if (Input.GetButtonDown(aButton)) {
                mountain.Increase(transform.position, radius, impactCurve, impact);
                _currentNumberOfPresses -= 1;
            } else if (Input.GetButtonDown(bButton)) {
                mountain.Increase(transform.position, radius, impactCurve, -impact);
                _currentNumberOfPresses -= 1;
            }
        }
    }
}
