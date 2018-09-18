using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private GameObject itemInHand;

    public GameObject ItemInHand
    {
        get
        {
            return itemInHand;
        }
    }

    public GameObject LetGoOfItem()
    {
        GameObject ret = itemInHand;
        itemInHand = null;
        return ret;
    }

    public void TakeItem(GameObject item)
    {
        if (item)
        {
            item.SetActive(true);
            if (item.GetComponent<Rigidbody>()) item.GetComponent<Rigidbody>().isKinematic = true;
            item.transform.parent = transform;
            item.transform.position = transform.position;
            item.transform.rotation = item.GetComponent<Item>().standardRotation;
            itemInHand = item;
        }
    }

    public bool IsHolding()
    {
        if (itemInHand == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
