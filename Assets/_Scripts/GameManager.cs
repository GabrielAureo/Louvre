using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {
		lockCursor();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
		
	}

	void OnApplicationFocus(bool hasFocus){
		if (hasFocus) lockCursor();
	}

	void lockCursor(){
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

    public static void OnPlayerDied()
    {
        print("gamemanager.onplayerdied");
    }
}
