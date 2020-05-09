using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairEventTrigger : DialogueTrigger {

    bool isNotUse;
    public GameObject objPrologueEvent;
    private PrologueEvents prologueEvent;

	// Use this for initialization
	void Start () {
        isNotUse = true;
        prologueEvent = objPrologueEvent.GetComponent<PrologueEvents>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isNotUse)
        {
            isNotUse = false;
            StartCoroutine(prologueEvent.DuplexStairEvent());
        }
    }
}
