using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAndWatering : MonoBehaviour
{
    [Header("Tower")]
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
    public GameObject Cap;
    [Header("Particles")]
    public GameObject ParticleBlow;
    public GameObject ParticleWater;
    [Header("Watering")]
    bool potPickedUp;
    public GameObject pot;

    private void Start()
    {
        Won = false;
        potPickedUp = false;
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
        if (other.gameObject.CompareTag("Pot"))
        {
            potPickedUp = true;
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
        if (!Soldiers[0].activeInHierarchy && !Soldiers[0].activeInHierarchy && !Soldiers[0].activeInHierarchy && !Won)
        {
            Cap.SetActive(true);
            Won = true;
            
        }
    }
}
