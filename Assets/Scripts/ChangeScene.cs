using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadCutSceneChapter1Start()
    {
        SceneManager.LoadScene("Chapter1Start");
    }
    public void LoadCutSceneeChapter1()
    {
        SceneManager.LoadScene("Chapter1");
    }
    public void LoadSceneChapter2()
    {
        SceneManager.LoadScene("Chapter_2");
    }
    public void LoadPrologueSkip()
    {
        SceneManager.LoadScene("Prologue_Skip", LoadSceneMode.Additive);
    }

    public void ClosePrologueSkip()
    {
        SceneManager.UnloadSceneAsync("Prologue_Skip");
    }


}
