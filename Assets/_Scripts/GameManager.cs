using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverMenuController gameOver;
    [SerializeField] private PauseMenuController pause;

	// Use this for initialization
	void Start ()
    {
		lockCursor();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause.PauseUnpause();
		}
	}

	void OnApplicationFocus(bool hasFocus)
    {
		if (hasFocus) lockCursor();
	}

	void lockCursor()
    {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

    public void OnPlayerDied()
    {
        gameOver.GameOver();
    }
}
