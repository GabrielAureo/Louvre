using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private float maxReach = 3f;
    [SerializeField] private float dropDistance = 1f;
    [SerializeField] private float dropHeight = 1f;
    [SerializeField] private float dropHorizontalAdjust = 0.5f;
    [SerializeField] private LayerMask itemLayerMask;

    private Inventory inventoryScript;
    private Hand handScript;
    private Camera mainCamera;

    private void Awake()
    {
        inventoryScript = GetComponent<Inventory>();
        handScript = GetComponentInChildren<Hand>();
        mainCamera = Camera.main;
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

        if (Input.GetKeyDown(KeyCode.Alpha1)) AttemptToSwitchItemInHand(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) AttemptToSwitchItemInHand(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) AttemptToSwitchItemInHand(3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) AttemptToSwitchItemInHand(4);
        if (Input.GetKeyDown(KeyCode.Alpha5)) AttemptToSwitchItemInHand(5);
        if (Input.GetKeyDown(KeyCode.Alpha6)) AttemptToSwitchItemInHand(6);
        if (Input.GetKeyDown(KeyCode.Alpha7)) AttemptToSwitchItemInHand(7);
        if (Input.GetKeyDown(KeyCode.Alpha8)) AttemptToSwitchItemInHand(8);
        if (Input.GetKeyDown(KeyCode.Alpha9)) AttemptToSwitchItemInHand(9);
        if (Input.GetKeyDown(KeyCode.Alpha0)) AttemptToSwitchItemInHand(10);
    }


    private void AttemptToGetItem()
    {
        Vector3 origin = mainCamera.transform.position;
        Vector3 direction = mainCamera.transform.forward;
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
        inventoryScript.AddItem(itemHeld);
        if (itemToTake.GetComponent<Item>()) itemToTake.GetComponent<Item>().OnInteraction();
    }

    private void DropItem()
    {
        Vector3 vectorPointingToTheLeftOfThePlayer = (-(Vector3.Cross(transform.up, transform.forward))).normalized;
        GameObject item = handScript.LetGoOfItem();
        item.transform.position = transform.position + transform.forward * dropDistance + Vector3.up * dropHeight + vectorPointingToTheLeftOfThePlayer * dropHorizontalAdjust;
        item.transform.parent = null;
        if (item.GetComponent<Rigidbody>()) item.GetComponent<Rigidbody>().isKinematic = false;

        if (inventoryScript.GetCount() > 0) handScript.TakeItem(inventoryScript.Pop());
    }

    private void AttemptToSwitchItemInHand(int inventoryIndex)
    {
        if (handScript.IsHolding() && inventoryScript.GetCount() >= inventoryIndex)
        {
            SwitchItemInHand(inventoryIndex);
        }
    }

    private void SwitchItemInHand(int inventoryIndex)
    {
        GameObject itemHeld = handScript.LetGoOfItem();
        handScript.TakeItem(inventoryScript.RemoveIndex(inventoryIndex - 1));
        inventoryScript.AddItemAt(itemHeld, inventoryIndex - 1);
    }
}
