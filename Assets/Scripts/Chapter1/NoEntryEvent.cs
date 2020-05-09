using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEntryEvent : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    
}
