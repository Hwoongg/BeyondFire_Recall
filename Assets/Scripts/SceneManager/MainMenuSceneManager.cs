using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuSceneManager : MonoBehaviour
{
    public void LoadHospitalCutScene()
    {
        SceneManager.LoadScene("CS_Hospital");
    }

    public void OpenSelector()
    {
        SceneManager.LoadScene("SceneSelect", LoadSceneMode.Additive);
    }
}
