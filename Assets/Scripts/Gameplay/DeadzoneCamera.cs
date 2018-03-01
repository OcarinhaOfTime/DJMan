using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Camera))]
public class DeadzoneCamera : MonoBehaviour {
    //public float smoothTime = 0.3F;
    public float speed = 1;
    public Transform target;
    //public Rect deadzone;

    public Rect[] limits;

    protected Camera _camera;
    protected Vector3 _currentVelocity;
    private Vector3 smoothPos;

    public void Start() {
        smoothPos = target.position;
        smoothPos.z = transform.position.z;
        _currentVelocity = Vector3.zero;

        _camera = GetComponent<Camera>();
        if (!_camera.orthographic) {
            Debug.LogError("deadzone script require an orthographic camera!");
            Destroy(this);
        }
    }

    public void LateUpdate() {
        float localX = target.position.x - transform.position.x;
        float localY = target.position.y - transform.position.y;
        smoothPos += new Vector3(localX, localY, 0);

        //if (localX < deadzone.xMin) {
        //    smoothPos.x += localX - deadzone.xMin;
        //} else if (localX > deadzone.xMax) {
        //    smoothPos.x += localX - deadzone.xMax;
        //}

        //if (localY < deadzone.yMin) {
        //    smoothPos.y += localY - deadzone.yMin;
        //} else if (localY > deadzone.yMax) {
        //    smoothPos.y += localY - deadzone.yMax;
        //}

        Rect camWorldRect = new Rect();
        camWorldRect.min = new Vector2(smoothPos.x - _camera.aspect * _camera.orthographicSize, smoothPos.y - _camera.orthographicSize);
        camWorldRect.max = new Vector2(smoothPos.x + _camera.aspect * _camera.orthographicSize, smoothPos.y + _camera.orthographicSize);

        for (int i = 0; i < limits.Length; ++i) {
            if (limits[i].Contains(target.position)) {
                Vector3 localOffsetMin = limits[i].min + camWorldRect.size * 0.5f;
                Vector3 localOffsetMax = limits[i].max - camWorldRect.size * 0.5f;

                localOffsetMin.z = localOffsetMax.z = smoothPos.z;

                smoothPos = Vector3.Max(smoothPos, localOffsetMin);
                smoothPos = Vector3.Min(smoothPos, localOffsetMax);

                break;
            }
        }

        transform.position = smoothPos;
        //transform.position = Vector3.Lerp(transform.position, smoothPos, Time.deltaTime * speed);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(DeadzoneCamera))]
public class DeadZonEditor : Editor {
    public void OnSceneGUI() {
        DeadzoneCamera cam = target as DeadzoneCamera;

        //Vector3[] vert =
        //{
        //    cam.transform.position + new Vector3(cam.deadzone.xMin, cam.deadzone.yMin, 0),
        //    cam.transform.position + new Vector3(cam.deadzone.xMax, cam.deadzone.yMin, 0),
        //    cam.transform.position + new Vector3(cam.deadzone.xMax, cam.deadzone.yMax, 0),
        //    cam.transform.position + new Vector3(cam.deadzone.xMin, cam.deadzone.yMax, 0)
        //};

        Color transp = new Color(0, 0, 0, 0);
        //Handles.DrawSolidRectangleWithOutline(vert, transp, Color.red);

        for (int i = 0; i < cam.limits.Length; ++i) {
            Vector3[] vertLimit =
           {
                new Vector3(cam.limits[i].xMin, cam.limits[i].yMin, 0),
                new Vector3(cam.limits[i].xMax, cam.limits[i].yMin, 0),
                new Vector3(cam.limits[i].xMax, cam.limits[i].yMax, 0),
                new Vector3(cam.limits[i].xMin, cam.limits[i].yMax, 0)
            };

            Handles.DrawSolidRectangleWithOutline(vertLimit, transp, Color.green);
        }
    }
}
#endif