using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<GameObject> inventory;

    private void Awake()
    {
        inventory = new List<GameObject>();
    }

    public void Add(GameObject go)
    {
        if (go) go.SetActive(false);
        inventory.Add(go);
    }

    public void Remove(GameObject go)
    {
        inventory.Remove(go);
    }

    public GameObject Pop()
    {
        GameObject last = inventory[inventory.Count - 1];
        inventory.Remove(last);
        return last;
    }

    public int GetCount()
    {
        return inventory.Count;
    }
}
