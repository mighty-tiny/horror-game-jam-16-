using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour
{
    public GameObject beeobj;
    public Transform bucket;
    //public GameObject[] soldiers;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bee"))
        {
            beeobj.SetActive(true);
            var i = ((float)bucket.position.y);
            do
            {
                i -= 0.5f;
            } while (i < 0);
        }
    }
}
