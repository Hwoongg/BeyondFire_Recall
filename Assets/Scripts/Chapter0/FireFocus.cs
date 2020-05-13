using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 최초 불 목격 CG 등장 트리거
public class FireFocus : DialogueTrigger
{
    bool isNotUse;
    public GameObject objCG;
    public GameObject objCGTop;
    public Sprite LookFireCG;

    private void Start()
    {
        isNotUse = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isNotUse)
        {
            isNotUse = false;
            StartCoroutine("FocusFire");
        }
    }

    

    IEnumerator FocusFire()
    {
        // CG 활성화
        objCG.SetActive(true);
        objCGTop.SetActive(true);
        objCGTop.GetComponent<Image>().sprite = LookFireCG;

        
        // 대사 출력
        DialogueManager.Instance().StartDialogue(dialogue);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        objCG.SetActive(false);

        yield break;
    }

}
