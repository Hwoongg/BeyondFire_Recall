using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FireFight : InteractionSystem {

    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    public Dialogue dialogue4;


    // 화재 진압 CG
    public GameObject objCG;
    public GameObject objCGJH;
    public GameObject objCGFire;

    public Sprite JH1;
    public Sprite JH2;
    public Sprite FireCG1;

    public GameObject objFadeEfx;
    private FadeEffect fadeEfx;





	// Use this for initialization
	void Start () {
        fadeEfx = objFadeEfx.GetComponent<FadeEffect>();
	}

    public override void doAction()
    {
        StartCoroutine("FightFire");
    }

    public override void upAction()
    {
        doAction();
    }
    public override void downAction()
    {
        doAction();
    }

    IEnumerator FightFire()
    {
        // Full CG 활성화
        objCG.SetActive(true);

        // 대화1
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        // 대화2 
        objCGJH.GetComponent<Image>().sprite = JH1;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue2);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        // 대화3 
        objCGJH.GetComponent<Image>().sprite = JH2;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue3);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        // 대화4
        objCGFire.GetComponent<Image>().sprite = FireCG1;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue4);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        objFadeEfx.SetActive(true);
        fadeEfx.FadeOut();
        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene("Chapter1Start");

        yield break;
    }
}
