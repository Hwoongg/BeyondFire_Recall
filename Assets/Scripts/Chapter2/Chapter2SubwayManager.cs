using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2SubwayManager : MonoBehaviour {
    
    public GameObject objFadeEfx;

	// Use this for initialization
	void Start ()
    {
        FindObjectOfType<Fader>().FadeIn();
        FindObjectOfType<CameraSystem>().changeViewport();
        
	}
	
}
