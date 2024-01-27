using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLine : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    private float _maxRayDistance = 4f;
    private OutLine outlineLast;


    private void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * _maxRayDistance, Color.green);
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out hit, _maxRayDistance))
        {
            if (hit.transform.gameObject.CompareTag("Bee"))
            {
                outlineLast = hit.transform.gameObject.GetComponent<OutLine>();
                outlineLast.enabled = true;
                if (outlineLast != null)
                {
                    outlineLast.enabled = false;
                }
                
                else if (outlineLast != null)
                {
                    outlineLast.enabled = false;
                    outlineLast = null;
                }
            }
        }
    }
}
