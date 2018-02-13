using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {
    public float fireRate = .25f;
    private Animator anim;
    public Transform cannonTip;
    private GenericPool<Projectile> pool;
    private PlayerController playerPhy;
    private AudioSource audioSource;

    private float timer = 666;


    private void Start() {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        pool = ProjectilePool.instance.pool;
        playerPhy = GetComponent<PlayerController>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && timer > fireRate) {
            timer = 0;
            var proj = pool.GetPoolable();
            proj.transform.position = cannonTip.position;
            proj.gameObject.SetActive(true);
            var dir = Vector2.right * playerPhy.facingSign;
            proj.Launch(dir.normalized);
            audioSource.Play();
        }

        timer += Time.deltaTime;
    }
}
