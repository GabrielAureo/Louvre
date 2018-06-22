using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		lockCursor();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnApplicationFocus(bool hasFocus){
		if (hasFocus) lockCursor();
	}

	void lockCursor(){
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
