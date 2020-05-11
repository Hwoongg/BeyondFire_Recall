using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] public Text textTalker;
    [SerializeField] public Text textSentence;
    [SerializeField] public Image imgTalker;

    public void ContinueEvent()
    {
        DialogueManager.Instance().DisplayNextSentence();
    }
}
