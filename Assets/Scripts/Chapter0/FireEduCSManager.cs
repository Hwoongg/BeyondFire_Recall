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
    // Use this for initialization
    void Start ()
    {
        //SceneCount = 0;
        //isStop = false;
        StartCoroutine("AllTalk");
        isEnd = false;
	}

    //// Update is called once per frame
    //void Update () {
    //       switch (SceneCount)
    //       {
    //           case 0:
    //               Event0();
    //               break;
    //           case 1:
    //               Event1();
    //               break;
    //           case 2:
    //               Event2();
    //               break;
    //           case 3:
    //               Event3();
    //               break;
    //       }
    //   }

    //   void Event0()
    //   {
    //       // 페이드 인
    //       objFadeEfx.SetActive(true);
    //       objFadeEfx.GetComponent<FadeEffect>().FadeIn();

    //       SceneCount++;
    //   }

    //   void Event1()
    //   {
    //       // 대화 1
    //       if (!isStop)
    //       {
    //           dialogue1.TriggerDialogue();
    //           isStop = true;
    //       }
    //       if (dialogueBox.activeSelf == false)
    //       {
    //           isStop = false;
    //           SceneCount++;
    //       }
    //   }

    //   void Event2()
    //   {
    //       // 대화 2
    //       if (!isStop)
    //       {
    //           dialogue2.TriggerDialogue();
    //           isStop = true;
    //       }
    //       if (dialogueBox.activeSelf == false)
    //       {
    //           isStop = false;
    //           SceneCount++;
    //       }
    //   }

    //   void Event5()
    //   {
    //       // Fade Out
    //       objFadeEfx.SetActive(true);
    //       objFadeEfx.GetComponent<FadeEffect>().FadeOut();
    //   }

    
    IEnumerator AllTalk()
    {
        objFadeEfx.SetActive(true);
        objFadeEfx.GetComponent<FadeEffect>().FadeIn();

        yield return new WaitForSeconds(1.5f);

        FindObjectOfType<DialogueManager>().StartDialogue(Talk1);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        FindObjectOfType<DialogueManager>().StartDialogue(Talk2);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        FindObjectOfType<DialogueManager>().StartDialogue(Talk3);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        FindObjectOfType<DialogueManager>().StartDialogue(Talk4);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        yield return new WaitForSeconds(0.5f);
        objFadeEfx.GetComponent<FadeEffect>().FadeOut();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Prologue");

        yield break;
    }
}
