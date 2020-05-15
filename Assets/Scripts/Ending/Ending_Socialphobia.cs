using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Socialphobia : MonoBehaviour
{
    Fader fader;
    DialogueManager dm;

    public GameObject objBlack;
    public GameObject objAfterCG;

    public Dialogue fadeDialogue;
    public Dialogue beforeDialogue;
    public Dialogue afterDialogue;
    // Use this for initialization
    void Start()
    {
        fader = FindObjectOfType<Fader>();
        dm = DialogueManager.Instance();

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

        objAfterCG.SetActive(true);
        DialogueManager.Instance().StartDialogue(afterDialogue);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        
        fader.FadeOut();

        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Ending_Suicide");
        yield break;
    }
}
