using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPlacedInSlotEventArg
{
    public GameObject item;

    public ItemPlacedInSlotEventArg(GameObject go)
    {
        item = go;
    }
}

public class ItemSlot : MonoBehaviour
{
    public LayerMask layerToFitHere;
    public List<GameObject> rightItemsToFitHere;

    [System.Serializable] public class MyItemEvent : UnityEvent<ItemPlacedInSlotEventArg> { }

    [SerializeField] private MyItemEvent OnRightItemFit;
    [SerializeField] private MyItemEvent OnWrongItemFit;


    public void FitItem(GameObject item)
    {
        if (item.GetComponent<Rigidbody>()) item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.parent = transform;

        Item itemScript = item.GetComponent<Item>();
        if (itemScript)
        {
            item.transform.localPosition = itemScript.GetLocalPositionWhenInSlot();
            item.transform.localScale = itemScript.GetLocalScaleWhenInSlot();
            item.transform.localRotation = itemScript.GetLocalRotationWhenInSlot();
        }

        CheckItem(item);
    }

    private void CheckItem(GameObject item)
    {
        if (rightItemsToFitHere.Count > 0)
        {
            if (IsThisItemRight(item))
            {
                OnRightItemFit.Invoke(new ItemPlacedInSlotEventArg(item.gameObject));
            }
            else
            {
                OnWrongItemFit.Invoke(new ItemPlacedInSlotEventArg(item.gameObject));
            }
        }
    }

    private bool IsThisItemRight(GameObject item)
    {
        return rightItemsToFitHere.Contains(item);
    }
}
