using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxRenderer : MonoBehaviour {
    [Range(0, 1f)]
    public float speed = 0;
    public Vector2 tilling = Vector2.one;
    public Vector2 offset = Vector2.zero;

    private SpriteRenderer spriteRenderer;
    private Vector2 start_pos;
    private Vector2 parallaxOffset;

    private void Start() {
        start_pos = transform.position;
    }

    private void Update() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        var pb = new MaterialPropertyBlock();
        pb.SetTexture("_MainTex", spriteRenderer.sprite.texture);
        pb.SetVector("_MainTex_SO", new Vector4(tilling.x, tilling.y, offset.x + parallaxOffset.x, offset.y + parallaxOffset.y));
        spriteRenderer.SetPropertyBlock(pb);        
    }

    private void FixedUpdate() {
        parallaxOffset = Vector2.right * (Camera.main.transform.position.x - start_pos.x) * speed * Time.fixedDeltaTime;
    }
}
