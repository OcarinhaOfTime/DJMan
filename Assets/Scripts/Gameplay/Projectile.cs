using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable {
    public float speed = 20;
    public int poolIndex { get; set; }

    private Vector3 velocity;
    public float timer;
    private float time2destroy = 3;
    private bool launched;

    private void FixedUpdate() {
        var angle = -Vector2.SignedAngle(velocity.normalized, Vector2.right);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position += velocity * Time.fixedDeltaTime;
    }

    private void OnEnable() {
        timer = 0;
    }

    public void Launch(Vector2 dir) {
        velocity = dir * speed;

        var angle = -Vector2.SignedAngle(velocity.normalized, Vector2.right);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        launched = true;
    }

    [ContextMenu("Recycle")]
    void RecycleME() {
        ProjectilePool.instance.pool.RecyclePoolable(this);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        velocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
        print(name + "i do hit something " + collision.collider.name);
        RecycleME();
    }

    public void OnRecycle() {
        launched = false;
        //transform.position = Vector2.zero;
        //velocity = Vector2.zero;
    }

    private void Update() {
        if (!launched)
            return;

        timer += Time.deltaTime;
        if (timer > time2destroy)
            RecycleME();
    }
}
