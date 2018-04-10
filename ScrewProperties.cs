using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewProperties : MonoBehaviour {

    public int index;
    public bool fastened;
    public bool maxHeight;
    GameObject targ;
    GameObject glow;
    GameObject origin;

    void Awake () {
        glow = this.transform.GetChild(1).gameObject;
        targ = transform.parent.transform.GetChild(index).gameObject;
        origin = transform.parent.transform.GetChild(index + 1).gameObject;
    }

    private void Update()
    {
        fastened = Vector3.Distance(glow.transform.position, targ.transform.position) < 0.01f;
        maxHeight = Vector3.Distance(glow.transform.position, origin.transform.position) < 0.01f;
        glow.SetActive(!fastened);
    }

}
