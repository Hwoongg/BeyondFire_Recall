using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_RightFloor : MonoBehaviour {

    public string InfoText;

    public GameObject LeftTrigger;

    bool Looping;

    private void Start()
    {
        Looping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //LeftTrigger.SetActive(true);
        //StartCoroutine(RightFloorText());
        //gameObject.SetActive(false);
        if (!Looping)
            StartCoroutine(Routine());
    }

    IEnumerator Routine()
    {
        Looping = true;
        LeftTrigger.SetActive(true);
        yield return StartCoroutine(RightFloorText());
        Looping = false;
        gameObject.SetActive(false);
        
        yield break;
    }

    IEnumerator RightFloorText()
    {
        PlaceInfoUI placeUI = FindObjectOfType<PlaceInfoUI>();
        placeUI.TextOn(InfoText);
        yield return StartCoroutine(placeUI.TextFadeInRoutine());

        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(placeUI.TextFadeOutRoutine());

        yield break;
    }
}
