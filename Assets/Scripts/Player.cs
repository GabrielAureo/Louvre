using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	CharacterController _controller;
	[SerializeField] float walkSpeed;

	
	void Awake(){
		_controller = GetComponent<CharacterController>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
		_controller.Move(moveDirection * walkSpeed);

		Look();
		
	}

	/*void Look()
    {
        Quaternion cr = cam.transform.rotation;
        Quaternion target = Quaternion.Euler(cr.eulerAngles.x - (Input.GetAxis("Mouse Y") * mouseSpeed), cr.eulerAngles.y + (Input.GetAxis("Mouse X") * mouseSpeed), 0);

        Vector3 tempAngles = target.eulerAngles;

        if (tempAngles.x > 180)
            tempAngles.x -= 360;

        float clampedX = Mathf.Clamp(tempAngles.x, -70, 70);
        tempAngles.x = clampedX;

        target = Quaternion.Euler(tempAngles);        
        
        cam.transform.rotation = target;

	} 
	*/
}
