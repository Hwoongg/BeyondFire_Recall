using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public void OpenChapter0()
    {
        SceneManager.LoadScene("Prologue");
    }

    public void OpenChapter1()
    {
        SceneManager.LoadScene("Chapter1");
    }

    public void OpenChapter2()
    {
        SceneManager.LoadScene("Chapter_2_subway_v3");
    }

    public void OpenChapter3()
    {
        SceneManager.LoadScene("Chapter_3_Hospital_v2");
    }

    public void CloseSelector()
    {
        SceneManager.UnloadSceneAsync("SceneSelect");
    }
}
