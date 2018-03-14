using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {



	void Start () {
		
	}



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "item" || other.gameObject.tag == "tool")
        {
            Destroy(other.gameObject);
        }
    }
}
