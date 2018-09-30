using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public LayerMask itemToFitHere;

    public void FitItem(GameObject go)
    {
        if (go.GetComponent<Rigidbody>()) go.GetComponent<Rigidbody>().isKinematic = true;
        go.transform.parent = transform;
        if (go.GetComponent<Item>())
        {
            go.transform.localPosition = go.GetComponent<Item>().GetLocalPositionWhenInSlot();
            go.transform.localScale = go.GetComponent<Item>().GetLocalScaleWhenInSlot();
            go.transform.localRotation = go.GetComponent<Item>().GetLocalRotationWhenInSlot();
        }
    }
}
