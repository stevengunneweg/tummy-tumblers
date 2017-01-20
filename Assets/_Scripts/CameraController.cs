using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLinq;

[RequireComponent(typeof(Camera)), ExecuteInEditMode()]
public class CameraController : MonoBehaviour {

    public enum CameraType {
        MOUNTAIN_OVERVIEW,
        BALLS_OVERVIEW
    }

    [Header("References")]
    public Transform mountainOverview;
    public Transform[] balls;
    public CameraType cameraType;

    [Header("Camera Position")]
    public Vector3 cameraDirection;
    public float moveSpeed = 1f;
    public float cameraDistance = 10f;

    [Header("Blobbing")]
    public AnimationCurve blobCurve;
    public float blobStrength = 1f;
    public float blobTime = 0f;
    public float blobDuration = 1f;

    public new Camera camera {
        get {
            return GetComponent<Camera>();
        }
    }

    public void SetToOverview() {
        cameraType = CameraType.MOUNTAIN_OVERVIEW;
    }

    public void SetBallTransforms(Transform[] balls) {
        cameraType = CameraType.BALLS_OVERVIEW;
        this.balls = balls;
    }

    protected void Update() {
        float blob = 0f;
        if (Application.isPlaying) {
            blobTime += Time.deltaTime;
        }
        blob = blobCurve.Evaluate(blobTime / blobDuration) * blobStrength;

        Transform[] transforms;
        if (cameraType == CameraType.MOUNTAIN_OVERVIEW) {
            if (mountainOverview == null) {

            }
            transforms = mountainOverview.GetComponentsInChildren<Transform>().ToArray();
        } else {
            transforms = balls.Where(ball => ball != null).ToArray();
        }
        Vector3 perfectPosition = GetPerfectPosition(transforms);
        perfectPosition.x += blob;
        MoveTo(perfectPosition);

        transform.LookAt(transforms.Average(t => t.position));
    }

    public Vector3 GetPerfectPosition(Transform[] transforms) {
        return camera.ScreenToWorldPoint(transforms.Average(t => camera.WorldToScreenPoint(t.position))) + cameraDirection * cameraDistance;
    }

    public void MoveTo(Vector3 perfectPosition) {
        if (Application.isPlaying) {
            transform.position = Vector3.Lerp(transform.position, perfectPosition, Time.deltaTime * moveSpeed);
        } else {
            transform.position = perfectPosition;
        }
        
    }
}
