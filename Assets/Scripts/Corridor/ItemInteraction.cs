using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private float maxReach = 3f;
    [SerializeField] private float dropDistance = 1f;
    [SerializeField] private LayerMask itemLayerMask;

    private Inventory inventoryScript;
    private Hand handScript;

    private void Awake()
    {
        inventoryScript = GetComponent<Inventory>();
        handScript = GetComponentInChildren<Hand>();
    }

    void Update ()
    {
        GetInput();
	}

    private void GetInput()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            if (handScript.ItemInHand && handScript.ItemInHand.GetComponent<Flashlight>()) handScript.ItemInHand.GetComponent<Flashlight>().TurnOnOff();
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            AttemptToGetItem();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (handScript.IsHolding())
            {
                DropItem();
            }
        }
    }

    private void AttemptToGetItem()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit hitInfo;

        if (Physics.Raycast(origin, direction, out hitInfo, maxReach, itemLayerMask) && hitInfo.collider.gameObject != null)
        {
            GetItem(hitInfo.collider.gameObject);
        }
    }

    private void GetItem(GameObject itemToTake)
    {
        GameObject itemHeld = handScript.LetGoOfItem();
        handScript.TakeItem(itemToTake);
        inventoryScript.Add(itemHeld);
    }

    private void DropItem()
    {
        GameObject item = handScript.LetGoOfItem();
        item.transform.position = transform.position + transform.forward * dropDistance;
        item.transform.parent = null;
        if (item.GetComponent<Rigidbody>()) item.GetComponent<Rigidbody>().isKinematic = false;

        if (inventoryScript.GetCount() > 0) handScript.TakeItem(inventoryScript.Pop());
    }
}
