using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform hinge;
    [SerializeField] private float angleToRotate;
    [SerializeField] private float durationOfRotation;

    private void Awake()
    {
        durationOfRotation = 1 / durationOfRotation;
    }

    public void Unlock()
    {
        StartCoroutine("OpenDoor");
    }

    private IEnumerator OpenDoor()
    {
        while (transform.localRotation.eulerAngles.y < angleToRotate)
        {
            transform.RotateAround(hinge.position, Vector3.up, angleToRotate * Time.deltaTime * durationOfRotation);
            yield return new WaitForEndOfFrame();
        }
    }
}
