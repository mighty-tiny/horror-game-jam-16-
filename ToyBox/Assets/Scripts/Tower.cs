using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    bool towered;
    public GameObject cam;
    public GameObject text;
    [Header("Keybinds")]
    public KeyCode Key = KeyCode.Space;
    public Outline outline;
    //public GameObject[] soldiers;
    public Transform TowerPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            towered = true;
            outline.enabled = true;
            if (Input.GetKeyDown(Key))
            {
                cam.transform.position = new Vector4(7.44f, 17.81f, 12.4f, 0);
                transform.position = new Vector4(7.44f, 17.81f, 12.4f, 0);
            }

        }
        else
        {
            towered = false;
            outline.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            towered = false;
            outline.enabled = false;
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
