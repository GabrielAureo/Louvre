using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, ISubject
{
    public List<IObserver> observers;

    private List<GameObject> inventory;

    public List<GameObject> Inv
    {
        get
        {
            return inventory;
        }

        set
        {
            inventory = value;
        }
    }

    private void Update()
    {
        print(inventory.Count);
    }

    private void Awake()
    {
        inventory = new List<GameObject>();
        observers = new List<IObserver>();
    }

    public void AddItem(GameObject go)
    {
        if (go)
        {
            go.SetActive(false);
            inventory.Add(go);
        }

        Notify();
    }

    public void AddItemAt(GameObject go, int index)
    {
        if (go)
        {
            go.SetActive(false);
            inventory.Insert(index, go);
        }

        Notify();
    }

    public GameObject RemoveItem(GameObject go)
    {
        inventory.Remove(go);
        Notify();
        return go;
    }

    public GameObject RemoveIndex(int index)
    {
        GameObject go = inventory[index];
        inventory.RemoveAt(index);
        Notify();
        return go;
    }

    public GameObject Pop()
    {
        GameObject last = inventory[inventory.Count - 1];
        inventory.Remove(last);
        Notify();
        return last;
    }

    public int GetCount()
    {
        return inventory.Count;
    }

    public void AddObserver(IObserver o)
    {
        observers.Add(o);
    }

    public void RemoveObserver(IObserver o)
    {
        observers.Remove(o);
    }

    public void Notify()
    {
        foreach (IObserver observer in observers)
        {
            observer.OnNotify();
        }
    }
}
