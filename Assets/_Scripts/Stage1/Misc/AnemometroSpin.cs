using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnemometroSpin : MonoBehaviour
{
    [SerializeField] private Vector3 rotationBySecond;

    private void FixedUpdate()
    {
        transform.Rotate(rotationBySecond * Time.fixedDeltaTime);
    }
}
