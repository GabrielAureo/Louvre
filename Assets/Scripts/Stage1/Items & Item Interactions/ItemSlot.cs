using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public LayerMask layerToFitHere;

    public void FitItem(GameObject item)
    {
        //print("base fit");
        if (item.GetComponent<Rigidbody>()) item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.parent = transform;

        Item itemScript = item.GetComponent<Item>();
        if (itemScript)
        {
            item.transform.localPosition = itemScript.GetLocalPositionWhenInSlot();
            item.transform.localScale = itemScript.GetLocalScaleWhenInSlot();
            item.transform.localRotation = itemScript.GetLocalRotationWhenInSlot();
        }

        //CheckItem(item);
    }
}
