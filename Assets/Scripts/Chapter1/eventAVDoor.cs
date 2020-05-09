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
            
            StartCoroutine("HasAVKeyEvent");
        }
        else
        {
            StartCoroutine("notHasAVKeyEvent");
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
        FindObjectOfType<DialogueManager>().StartDialogue(dialogueKeyHas);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        GetComponent<NormalDoorInteraction>().enabled = true;
        Destroy(this);
        yield break;
    }

    IEnumerator notHasAVKeyEvent()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogueKeyNotHas);
        yield break;
    }
}
