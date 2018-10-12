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

public class PuzzleItemSlot : ItemSlot
{
    public List<GameObject> rightItemsToFitHere;

    [System.Serializable] public class MyItemEvent : UnityEvent<ItemPlacedInSlotEventArg> { }

    [SerializeField] private MyItemEvent OnRightItemFit;
    [SerializeField] private MyItemEvent OnWrongItemFit;

    public new void FitItem(GameObject item)
    {
        //print("fit");
        base.FitItem(item);

        CheckItem(item);
    }

    private void CheckItem(GameObject item)
    {
        //print("check");
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
