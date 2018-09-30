using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public Vector3 localPositionWhenInHand;
    public Vector3 rotationWhenInHand;
    [SerializeField] private string description;
    [SerializeField] private UnityEvent onInteraction;

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;

    private void Start()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
        originalRotation = transform.localRotation;
    }

    public void OnInteraction()
    {
        onInteraction.Invoke();
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
}
