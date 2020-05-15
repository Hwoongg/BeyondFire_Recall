using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// 챕터3 시작 표시 + 시작대화
//

public class Chapter3Manager : MonoBehaviour {

    public GameObject StartUI;
    private Text text;
    

    public Dialogue dialogue;
    private void Awake()
    {

    }

    void Start ()
    {
        FindObjectOfType<Fader>().FadeIn();
        StartCoroutine(MainRoutine());
	}

    IEnumerator MainRoutine()
    {
        CharacterMover playerMover = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMover>();

        playerMover.moveType = CharacterMover.MoveType.LOCK;

        StartUI.SetActive(true);

        text = StartUI.GetComponentInChildren<Text>();

        yield return StartCoroutine(StartTextFadeIn());

        yield return new WaitForSeconds(2.0f);

        yield return StartCoroutine(StartTextFadeOut());

        StartUI.SetActive(false);
        
        

        DialogueManager.Instance().StartDialogue(dialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().canvasObj.activeSelf == false);

        playerMover.moveType = CharacterMover.MoveType.COMMANDMOVE;
        yield break;
    }

    IEnumerator StartTextFadeIn()
    {
        float nowLerp = 0.0f;
        float nowAlpha;

        while (true)
        {
            nowAlpha = Mathf.Lerp(0, 1, nowLerp);
            text.color = new Color(1, 1, 1, nowAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime;
            else
                break;

            yield return null;
        }
        yield break;
    }

    IEnumerator StartTextFadeOut()
    {
        float nowLerp = 0.0f;
        float nowAlpha;

        while (true)
        {
            nowAlpha = Mathf.Lerp(1, 0, nowLerp);
            text.color = new Color(1, 1, 1, nowAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime;
            else
                break;

            yield return null;
        }
        yield break;
    }
}
