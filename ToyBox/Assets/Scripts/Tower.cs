using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    static public bool towered;
    public GameObject cam;
    public GameObject text;
    [Header("Keybinds")]
    public KeyCode Key = KeyCode.Space;
    public Outline outline;
    //public GameObject[] soldiers;
    public Transform TowerPos;
    public static bool Won;
    public GameObject[] Soldiers;
    private void Start()
    {
        Won = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            towered = true;
            outline.enabled = true;
            

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
        if (Won)
        {

        }
    }
}
