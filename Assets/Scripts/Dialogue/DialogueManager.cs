using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// 대화 UI 운용 시스템의 주체입니다. 트리거로부터 대화객체를 전달받아 이를 출력해주는 역할
//

public class DialogueManager : MonoBehaviour
{
    // 프리팹 내부 요소들. Box 클래스로부터 복사받는다.
    // 참조 부분이 너무 많아서 변경에 어려운 상태
    [HideInInspector] public Text textTalker;
    [HideInInspector] public Text textSentence;
    [HideInInspector] public Image imgTalker;

    // 출력할 대사
    private Queue<string> sentences;
    

    static DialogueManager instance;
    static GameObject container;
    GameObject canvasPrf; 
    [HideInInspector] public GameObject canvasObj; // 참조부분 너무 많아서 변경어려움
    DialogueBox dialogueBox;

    bool isTyping = false;
    [HideInInspector] public bool isSkip = false;
    
    static public DialogueManager Instance()
    {
        if(instance == null)
        {
            container = new GameObject();
            instance = container.AddComponent<DialogueManager>();
        }

        return instance;
    }
    private void Awake()
    {
        sentences = new Queue<string>();

        DontDestroyOnLoad(gameObject);
        instance = this;
        System.Type ty = GetType();
        gameObject.name = ty.ToString();

        canvasPrf = Resources.Load<GameObject>("Prefabs/Canvas_Dialogue");
        // 매니저의 자식으로 캔버스 부착
        canvasObj = Instantiate<GameObject>(canvasPrf, transform);
        dialogueBox = canvasObj.GetComponent<DialogueBox>();

        // 캔버스의 대화상자 오브젝트
        Transform box = canvasObj.transform.Find("DialogueBox");
        textTalker = dialogueBox.textTalker;
        textSentence = dialogueBox.textSentence;
        imgTalker = dialogueBox.imgTalker;

        canvasObj.SetActive(false);
    }
    

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);

        
        // 대화박스 UI 활성화. 씬으로 교체?
        canvasObj.SetActive(true);
        imgTalker.gameObject.SetActive(true);

        textTalker.text = dialogue.name;
      
        imgTalker.sprite = dialogue.sprNPCImage;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (!isTyping)
        {
            if (sentences.Count == 0)
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
        else
        {
            isSkip = true;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        textSentence.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            if (isSkip)
            {
                // 문장 즉시 완성
                textSentence.text = sentence;
                isSkip = false;
                break;
            }
            else
            {
                textSentence.text += letter;
            }
            yield return null;
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");

        // 대화 UI 비활성화
        imgTalker.gameObject.SetActive(false);
        canvasObj.SetActive(false);
    }
}
