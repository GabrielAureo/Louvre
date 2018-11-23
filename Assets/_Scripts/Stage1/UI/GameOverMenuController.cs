using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameOverMenuController : MonoBehaviour
{
    [SerializeField] private Image youDiedBG;
    [SerializeField] private TextMeshProUGUI youDiedText;
    [SerializeField] private GameObject playAgainButton;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject quitButton;

    private Sequence loseGameSequence;

    private void Awake()
    {
        youDiedBG.color = new Color(youDiedBG.color.r, youDiedBG.color.g, youDiedBG.color.b, 0);
        youDiedText.alpha = 0f;
        youDiedBG.gameObject.SetActive(false);
        youDiedText.gameObject.SetActive(false);
        playAgainButton.SetActive(false);
        menuButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void GameOver()
    {
        StartCoroutine(LoseGame());
    }

    private IEnumerator LoseGame()
    {
        FindObjectOfType<FirstPersonController>().enabled = false;
        youDiedBG.gameObject.SetActive(true);
        youDiedText.gameObject.SetActive(true);

        loseGameSequence = DOTween.Sequence();

        loseGameSequence.Insert(0f, youDiedBG.DOFade(1, 1));
        loseGameSequence.Insert(0f, DOTween.To(() => youDiedText.alpha, x => youDiedText.alpha = x, 1, 1));

        loseGameSequence.Play();

        yield return loseGameSequence.WaitForCompletion();

        UnlockCursor();
        playAgainButton.SetActive(true);
        menuButton.SetActive(true);
        quitButton.SetActive(true);
    }

    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayAgain()
    {
        //SceneManager.LoadScene(1);
        LoadingScreen.LoadScene(1);
    }

    public void Menu()
    {
        //SceneManager.LoadScene(0);
        LoadingScreen.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
