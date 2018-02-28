using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour {
    public Graphic pressAnyKey;
    public float blinkSpeed = 1;

    private void Update() {
        var c = pressAnyKey.color;
        var t = Mathf.PingPong(Time.time * blinkSpeed, 1);
        c.a = Mathf.SmoothStep(0, 1, t);
        pressAnyKey.color = c;

        if (Input.anyKeyDown) {
            SceneManager.LoadScene(1);
        }
    }
}
