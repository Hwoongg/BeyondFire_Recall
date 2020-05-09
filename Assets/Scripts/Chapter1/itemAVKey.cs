using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemAVKey : ItemSystem
{
    public Dialogue dialogue;

    private void Start() // 부모의 Start와의 관계 명확히 할 것
    {
        ItemName = "AVKey";
    }
    public override void doAction()
    {
        StartCoroutine("AVKeyEvent");
    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    IEnumerator AVKeyEvent()
    {
        FindObjectOfType<CharacterMover>().myAnimator.SetBool("IsBack", true);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);


        FindObjectOfType<InventorySystem>().AddItem(this);

        gameObject.SetActive(true);

        FindObjectOfType<CharacterMover>().myAnimator.SetBool("IsBack", false);
        yield break;
    }
}
