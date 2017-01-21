using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLinq;

public class CameraController : MonoBehaviour {

    public Camera cam;
    private Transform[] focus;
    public float alignOffset = 0.1f;
    public float alignSpeed = 5f;
    public float growSpeed = 5f;

    protected void Update() {
        // Calculate Min and Max values
        float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;
        foreach (var t in focus.Where(t => t != null)) {
            Vector3 viewportPoint_3 = cam.WorldToViewportPoint(t.position);
            Vector2 viewportPoint = new Vector2(viewportPoint_3.x, viewportPoint_3.y);

            minX = Mathf.Min(minX, viewportPoint.x);
            maxX = Mathf.Max(maxX, viewportPoint.x);
            minY = Mathf.Min(minY, viewportPoint.y);
            maxY = Mathf.Max(maxY, viewportPoint.y);
        }

        // Calculate diff, minDiff and center values
        float diffX = Mathf.Abs(minX - maxX);
        float diffY = Mathf.Abs(minY - maxY);
        float minDiff = Mathf.Min(diffX, diffY);
        float maxDiff = Mathf.Max(diffX, diffY);
        float centerX = (minX + maxX) / 2;
        float centerY = (minY + maxY) / 2;

        // Align X
        if (centerX > 0.5f + alignOffset) {
            transform.position += transform.right * Time.deltaTime * alignSpeed;
        } else if (centerX < 0.5f - alignOffset) {
            transform.position -= transform.right * Time.deltaTime * alignSpeed;
        }

        // Align Y
        if (centerY > 0.5f + alignOffset) {
            transform.position += transform.up * Time.deltaTime * alignSpeed;
        } else if (centerY < 0.5f - alignOffset) {
            transform.position -= transform.up * Time.deltaTime * alignSpeed;
        }

        // Grow
        if (maxDiff > 0.4f) {
            transform.position -= transform.forward * growSpeed * Time.deltaTime;
        } else if (minDiff < 0.2f) {
            transform.position += transform.forward * growSpeed * Time.deltaTime;
        }
    }

    public void Focus(Transform[] focus) {
        this.focus = focus;
    }
}
