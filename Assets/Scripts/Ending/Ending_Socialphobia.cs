using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Socialphobia : MonoBehaviour
{
    public GameObject objFadeEfx;
    //private FadeEffect fadeEfx;

    public GameObject objBlack;
    public GameObject objAfterCG;

    public Dialogue fadeDialogue;
    public Dialogue beforeDialogue;
    public Dialogue afterDialogue;
    // Use this for initialization
    void Start()
    {
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

        objAfterCG.SetActive(true);
        FindObjectOfType<DialogueManager>().StartDialogue(afterDialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        
        objFadeEfx.GetComponent<FadeEffect>().FadeOut();

        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Ending_Suicide");
        yield break;
    }
}
