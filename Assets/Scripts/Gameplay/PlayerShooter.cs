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
    public Transform arrow;
    //public Transform arm;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        pool = ProjectilePool.instance.pool;
        playerPhy = GetComponent<PlayerController>();
    }

    private void Update() {
        Vector2 dir = Vector2.right * playerPhy.facingSign;
        Vector2 aimDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        var angle = Vector2.SignedAngle(Vector2.right, dir);
        if (aimDir.magnitude > .1f) {
            angle = Vector2.SignedAngle(Vector2.right, aimDir);
            //angle = Mathf.Repeat(angle, 360);
            angle = (int)(angle / 45);
            angle = angle * 45;
        }

        if (Input.GetButtonDown("Fire1") && timer > fireRate) {
            timer = 0;
            var proj = pool.GetPoolable();
            proj.transform.position = cannonTip.position;
            proj.gameObject.SetActive(true);
            Vector2 projDir = Quaternion.Euler(0, 0, angle) * Vector2.right;
            proj.Launch(projDir + Vector2.Scale(playerPhy.deltaVelocity, Vector2.right));
            audioSource.Play();
        }

        arrow.rotation = Quaternion.Euler(0, 0, angle);
        arrow.localScale = new Vector3(playerPhy.facingSign, 1, 1);

        timer += Time.deltaTime;
    }

    private void LateUpdate() {
        

        

        

        
        //arm.localScale = new Vector3(playerPhy.facingSign, playerPhy.facingSign, 1);
        //if(playerPhy.facingSign < 0) {
        //    arm.rotation = Quaternion.Euler(0, 0, angle + 66 - 135);
        //} else {
        //    arm.rotation = Quaternion.Euler(0, 0, angle + 66);
        //}
        
    }
}
