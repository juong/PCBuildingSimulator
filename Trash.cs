using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (((other.gameObject.tag == "item" && !other.name.StartsWith("Case") && !other.name.Contains("notepad")) || other.gameObject.tag == "tool" ) && other.transform.parent.name != "Hand")
        {
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (((other.gameObject.tag == "item" && !other.name.StartsWith("Case") && !other.name.Contains("notepad")) || other.gameObject.tag == "tool") && other.transform.parent.name != "Hand")
        {
            Destroy(other.gameObject);
        }
    }
}
