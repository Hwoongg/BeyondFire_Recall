﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Interview : MonoBehaviour
{
    


    public GameObject objCG1;
    public GameObject objCG2;
    public GameObject objCG3;
    public GameObject objCG4;

    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    public Dialogue dialogue4;
    DialogueManager dm;
    Fader fader;
    // Use this for initialization
    void Start()
    {
        dm = DialogueManager.Instance();
        fader = FindObjectOfType<Fader>();

        StartCoroutine(MainRoutine());
    }
    IEnumerator MainRoutine()
    {
        fader.FadeIn();

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().canvasObj.activeSelf == false);

        objCG1.SetActive(false);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue2);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().canvasObj.activeSelf == false);

        objCG2.SetActive(false);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue3);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().canvasObj.activeSelf == false);

        objCG3.SetActive(false);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue4);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().canvasObj.activeSelf == false);

        fader.FadeOut();

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Ending_Socialphobia");
        yield break;
    }
}
