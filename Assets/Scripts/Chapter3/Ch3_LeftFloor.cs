using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_LeftFloor : MonoBehaviour {

    public string InfoText;

    public GameObject RightTrigger;

    bool Looping;
    

    private void Start()
    {
        Looping = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //RightTrigger.SetActive(true);
        //StartCoroutine(LeftFloorText());
        //gameObject.SetActive(false);
        if(!Looping)
            StartCoroutine(Routine());
    }

    IEnumerator Routine()
    {
        Looping = true;
        RightTrigger.SetActive(true);
        yield return StartCoroutine(LeftFloorText());
        Looping = false;
        gameObject.SetActive(false);
        
        yield break;
    }
    IEnumerator LeftFloorText()
    {
        PlaceInfoUI placeUI = FindObjectOfType<PlaceInfoUI>();
        placeUI.TextOn(InfoText);
        yield return StartCoroutine(placeUI.TextFadeInRoutine());

        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(placeUI.TextFadeOutRoutine());

        yield break;
    }
}
