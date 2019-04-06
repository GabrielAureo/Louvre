using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverMenuController gameOver;

	void Start ()
    {
		lockCursor();
	}

	public void OnApplicationFocus(bool hasFocus)
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
