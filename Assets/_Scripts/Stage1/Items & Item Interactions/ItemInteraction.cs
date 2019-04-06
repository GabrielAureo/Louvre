using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour, ISubject
{
    [SerializeField] private float maxReach = 3f;
    [SerializeField] private float dropDistance = 1f;
    [SerializeField] private float dropHeight = 1f;
    [SerializeField] private float dropHorizontalAdjust = 0.5f;
    [SerializeField] private LayerMask interactableItemLayerMask;
    [SerializeField] private LayerMask examinableItemLayerMask;
    [SerializeField] private LayerMask slotLayerMask;
    [SerializeField] private Hand handScript;

    public List<IObserver> observers;

    private Inventory inventoryScript;
    private Camera mainCamera;

    private void Awake()
    {
        inventoryScript = GetComponent<Inventory>();
        mainCamera = Camera.main;
        observers = new List<IObserver>();
    }

    void Update ()
    {
        GetInput();
        AttemptToGetItemDescription();
	}

    private void GetInput()
    {
        // The player will no longer be able to drop the flashlight, so we don't need to check if she is holding it anymore
        //if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        //{
        //    if (handScript.ItemInHand && handScript.ItemInHand.GetComponent<Flashlight>()) handScript.ItemInHand.GetComponent<Flashlight>().TurnOnOff();
        //}

        if (Input.GetKeyDown(KeyCode.E))
        {
            AttemptToGetItem();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!AttemptToFitItemInSlot() && handScript.IsHolding())
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

    private void AttemptToGetItemDescription()
    {
        RaycastHit hitInfo;
        if (PerformRaycast(out hitInfo, examinableItemLayerMask))
        {
            GetItemDescription(hitInfo.collider.gameObject);
        }
    }

    private void GetItemDescription(GameObject item)
    {
        string description = null;

        if (item.GetComponent<Item>()) description = item.GetComponent<Item>().GetDescription();
        else if (item.GetComponent<Painting>()) description = item.GetComponent<Painting>().GetDescription();

        Notify(new NotifyArg(description));
    }

    private void AttemptToGetItem()
    {
        RaycastHit hitInfo;
        if (PerformRaycast(out hitInfo, interactableItemLayerMask))
        {
            if (!handScript.ItemInHand) // The game won't have an Inventory System anymore, so the player now can only hold one item at a time.
                GetItem(hitInfo.collider.gameObject);
        }
    }

    private void GetItem(GameObject itemToTake)
    {
        if (itemToTake.GetComponent<Item>()) itemToTake.GetComponent<Item>().OnInteraction();
        GameObject itemHeld = handScript.LetGoOfItem();
        if (itemToTake.GetComponent<Collider>()) itemToTake.GetComponent<Collider>().enabled = false;
        handScript.TakeItem(itemToTake);
        inventoryScript.AddItem(itemHeld);
    }

    private void DropItem()
    {
        Vector3 vectorPointingToTheLeftOfThePlayer = (-(Vector3.Cross(transform.up, transform.forward))).normalized;
        GameObject item = handScript.LetGoOfItem();
        if (item && item.GetComponent<Collider>()) item.GetComponent<Collider>().enabled = true;
        item.transform.position = transform.position + transform.forward * dropDistance + Vector3.up * dropHeight + vectorPointingToTheLeftOfThePlayer * dropHorizontalAdjust;
        item.transform.parent = null;
        if (item.GetComponent<Rigidbody>()) item.GetComponent<Rigidbody>().isKinematic = false;
        if (item.GetComponent<Item>()) item.GetComponent<Item>().OnDropped();

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

    private bool AttemptToFitItemInSlot()
    {
        RaycastHit hitInfo;
        if (PerformRaycast(out hitInfo, slotLayerMask))
        {
            LayerMask itemLayer = hitInfo.collider.gameObject.GetComponent<ItemSlot>().layerToFitHere;
            if (itemLayer == (itemLayer | (1 << handScript.ItemInHand.layer)))
            {
                FitItemInSlot(hitInfo.collider.gameObject);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void FitItemInSlot(GameObject slot)
    {
        GameObject item = handScript.LetGoOfItem();
        if (item && item.GetComponent<Collider>()) item.GetComponent<Collider>().enabled = true;
        if (slot.GetComponent<PuzzleOneItemSlot>()) slot.GetComponent<PuzzleOneItemSlot>().FitItem(item);
        else if (slot.GetComponent<ItemSlot>()) slot.GetComponent<ItemSlot>().FitItem(item);
        if (inventoryScript.GetCount() > 0) handScript.TakeItem(inventoryScript.Pop());
    }

    public void AddObserver(IObserver o)
    {
        observers.Add(o);
    }

    public void RemoveObserver(IObserver o)
    {
        observers.Remove(o);
    }

    public void Notify(NotifyArg arg)
    {
        foreach (IObserver o in observers)
        {
            o.OnNotify(arg);
        }
    }

    private bool PerformRaycast(out RaycastHit hitInfo, LayerMask layer)
    {
        Vector3 origin = mainCamera.transform.position;
        Vector3 direction = mainCamera.transform.forward;

        //Debug.DrawRay(origin, direction * maxReach, Color.red, 0.3f);

        if (Physics.Raycast(origin, direction, out hitInfo, maxReach, layer) && hitInfo.collider.gameObject != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
