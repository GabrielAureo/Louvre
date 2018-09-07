using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvas : MonoBehaviour, IObserver
{
    [SerializeField] private Inventory inventoryScript;
    [SerializeField] private GameObject itemPrefab;

    private List<GameObject> items;

    private void Start()
    {
        items = new List<GameObject>();
        inventoryScript.AddObserver(this);
    }

    public void OnNotify()
    {
        foreach (GameObject go in items) Destroy(go);
        items.Clear();

        foreach (GameObject go in inventoryScript.Inv)
        {
            string s = go.name + "\n" + (inventoryScript.Inv.IndexOf(go) + 1).ToString();
            //string s = go.name;
            GameObject instance = Instantiate(itemPrefab, transform);
            instance.GetComponent<Text>().text = s;
            items.Add(instance);
        }
    }
}
