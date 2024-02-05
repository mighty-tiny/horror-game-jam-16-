using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gathering : MonoBehaviour
{
    public bool bush;
    public bool pot;
    public GameObject particle;
    public GameObject handObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            if (bush)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
            }
            else if (pot)
            {
                handObj.SetActive(true);
                Instantiate(particle, transform.position, Quaternion.identity);
            }
        }
    }
}
