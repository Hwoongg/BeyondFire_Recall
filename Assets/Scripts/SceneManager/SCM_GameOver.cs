using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCM_GameOver : MonoBehaviour
{
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance();
    }
    public void RestartEvent()
    {
        SceneManager.LoadScene(gm.GetLastScene());
    }

    public void MainMenuEvent()
    {
        SceneManager.LoadScene("MainMenu_v2");
    }
}
