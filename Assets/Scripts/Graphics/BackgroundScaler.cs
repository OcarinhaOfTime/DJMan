using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour {
    public Vector3 offset;
    public bool vertical;
    public float x_mul = 1;

    private void Start() {
        Fit();
    }

    private void Reset() {
        Fit();
    }

    private void OnEnable() {
        Fit();
    }

    public void Fit() {
        if (vertical) {
            FitVertical();
        } else {
            FitHorizontal();
        }
    }

    [ContextMenu("Fit Horizontal")]
    public void FitHorizontal() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        var world_size = sr.sprite.rect.size / sr.sprite.pixelsPerUnit;
        var screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        var width = screenWidth / world_size.x * 2f;

        var delta = world_size.y * width - Camera.main.orthographicSize * 2;
        transform.localScale = Vector3.one * width;
        transform.position = Vector3.up * delta * .5f + Vector3.forward * transform.position.z + offset;
    }

    [ContextMenu("Fit Vertical")]
    public void FitVertical() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        var world_size = sr.sprite.rect.size / sr.sprite.pixelsPerUnit;
        var height = Camera.main.orthographicSize / world_size.y * 2f;

        var delta = world_size.y * height - Camera.main.orthographicSize * 2;
        var scale = Vector3.one * height;
        scale.x *= x_mul;
        transform.localScale = scale;
        transform.position = Vector3.up * delta * .5f + Vector3.forward * transform.position.z + offset;
    }
}
