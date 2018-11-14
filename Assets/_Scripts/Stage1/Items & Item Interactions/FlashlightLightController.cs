using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashlightLightController : MonoBehaviour
{
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 pointHit;
        //float distance;
        int layerMask = ~LayerMask.GetMask("Player"); // Todas as layers exceto o player

        Vector3 origin = mainCam.transform.position;
        Vector3 direction = mainCam.transform.forward;
        
        if (Physics.Raycast(origin, direction, out hit, 100f, layerMask))
        {
            pointHit = hit.point;
        }
        else
        {
            pointHit = origin + direction * 5f;
        }

        transform.DOLookAt(pointHit, 0.5f, AxisConstraint.None, transform.up);

        //distance = Vector3.Distance(origin, pointHit);
        //Debug.DrawLine(origin, pointHit, Color.yellow, 0.1f);
    }
}
