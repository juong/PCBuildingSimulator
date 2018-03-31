using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewProperties : MonoBehaviour {

    public float topDist;
    public float botDist;

    GameObject glow;

    public bool sideways;

	void Awake () {
        glow = this.transform.GetChild(1).gameObject;
        topDist = this.transform.position.y;
        botDist = this.transform.position.y - 0.04f;
        if (sideways)
        {
            topDist = this.transform.localPosition.z;
            botDist = this.transform.localPosition.z - 0.002f;
        }
	}

    private void Update()
    {
        if (!sideways)
        {
            if (this.transform.position.y <= botDist)
            {
                glow.SetActive(false);
            }
            else
            {
                glow.SetActive(true);
            }
        }
        else
        {
            if (this.transform.localPosition.z <= botDist)
            {
                glow.SetActive(false);
            }
            else
            {
                glow.SetActive(true);
            }
        }
    }

}
