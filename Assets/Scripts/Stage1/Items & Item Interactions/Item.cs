using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public Quaternion standardRotation;
    [SerializeField] private string description;
    [SerializeField] private UnityEvent onInteraction;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        originalPosition = transform.localPosition;
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

    public Vector3 GetOriginalPosition()
    {
        return originalPosition;
    }

    public Quaternion GetOriginalRotation()
    {
        return originalRotation;
    }
}
