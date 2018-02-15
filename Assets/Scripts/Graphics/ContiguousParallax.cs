using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParallaxRenderer))]
public class ContiguousParallax : MonoBehaviour {
    public float speed = 1;
    private ParallaxRenderer pr;

    private void Start() {
        pr = GetComponent<ParallaxRenderer>();
    }

    private void Update() {
        pr.offset += Vector2.right * speed * Time.deltaTime;
    }
}
