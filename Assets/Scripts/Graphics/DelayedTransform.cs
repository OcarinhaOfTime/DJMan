using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedTransform : MonoBehaviour {
    public Transform target;
    
	IEnumerator Start () {
        yield return new WaitForEndOfFrame();
        target.position = transform.position;
        target.rotation = transform.rotation;
        target.localScale = transform.localScale;
    }
}
