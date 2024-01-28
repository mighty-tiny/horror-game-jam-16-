using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour
{
    public GameObject beeobj;
    //public GameObject[] soldiers;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bee") )
        {
            beeobj.SetActive(true);

        }
    }
}
