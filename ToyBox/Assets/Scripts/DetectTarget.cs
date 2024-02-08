using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour
{
    public GameObject beeobj;
    public GameObject Bucket;
    [Header("Particles")]
    public GameObject ParticleBlow;
    public bool bear;
    public static bool bearDialogue;
    //float f;
    //public GameObject[] soldiers;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bee") && !bear)
        {
            //f = ((float)Bucket.position.y);
            Instantiate(ParticleBlow, transform.position, Quaternion.identity);
            Bucket.SetActive(false);
            //do
            //{
            //    f -= 0.1f;
            //} while (Bucket.position.y < 0);
        }
        else if (other.gameObject.CompareTag("Player") && bear)
        {
            bearDialogue = true;
            Debug.Log("yeah");
        }
    }
}
