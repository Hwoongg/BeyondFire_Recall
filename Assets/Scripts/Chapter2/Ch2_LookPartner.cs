using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ch2_LookPartner : InteractionSystem
{
    // 상단 CG 패널
    public GameObject objCG;
    public GameObject objCGTop;

    public Sprite LookPartnerCG;


    public Dialogue dialogue;

    CharacterMover playerMover;

    //bool isLooping;

    // Use this for initialization
    void Start()
    {
        //isLooping = false;
        //TopCGPannel = objTopCGPannel.GetComponent<Image>();

        playerMover = FindObjectOfType<CharacterMover>();

    }

    
    public override void doAction()
    {
        StartCoroutine(MainRoutine());
    }
    public override void upAction()
    {
        doAction();
    }
    public override void downAction()
    {
        doAction();
    }
    IEnumerator MainRoutine()
    {
        // 무브 잠금
        playerMover.moveType = CharacterMover.MoveType.NONE;

        // 상단 CG 출현. 패널 활성화
        objCG.SetActive(true);
        objCGTop.SetActive(true);

        // 상단 패널 이미지 교체
        objCGTop.GetComponent<Image>().sprite = LookPartnerCG;

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().canvasObj.activeSelf == false);

        objCGTop.SetActive(false);
        objCG.SetActive(false);

        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<FadeEffect>().FadeOut();
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("Chapter_3_Hospital_v2");

        //// 무브 잠금 해제
        //playerMover.moveType = CharacterMover.MoveType.COMMANDMOVE;

        yield break;
    }
}
