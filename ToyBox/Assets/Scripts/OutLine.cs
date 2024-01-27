using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLine : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    private float _maxRayDistance = 4f;
    private Outline outlineLast;


    private void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * _maxRayDistance, Color.green);
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out hit, _maxRayDistance))
        {
            if (hit.transform.gameObject.CompareTag("Bee") || hit.transform.gameObject.CompareTag("Tower"))
            {
                if (outlineLast != null)
                {
                    outlineLast.enabled = false;
                }

                outlineLast = hit.transform.gameObject.GetComponent<Outline>();
                outlineLast.enabled = true;
                
                
                
            }
            else if (outlineLast != null)
            {
                outlineLast.enabled = false;
                outlineLast = null;
            }
        }
    }
}
