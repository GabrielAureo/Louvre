﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, Readable
{
    public Vector3 localPositionWhenInHand;
    public Vector3 rotationWhenInHand;
    
    [SerializeField] private UnityEvent onInteraction;

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private Transform originalParent;

    [TextArea]
    [SerializeField] private string description = "Shoshite toki wa ugoki desu.";

    public void Start()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
        originalRotation = transform.localRotation;
        originalParent = transform.parent;
    }

    public virtual void OnInteraction()
    {
        onInteraction.Invoke();

        ItemSlot itemSlot = GetCurrentSlot();
        if (itemSlot) itemSlot.OnItemRemoved(gameObject);
    }

    public string GetDescription()
    {
        return description;
    }

    public Vector3 GetLocalPositionWhenInSlot()
    {
        return originalPosition;
    }

    public Vector3 GetLocalScaleWhenInSlot()
    {
        return originalScale;
    }

    public Quaternion GetLocalRotationWhenInSlot()
    {
        return originalRotation;
    }

    public void ResetItem()
    {
        GetBackToOriginalParent();
    }

    private void GetBackToOriginalParent()
    {
        if (originalParent && originalParent.GetComponent<ItemSlot>())
        {
            originalParent.GetComponent<ItemSlot>().FitItem(gameObject);
        }
    }

    public Transform GetOriginalSlot()
    {
        return originalParent;
    }

    public void SetNewOriginalSlot(Transform newSlot)
    {
        originalParent = newSlot;
    }

    public ItemSlot GetCurrentSlot()
    {
        if (transform.parent && transform.parent.GetComponent<ItemSlot>()) return transform.parent.GetComponent<ItemSlot>();
        else return null;
    }

    public static void ExchangeSlots(Item item1, Item item2)
    {
        Transform parent1 = item1.GetOriginalSlot();
        Transform parent2 = item2.GetOriginalSlot();

        item1.SetNewOriginalSlot(parent2);
        item2.SetNewOriginalSlot(parent1);
    }

    public virtual void OnDropped() {}
}
