using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireEduCSManager : MonoBehaviour
{
    //int SceneCount;
    //bool isStop;

    //public GameObject dialogueBox;
    //public DialogueTrigger dialogue1;
    //public DialogueTrigger dialogue2;
    //public DialogueTrigger dialogue3;
    //public DialogueTrigger dialogue4;

    public Dialogue Talk1;
    public Dialogue Talk2;
    public Dialogue Talk3;
    public Dialogue Talk4;
    bool isEnd;

    public GameObject objFadeEfx;

    DialogueManager dm;

    void Start ()
    {
        //SceneCount = 0;
        //isStop = false;
        dm = DialogueManager.Instance();
        StartCoroutine(AllTalk());
        isEnd = false;
	}

    
    IEnumerator AllTalk()
    {
        objFadeEfx.SetActive(true);
        objFadeEfx.GetComponent<FadeEffect>().FadeIn();

        yield return new WaitForSeconds(1.5f);

        dm.StartDialogue(Talk1);
        yield return new WaitUntil(() => dm.canvasObj.activeSelf == false);

        dm.StartDialogue(Talk2);
        yield return new WaitUntil(() => dm.canvasObj.activeSelf == false);

        dm.StartDialogue(Talk3);
        yield return new WaitUntil(() => dm.canvasObj.activeSelf == false);

        dm.StartDialogue(Talk4);
        yield return new WaitUntil(() => dm.canvasObj.activeSelf == false);

        yield return new WaitForSeconds(0.5f);
        objFadeEfx.GetComponent<FadeEffect>().FadeOut();
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Prologue");

        yield break;
    }
}
