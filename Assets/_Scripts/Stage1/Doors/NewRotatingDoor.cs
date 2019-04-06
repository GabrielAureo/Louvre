using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewRotatingDoor : MonoBehaviour
{
    [SerializeField] private Vector3 rotationWhenOpen = new Vector3(0, 90, 0);
    [SerializeField] private Vector3 rotationWhenClosed = new Vector3(0, 0, 0);
    [SerializeField] private float rotationDuration = 0.5f;

    public void OpenDoor()
    {
        transform.DOLocalRotate(rotationWhenOpen, rotationDuration);
    }

    public void CloseDoor()
    {
        transform.DOLocalRotate(rotationWhenClosed, rotationDuration);
    }
}
