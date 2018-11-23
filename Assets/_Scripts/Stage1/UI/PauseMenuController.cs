using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject pausedText;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject quitButton;

    private bool isPaused = false;

    private void Awake()
    {
        //Resume();

        background.SetActive(false);
        pausedText.SetActive(false);
        resumeButton.SetActive(false);
        menuButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void PauseUnpause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        UnlockCursor();

        background.SetActive(true);
        pausedText.SetActive(true);
        resumeButton.SetActive(true);
        menuButton.SetActive(true);
        quitButton.SetActive(true);
        FindObjectOfType<FirstPersonController>().enabled = false;

        Time.timeScale = 0f;

        isPaused = true;
    }

    public void Resume()
    {
        LockCursor();

        background.SetActive(false);
        pausedText.SetActive(false);
        resumeButton.SetActive(false);
        menuButton.SetActive(false);
        quitButton.SetActive(false);
        FindObjectOfType<FirstPersonController>().enabled = true;

        Time.timeScale = 1f;

        isPaused = false;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(0);
        LoadingScreen.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        FindObjectOfType<HandSmoothMovement>().enabled = true;
    }

    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        FindObjectOfType<HandSmoothMovement>().enabled = false;
    }
}
