using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gathering : MonoBehaviour
{
    public bool bush;
    public GameObject particle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            if (bush)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
            }
        }
    }
}
