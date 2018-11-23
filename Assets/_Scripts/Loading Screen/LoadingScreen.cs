using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScreen : MonoBehaviour
{
    private static int scene;

    [SerializeField] private Slider slider;
    [SerializeField] private float loadingBarTime = 0.2f;

    private AsyncOperation asyncLoading;

    private void Start()
    {
        StartCoroutine(Load());
    }

    public static void LoadScene(int s)
    {
        scene = s;
        SceneManager.LoadScene("LoadingScreen");
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(5f);

        asyncLoading = SceneManager.LoadSceneAsync(scene);
        asyncLoading.allowSceneActivation = true;

        while (!asyncLoading.isDone)
        {
            slider.DOValue(asyncLoading.progress, loadingBarTime);

            if (asyncLoading.progress == 0.9f)
            {
                slider.value = 1f;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
