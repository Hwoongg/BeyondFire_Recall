using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// 대화 UI 운용 시스템의 주체입니다. 트리거로부터 대화객체를 전달받아 이를 출력해주는 역할
//

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    public GameObject objDialogueBox;
    public Image imgNPCImage;

    // Use this for initialization
    private void Awake()
    {
        sentences = new Queue<string>();
    }
    void Start () {
        //sentences = new Queue<string>();
	}

    

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);

        
        // 대화박스 UI 활성화
        objDialogueBox.SetActive(true);
        imgNPCImage.gameObject.SetActive(true);

        nameText.text = dialogue.name;
      
        imgNPCImage.sprite = dialogue.sprNPCImage;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        //Debug.Log(sentence);

        // 일반 출력
        //dialogueText.text = sentence;

        // 한글자씩 출력
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");

        // 대화 UI 비활성화
        imgNPCImage.gameObject.SetActive(false);
        objDialogueBox.SetActive(false);
    }
}
