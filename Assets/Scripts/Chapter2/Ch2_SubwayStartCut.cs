using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//
// 챕터2 시작 표시 + 지하철 진입 전 CG + 대화 씬 스크립트
//

public class Ch2_SubwayStartCut : MonoBehaviour {

    public GameObject objFadeEfx;
    //private FadeEffect fadeEfx;

    public GameObject StartUI;
    private Text text;

    public GameObject objAfterCG;

    public Dialogue BeforeDialogue;
    public Dialogue AfterDialogue;

    DialogueManager dialogueManager;

    // Use this for initialization
    void Start ()
    {
        dialogueManager = DialogueManager.Instance();
        StartCoroutine(MainRoutine());
	}
	

    IEnumerator MainRoutine()
    {
        StartUI.SetActive(true);

        text = StartUI.GetComponentInChildren<Text>();

        yield return StartCoroutine(StartTextFadeIn());

        yield return new WaitForSeconds(2.0f);

        yield return StartCoroutine(StartTextFadeOut());

        StartUI.SetActive(false);

        objFadeEfx.SetActive(true);
        objFadeEfx.GetComponent<FadeEffect>().FadeIn();

         
        dialogueManager.StartDialogue(BeforeDialogue);
        yield return new WaitUntil(() => dialogueManager.canvasObj.activeSelf == false);


        // 이미지 추가 활성화
        objAfterCG.SetActive(true);

        dialogueManager.StartDialogue(AfterDialogue);
        yield return new WaitUntil(() => dialogueManager.canvasObj.activeSelf == false);


        // 다 끝나면 페이드아웃
        objFadeEfx.GetComponent<FadeEffect>().FadeOut();

        yield return new WaitForSeconds(2.0f);
        // 다음 씬으로
        SceneManager.LoadScene("Chapter_2_Subway_v3");

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
