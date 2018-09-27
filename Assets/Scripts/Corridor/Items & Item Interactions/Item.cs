using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public Quaternion standardRotation;
    [SerializeField] private string description;
    [SerializeField] private UnityEvent onInteraction;

    public void OnInteraction()
    {
        onInteraction.Invoke();
    }

    public string GetDescription()
    {
        return description;
    }
}
