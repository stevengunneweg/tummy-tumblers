using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour {

    public GameObject postPrefab;
    public GameObject plankPrefab;

    public Vector3 postOffset = Vector3.forward;
    public int numberOfPosts = 10;

    [ContextMenu("Generate Fence")]
    protected void Generate_EDITORONLY() {
        if (Application.isPlaying)
            return;

        foreach (Transform child in transform) {
            DestroyImmediate(child.gameObject);
        }

        Vector3? prevPosition = null;
        for (int i = 0; i < numberOfPosts; i++) {
            Vector3 position = transform.position + postOffset * i;
            Instantiate(postPrefab, position, Quaternion.identity, transform).name = postPrefab.name + " " + i.ToString("000");
            if (prevPosition.HasValue) {
                Vector3 centerPosition = (prevPosition.Value + position) / 2;
                Instantiate(plankPrefab, centerPosition, Quaternion.identity, transform).name = plankPrefab.name + " " + i.ToString("000");
            }
            prevPosition = position;
        }
    }

    protected void OnDrawGizmos() {
        Vector3? prevPosition = null;
        for (int i = 0; i < numberOfPosts; i++) {
            Vector3 position = transform.position + postOffset * i;
            Gizmos.DrawSphere(position, 0.1f);
            if (prevPosition != null) {
                Gizmos.DrawLine(position, prevPosition.Value);
            }
            prevPosition = position;
        }
    }
	
}
