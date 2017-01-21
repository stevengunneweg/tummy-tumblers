using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AlignToMountain : MonoBehaviour {

    public bool alignRotation = false;
    public Color debugColor = Color.red;
    public Vector3? lastHit;

    protected void Update() {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position + Vector3.up * 99999, Vector3.down), out hit, float.PositiveInfinity, LayerMask.GetMask("Mountain"))) {
            lastHit = hit.point;
            if (alignRotation) {
                transform.up = hit.normal;
            }
            transform.position = hit.point;
        } else {
            lastHit = null;
        }
    }

    public void OnDrawGizmos() {
        if (lastHit != null) { 
            Gizmos.color = debugColor;
            Gizmos.DrawSphere(lastHit.Value, 0.1f);
        } else {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }

}
