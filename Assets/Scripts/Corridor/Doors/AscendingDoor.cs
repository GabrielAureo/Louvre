using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscendingDoor : Door
{
    [SerializeField] private float heightToAscend;
    [SerializeField] private float duration;

    private float originalY;

    private void Awake()
    {
        originalY = transform.localPosition.y;
        duration = 1 / duration;
    }

    public void Unlock()
    {
        StartCoroutine("OpenDoor");
    }

    private IEnumerator OpenDoor()
    {
        while (transform.localPosition.y < heightToAscend)
        {
            Vector3 pos = transform.localPosition;
            transform.localPosition = new Vector3(pos.x, pos.y + heightToAscend * Time.deltaTime * duration, pos.z);
            yield return new WaitForEndOfFrame();
        }
    }
}
