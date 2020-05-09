using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartOnUI : MonoBehaviour
{
    public GameObject UI;
    
    void Awake()
    {
        UI.SetActive(true);
    }
}