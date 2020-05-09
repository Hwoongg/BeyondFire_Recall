using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueDoorTrigger : DialogueTrigger
{
    bool isNotUse;
    public GameObject objPrologueEvent;
    private PrologueEvents prologueEvent;

    private void Start()
    {
        isNotUse = true;
        prologueEvent = objPrologueEvent.GetComponent<PrologueEvents>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isNotUse)
        {
            isNotUse = false;
            StartCoroutine(prologueEvent.LibraryDoorEvent());
        }
    }
}
