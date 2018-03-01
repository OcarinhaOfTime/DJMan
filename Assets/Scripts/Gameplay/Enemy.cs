using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int life = 1;

    public Transform left;
    public Transform right;

    public float speed = 1;

    public bool facingRight {
        get {
            return transform.localScale.x > 0;
        }
        set {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (value ? 1 : -1), transform.localScale.y, transform.localScale.z);
        }
    }

    public float facingDir {
        get {
            return facingRight ? 1 : -1;
        }
    }

    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            GameManager.instance.Die();
        }
    }

    public void TakeDamage() {
        if(--life <= 0) {
            Die();
        } else {
            this.LerpRoutine(.5f, (t) => {
                var t2 = Mathf.PingPong(2 * t, 1);
                var t3 = Mathf.SmoothStep(0, 1, t2);
                spriteRenderer.color = Color.Lerp(Color.white, Color.red, t3);
            });
        }
    }

    void Die() {
        GameManager.instance.EnemyDeath();
        Destroy(gameObject);
    }

    public void Update() {
        if((transform.position.x > right.position.x && facingRight) || (transform.position.x < left.position.x && !facingRight)) {
            facingRight = !facingRight;
        }


        transform.position += Vector3.right * Time.deltaTime * speed * facingDir;
    }

    public void SetLevel(int level) {
        speed += (int)Mathf.Min(5, .2f * level);
        life += (int)Mathf.Min(5, .2f * level);
    }
}
