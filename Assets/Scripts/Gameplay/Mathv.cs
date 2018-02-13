using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mathv {
    public static float Sign(float x) {
        if (x > .01f)
            return 1;

        if (x < -.01f)
            return -1;

        return 0;
    }

    public static float LinearDist(float a, float b) {
        var delta = a - b;
        return delta < 0 ? Mathf.Infinity : delta;
    }

    public static bool IsNear(float a, float b, float omega = .1f) {
        return Mathf.Abs(a - b) < omega;
    }
}
