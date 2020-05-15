using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Suicide : MonoBehaviour
{
    
    public GameObject objBlack;
    public GameObject objAfterCG1;
    public GameObject objAfterCG2;

    public Dialogue fadeDialogue;
    public Dialogue beforeDialogue;
    public Dialogue afterDialogue;
    AudioSource audio;

    Fader fader;
    DialogueManager dm;

    void Start()
    {
        fader = FindObjectOfType<Fader>();
        dm = DialogueManager.Instance();

        audio = gameObject.GetComponent<AudioSource>();
        StartCoroutine(MainRoutine());
    }
    IEnumerator MainRoutine()
    {
        DialogueManager.Instance().StartDialogue(fadeDialogue);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);


        
        objBlack.SetActive(false);
        fader.FadeIn();

        DialogueManager.Instance().StartDialogue(beforeDialogue);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        objAfterCG1.SetActive(true);
        objAfterCG2.SetActive(true);
        DialogueManager.Instance().StartDialogue(afterDialogue);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);
        audio.Play();
        // 씬 끝나기 직전 딜레이
        yield return new WaitForSeconds(4.0f);

        fader.FadeOut();
        audio.Play();

        yield return new WaitForSeconds(7.0f);

        SceneManager.LoadScene("Title");

        yield break;
    }
}