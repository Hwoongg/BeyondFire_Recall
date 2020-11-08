using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventAVDoor :InteractionSystem
{

    public Dialogue dialogueKeyHas;
    public Dialogue dialogueKeyNotHas;
    
    public override void doAction()
    {
        if(FindObjectOfType<InventorySystem>().CheckPassiveItem("AVKey"))
        {
            
            StartCoroutine(HasAVKeyEvent());
        }
        else
        {
            StartCoroutine(notHasAVKeyEvent());
            //StartCoroutine("HasAVKeyEvent");
        }
    }
    public override void upAction()
    {
        doAction();
    }
    public override void downAction()
    {
        doAction();
    }

    IEnumerator HasAVKeyEvent()
    {
        DialogueManager.Instance().StartDialogue(dialogueKeyHas);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        GetComponent<NormalDoorInteraction>().enabled = true;
        FindObjectOfType<GetInteraction>().triggerInteraction
            = GetComponent<NormalDoorInteraction>();
        Destroy(this);
        yield break;
    }

    IEnumerator notHasAVKeyEvent()
    {
        DialogueManager.Instance().StartDialogue(dialogueKeyNotHas);
        FindObjectOfType<Note>().AddMission("FindAVKey");
        yield break;
    }
}
