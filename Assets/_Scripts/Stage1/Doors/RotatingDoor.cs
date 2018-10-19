using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDoor : Door
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
        OpenDoor();
    }

    private IEnumerator OpenDoor()
    {
        while (transform.rotation.eulerAngles.y < angleToRotate)
        {
            transform.RotateAround(hinge.position, Vector3.up, angleToRotate * Time.deltaTime * durationOfRotation);
            yield return new WaitForEndOfFrame();
        }
    }

    /*private void OpenDoor()
    {
        //transform.RotateAround(hinge.position, transform.forward, angleToRotate);
        Vector3 rot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rot.x, rot.y, angleToRotate);
    }*/
}
