using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashlightLightController : MonoBehaviour
{
    private Camera mainCam;
    private int layerMask;

    private void Start()
    {
        mainCam = Camera.main;

        // Criando uma Layer Mask: todas as layers exceto Player e LightedArea
        //               \/ Cria uma Layer que é só o Player, usando a operação de shift
        //                                                        \/ Cria uma Layer que é só a LightedArea, usando a operação de shift
        //                                                  \/ Usa a operação de OR bitwise para combinar as duas Layer do Player e da LightedArea
        //          \/ Usa a operação de NOT bitwise para inverter a Layer, resultando numa Layer que vale para todas menos Player de LightedArea
        layerMask = ~((1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("LightedArea")) | (1 << LayerMask.NameToLayer("ItemSlot")));
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 pointHit;

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

        //float distance = Vector3.Distance(origin, pointHit);
        //Debug.DrawLine(origin, pointHit, Color.yellow, 0.1f);
    }
}
