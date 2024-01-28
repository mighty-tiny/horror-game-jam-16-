using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    bool towered;
    public GameObject text;
    //public GameObject[] soldiers;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            towered = true;

        }
        else
        {
            towered = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            towered = false;
        }
    }
    private void Update()
    {
        if (towered)
        {
            text.SetActive(true);
            towered = false;
        }
    }
}
