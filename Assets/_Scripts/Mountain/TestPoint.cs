using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPoint : MonoBehaviour {

    public AnimationCurve curve;
    public float radius = 1f;
    public float increaseAmount = 1f;
    public Mountain mountain;
    public Vector3? lastHit;

    [ContextMenu("Apply to Mountain")]
    public void ApplyToMountain() {
        if (lastHit == null)
            return;
        
        mountain.Increase(lastHit.Value, radius, curve, increaseAmount);
    }

	public void OnDrawGizmos() {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position + Vector3.up * 99999, Vector3.down), out hit, float.PositiveInfinity, LayerMask.GetMask("Mountain"))) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hit.point, radius);
            Gizmos.DrawSphere(hit.point, radius / 10f);
            lastHit = hit.point;
            transform.position = hit.point;
        } else 
            lastHit = null;
    }
}
