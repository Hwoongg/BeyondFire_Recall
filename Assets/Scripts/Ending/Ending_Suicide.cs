using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Suicide : MonoBehaviour
{
    public GameObject objFadeEfx;
    //private FadeEffect fadeEfx;

    public GameObject objBlack;
    public GameObject objAfterCG1;
    public GameObject objAfterCG2;

    public Dialogue fadeDialogue;
    public Dialogue beforeDialogue;
    public Dialogue afterDialogue;
    AudioSource audio;
    
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        StartCoroutine(MainRoutine());
    }
    IEnumerator MainRoutine()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(fadeDialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);


        objFadeEfx.SetActive(true);
        objBlack.SetActive(false);
        objFadeEfx.GetComponent<FadeEffect>().FadeIn();

        FindObjectOfType<DialogueManager>().StartDialogue(beforeDialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        objAfterCG1.SetActive(true);
        objAfterCG2.SetActive(true);
        FindObjectOfType<DialogueManager>().StartDialogue(afterDialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);
        audio.Play();
        // 씬 끝나기 직전 딜레이
        yield return new WaitForSeconds(4.0f);

        objFadeEfx.GetComponent<FadeEffect>().FadeOut();
        audio.Play();

        yield return new WaitForSeconds(7.0f);

        SceneManager.LoadScene("Title");

        yield break;
    }
}