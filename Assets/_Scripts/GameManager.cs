using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image youDiedBG;
    [SerializeField] private TextMeshProUGUI youDiedText;

    private Sequence loseGameSequence;

	void Awake(){
		//DontDestroyOnLoad(this);

        youDiedBG.color = new Color(youDiedBG.color.r, youDiedBG.color.g, youDiedBG.color.b, 0);
        youDiedText.alpha = 0f;
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

    public void OnPlayerDied()
    {
        StartCoroutine(LoseGame());
    }

    private IEnumerator LoseGame()
    {
        loseGameSequence = DOTween.Sequence();

        loseGameSequence.Insert(0f, youDiedBG.DOFade(1, 1));
        loseGameSequence.Insert(0f, DOTween.To( () => youDiedText.alpha, x => youDiedText.alpha = x, 1, 1));

        loseGameSequence.Play();

        yield return loseGameSequence.WaitForCompletion();

        SceneManager.LoadScene("Stage1");
    }
}
