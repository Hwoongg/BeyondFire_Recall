using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCM_GameOver : MonoBehaviour
{
    GameManager gm;
    [SerializeField] GameObject coverObj;
    Vector4 transColor;
    [SerializeField] float fadeSpeed = 1;

    delegate void FadeCallBack();

    bool isFading;

    private void Awake()
    {
        isFading = false;
    }

    private void Start()
    {
        gm = GameManager.Instance();

        StartCoroutine(UIAnimator.Color(coverObj, Color.black, Vector4.zero, fadeSpeed));
    }

    public void RestartEvent()
    {
        if (isFading)
            return;

        StopAllCoroutines();

        StartCoroutine(
            FadeOutRoutine(Restart));
    }

    void Restart()
    {
        string sceneName = gm.GetLastScene();

        if (sceneName == null)
        {
            Debug.Log("Last Scene Is Notihing");
            return;
        }
    }

    public void MainMenuEvent()
    {
        if (isFading)
            return;

        StopAllCoroutines();

        StartCoroutine(
            FadeOutRoutine(GotoMainMenu));
    }

    void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu_v2");
    }

    IEnumerator FadeOutRoutine(FadeCallBack fadeCallBack)
    {
        isFading = true;
        yield return UIAnimator.Color(coverObj, Vector4.zero, Color.black, fadeSpeed * 2);

        fadeCallBack();
    }
}
