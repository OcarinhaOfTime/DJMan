using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour {
    public int poolSize = 100;
    public GenericPool<Projectile> pool = new GenericPool<Projectile>();
    public static ProjectilePool instance;
    public Projectile prefab;
    private int usedItems;

    void Awake() {
        instance = this;
        pool.Setup(poolSize, prefab);
    }

    private void Update() {
        usedItems = pool.used;
    }
}
