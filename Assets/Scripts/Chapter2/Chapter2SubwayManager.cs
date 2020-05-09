using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2SubwayManager : MonoBehaviour {
    
    public GameObject objFadeEfx;

	// Use this for initialization
	void Start ()
    {
        objFadeEfx.SetActive(true);
        objFadeEfx.GetComponent<FadeEffect>().FadeIn();
        FindObjectOfType<CameraSystem>().changeViewport();
        
	}
	
}
