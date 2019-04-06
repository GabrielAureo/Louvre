using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class HandSmoothMovement : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private Transform rHand, lHand;
    [SerializeField] private float smoothTime = 5f;
    [SerializeField] private float XSensitivity = 2f;
    [SerializeField] private float YSensitivity = 2f;
    [SerializeField] private float MinimumX = -30f;
    [SerializeField] private float MaximumX = 30f;

    private Quaternion targetYRot, targetXRot;
    private Camera mainCamera;

    private void Start()
    {
        targetYRot = character.localRotation;
        targetXRot = character.localRotation;

        mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.position = character.position;

        Rotate();
    }

    private void Rotate()
    {
        Quaternion q = mainCamera.transform.localRotation;
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;
        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        if (angleX < MaximumX && angleX > MinimumX)
        {
            float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;
            targetXRot *= Quaternion.Euler(-xRot, 0f, 0f);
            rHand.localRotation = Quaternion.Slerp(rHand.localRotation, targetXRot, smoothTime * Time.deltaTime);
            lHand.localRotation = Quaternion.Slerp(lHand.localRotation, targetXRot, smoothTime * Time.deltaTime);
        }

        float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
        targetYRot *= Quaternion.Euler(0f, yRot, 0f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetYRot, smoothTime * Time.deltaTime);
    }
}
