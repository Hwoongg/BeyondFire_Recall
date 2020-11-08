using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public void OpenChapter0()
    {
        SceneManager.LoadScene("CS_FireEdu");
    }

    public void OpenChapter1()
    {
        SceneManager.LoadScene("Chapter1Start");
    }

    public void OpenChapter2()
    {
        SceneManager.LoadScene("Ch2_SubwayReady");
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
