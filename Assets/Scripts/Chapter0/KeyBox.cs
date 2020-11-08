using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBox : ItemSystem {

    public Dialogue dialogue;
    public Dialogue itemOverDlg;
    public GameObject objPrologueEvent;
    private PrologueEvents prologueEvents;
    bool isItemDrop = false;

    NPC_JooIm jooim;
    private void Start()
    {
        jooim = FindObjectOfType<NPC_JooIm>();
    }
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
        // 이미 열쇠를 뱉었거나 주임에게 퀘스트를 받지 않았다면
        if (isItemDrop || !jooim.isReqOver)
        {
            DialogueManager.Instance().StartDialogue(itemOverDlg);
        }
        else
        {
            prologueEvents = objPrologueEvent.GetComponent<PrologueEvents>();
            FindObjectOfType<CharacterMover>().myAnimator.SetBool("IsBack", true);
            DialogueManager.Instance().StartDialogue(dialogue);
            yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

            yield return StartCoroutine(prologueEvents.InvenTutorial());

            // 아이템 타입을 체크하여 해당 타입에 맞는 슬롯에 들어갈 함수 호출
            //inventorySystem.AddItem(this);
            var n = FindObjectOfType<Note>();
            if (n)
            {
                n.RemoveMission("FindKey");

                // 주임에게 퀘스트 받은 적 있어야만 창고진행퀘 얻음
                if (FindObjectOfType<NPC_JooIm>().isReqOver) 
                {
                    n.AddMission("GoToStore");
                }
            }
            var itemSys = FindObjectOfType<InventorySystem>();
            if (itemSys)
            {
                if (!isItemDrop)
                {
                    itemSys.AddItem(this);
                    isItemDrop = true;
                }
            }
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
        }
        yield break;
    }
}
