using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPool <T> where T : MonoBehaviour, IPoolable {
    T prefab;
    T[] pool;

    private int objPtr = 0;
    private int readyObjs = 0;
    public int used { get { return pool.Length - readyObjs; } }

    public void Setup(int poolSize, T prefab) {
        pool = new T[poolSize];
        this.prefab = prefab;

        for(int i = 0; i < poolSize; i++) {
            pool[i] = GameObject.Instantiate(prefab);
            pool[i].poolIndex = i;
        }

        readyObjs = poolSize;
    }

    public T GetPoolable(bool activate = false) {
        if(readyObjs == 0) {
            ExpandPool();
        }

        var p = pool[objPtr];
        objPtr = (objPtr + 1) % pool.Length;
        readyObjs--;
        p.gameObject.SetActive(activate);
        return p;
    }

    public void RecyclePoolable(T p) {
        p.OnRecycle();
        p.gameObject.SetActive(false);
        int newIndex = (objPtr + readyObjs) % pool.Length;
        Swap(newIndex, p.poolIndex);
        readyObjs++;
    }

    void Swap(int indexA, int indexB) {
        var temp = pool[indexA];
        pool[indexA] = pool[indexB];
        pool[indexB] = temp;

        pool[indexA].poolIndex = indexB;
        pool[indexB].poolIndex = indexA;
    }

    void ExpandPool() {
        T[] newPool = new T[pool.Length + 1];
        pool.CopyTo(newPool, 0);
        pool = newPool;
        objPtr = newPool.Length - 1;
        newPool[objPtr] = GameObject.Instantiate(prefab);
        newPool[objPtr].poolIndex = objPtr;
        readyObjs++;
    }
}
