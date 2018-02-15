using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowerHorizontal : MonoBehaviour {
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate() {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10));
        var pos = targetPosition;
        pos.y = 0;
        transform.position = pos;
    }
}
