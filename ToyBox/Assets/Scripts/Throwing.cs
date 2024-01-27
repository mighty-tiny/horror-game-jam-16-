using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throwing : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public Image Current;
    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;
    bool readyToThrow;
    [Header("Text")]
    public GameObject ClickE;
    [Header("PickedObjects")]
    public GameObject Bee; 
    private void Start()
    {
        readyToThrow = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bee"))
        {
            other.gameObject.SetActive(false);
            readyToThrow = true;
            Bee.gameObject.SetActive(true);
            ClickE.SetActive(true);
            
        }
    }

    private void Update()
    {
        if (Input.GetKey(throwKey))
        {
            FadeOut();
        }

        else if(Input.GetKeyUp(throwKey) && readyToThrow && totalThrows > 0)
        {

            Throw();
        }
    }

    private void Throw()
    {

        readyToThrow = false;
        Bee.gameObject.SetActive(false);
        ClickE.gameObject.SetActive(false);


        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;
        

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }
    private void ResetThrow()
    {
        readyToThrow = true;
    }
    public void FadeOut()
    {
        for (float f = 1f; f >= 0f; f += 0.5f)
        {
            Color c = Current.material.color;
            c.a = f;
            Current.material.color = c;

        }
    }
}