using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour
{
    public GameObject beeobj;
    public GameObject Bucket;
    [Header("Particles")]
    public GameObject ParticleBlow;
    //float f;
    //public GameObject[] soldiers;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bee") )
        {
            //f = ((float)Bucket.position.y);
            Instantiate(ParticleBlow, transform.position, Quaternion.identity);
            Bucket.SetActive(false);
            //do
            //{
            //    f -= 0.1f;
            //} while (Bucket.position.y < 0);
        }
    }
}
