using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {

	[SerializeField]
	private GameObject _minePrefab;
	[SerializeField]
	private GameObject _jumpPrefab;

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
        string arrowXAxis = prefix + "_ArrowXaxis";
        string arrowYAxis = prefix + "_ArrowYaxis";
        string aButton = prefix + "_Abutton";
        string bButton = prefix + "_Bbutton";

        float xvalue = Input.GetAxis(xAxis);
        if(Input.GetAxis(arrowXAxis) >= 0.07f || Input.GetAxis(arrowXAxis) <= -0.07f)
            xvalue = Input.GetAxis(arrowXAxis);

        float yValue = Input.GetAxis(yAxis);
        if (Input.GetAxis(arrowYAxis) >= 0.07f || Input.GetAxis(arrowYAxis) <= -0.07f)
            yValue = -Input.GetAxis(arrowYAxis);
        
        transform.position += transform.right * -xvalue;
        transform.position += transform.forward * yValue;

		if (_currentNumberOfPresses > 0) {
			if (Input.GetButtonDown (aButton)) {
				GameObject mine = Instantiate<GameObject> (_minePrefab);
				mine.transform.position = transform.position;
				_currentNumberOfPresses--;
			} else if (Input.GetButtonDown (bButton)) {
				GameObject jump = Instantiate<GameObject> (_jumpPrefab);
				jump.transform.position = transform.position;
				_currentNumberOfPresses--;
			}
		}

        /*if (_currentNumberOfPresses > 0) {
            if (Input.GetButtonDown(aButton)) {
                mountain.Increase(transform.position, radius, impactCurve, impact);
                _currentNumberOfPresses -= 1;
            } else if (Input.GetButtonDown(bButton)) {
                mountain.Increase(transform.position, radius, impactCurve, -impact);
                _currentNumberOfPresses -= 1;
            }
        }*/
    }
}
