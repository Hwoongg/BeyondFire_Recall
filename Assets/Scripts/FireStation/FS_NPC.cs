using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FS_NPC : InteractionSystem
{
    [SerializeField] Dialogue dialogue;
    bool isReady;

    private void Start()
    {
        isReady = true;
    }
    public override void doAction()
    {
        if (isReady)
        {
            DialogueManager.Instance().StartDialogue(dialogue);
            isReady = false;
            FindObjectOfType<FireStationManager>().CountDown();
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
}
