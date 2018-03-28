using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewProperties : MonoBehaviour {

    public float topDist;
    public float botDist;

    GameObject glow;

	void Awake () {
        glow = this.transform.GetChild(1).gameObject;
        topDist = this.transform.position.y;
        botDist = this.transform.position.y - 0.04f;
	}

    private void Update()
    {
        if(this.transform.position.y <= botDist)
        {
            glow.SetActive(false);
        }
        else
        {
            glow.SetActive(true);
        }
    }

}
