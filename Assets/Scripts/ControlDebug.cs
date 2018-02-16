using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDebug : MonoBehaviour {
	void Update () {
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(kcode))
                Debug.Log("KeyCode down: " + kcode);
        }

        //print(Input.GetAxisRaw("RT"));
    }
}
