using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemSlot : MonoBehaviour
{
    public LayerMask layerToFitHere;
    public List<GameObject> rightItemsToFitHere;

    [SerializeField] private UnityEvent OnRightItemFit;
    [SerializeField] private UnityEvent OnWrongItemFit;


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

        CheckItem(go);
    }

    private void CheckItem(GameObject item)
    {
        if (rightItemsToFitHere.Count > 0)
        {
            if (IsThisItemRight(item))
            {
                OnRightItemFit.Invoke();
            }
            else
            {
                OnWrongItemFit.Invoke();
            }
        }
    }

    private bool IsThisItemRight(GameObject item)
    {
        return rightItemsToFitHere.Contains(item);
    }
}
