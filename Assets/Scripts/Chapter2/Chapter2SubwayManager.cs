using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2SubwayManager : MonoBehaviour {
    
    
	void Start ()
    {
        FindObjectOfType<Fader>().FadeIn();
        FindObjectOfType<CameraSystem>().changeViewport();
        FindObjectOfType<InventorySystem>().CreateO2Gauge();
	}
	
}
