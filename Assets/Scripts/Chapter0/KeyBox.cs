using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBox : ItemSystem {

    public Dialogue dialogue;
    public GameObject objPrologueEvent;
    private PrologueEvents prologueEvents;

    
    public override void doAction()
    {

        StartCoroutine("KeyBoxEvent");
        
    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    IEnumerator KeyBoxEvent()
    {
        prologueEvents = objPrologueEvent.GetComponent<PrologueEvents>();
        FindObjectOfType<CharacterMover>().myAnimator.SetBool("IsBack", true);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        yield return StartCoroutine(prologueEvents.InvenTutorial());

        // 아이템 타입을 체크하여 해당 타입에 맞는 슬롯에 들어갈 함수 호출
        //inventorySystem.AddItem(this);
        FindObjectOfType<InventorySystem>().AddItem(this);
        //switch (itemType)
        //{
        //    case ItemType.ACTIVE:
        //        inventorySystem.AddItem(gameObject, inventorySystem.ActiveSlot);
        //        break;

        //    case ItemType.PASSIVE:
        //        inventorySystem.AddItem(gameObject, inventorySystem.PassiveSlot);
        //        break;
        //}
        Debug.Log("아이템 " + gameObject.name + " 습득");

        gameObject.SetActive(true);

        FindObjectOfType<CharacterMover>().myAnimator.SetBool("IsBack", false);
        yield break;
    }
}
