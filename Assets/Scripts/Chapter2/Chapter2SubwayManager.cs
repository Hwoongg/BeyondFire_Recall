using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2SubwayManager : MonoBehaviour {

    O2Gauge playerO2;

    bool isAntiSmoking;
    SmokeWall[] smokeWalls;

    [SerializeField] Dialogue dialogue;

	void Start ()
    {
        FindObjectOfType<Fader>().FadeIn();
        FindObjectOfType<CameraSystem>().changeViewport();
        FindObjectOfType<InventorySystem>().CreateO2Gauge();
        playerO2 = FindObjectOfType<O2Gauge>();
        smokeWalls = FindObjectsOfType<SmokeWall>();
        isAntiSmoking = false;
	}

    private void Update()
    {
        if(playerO2.GetPercentage() < 0.4f)
        {
            if(!isAntiSmoking)
            {
                StartAntiSmoking();
                isAntiSmoking = true;
            }
        }
    }

    void StartAntiSmoking()
    {
        // 진압 시작 대사 출력
        DialogueManager.Instance().StartDialogue(dialogue);

        for(int i=0; i<smokeWalls.Length; i++)
        {
            smokeWalls[i].StartMove();
        }
    }
}
