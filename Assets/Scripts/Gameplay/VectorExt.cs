using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExt {
    public static Vector2 Rotate(this Vector2 vec, float radians) {
        var x = vec.x * Mathf.Cos(radians) - vec.y * Mathf.Sin(radians);
        var y = vec.x * Mathf.Sin(radians) + vec.y * Mathf.Cos(radians);
        return new Vector2(x, y);
    }

    public static Vector2 RotateDeg(this Vector2 vec, float deg) {
        return Quaternion.Euler(0, 0, deg) * vec;
    }

    public static Vector2 AddNoise(this Vector2 vec, float noise) {
        return Quaternion.Euler(Vector3.forward * Random.Range(-noise, noise)) * vec;
    }
}
