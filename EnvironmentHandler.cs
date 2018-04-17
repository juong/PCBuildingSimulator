using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour {

    public bool fan;
	
	void Update () {
        if (fan)
        {
            transform.Rotate(0, 0, 100*Time.deltaTime);
        }
	}
}
