using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter1Manager : MonoBehaviour
{
    [HideInInspector]
    public bool FireLull;

    public List<Chapter1_FireEfx> FireList;
    public GameObject objFadeEfx;
    private FadeEffect fadeEfx;

    public GameObject StartUI;

    private Text text;

    public GameObject PartnerChar;
    public Dialogue PartnerDialogue;
    Vector3 partnerDest;

    private void Awake()
    {
        FireLull = false;
        FireList = new List<Chapter1_FireEfx>();
        fadeEfx = objFadeEfx.GetComponent<FadeEffect>();
        partnerDest = new Vector3(13.79f, -3.549f - 1.8f, 0);
    }

    private void Start()
    {

        StartCoroutine(ChapterStart());
        //objFadeEfx.SetActive(true);
        //fadeEfx.FadeIn();

    }

    public void StartFireLull()
    {
        FireLull = true;

        // 화염 리스트에 진압중 메세지 뿌리기
        for(int i=0; i<FireList.Count; i++)
        {
            FireList[i].lullFire();
        }

    }

    IEnumerator ChapterStart()
    {

        CharacterMover playerMover = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMover>();

        playerMover.moveType = CharacterMover.MoveType.NONE;

        StartUI.SetActive(true);

        text = StartUI.GetComponentInChildren<Text>();

        yield return StartCoroutine(StartTextFadeIn());

        yield return new WaitForSeconds(2.0f);

        yield return StartCoroutine(StartTextFadeOut());

        StartUI.SetActive(false);

        objFadeEfx.SetActive(true);
        fadeEfx.FadeIn();

        yield return StartCoroutine(PartnerEvent());

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

    IEnumerator PartnerEvent()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(PartnerDialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        float nowLerp = 0.0f;
        Vector3 nowPartnerCoord;
        Vector3 startPartnerCoord = PartnerChar.transform.position;
        Animator partnerAnimator = PartnerChar.GetComponent<Animator>();
        while (true)
        {
            partnerAnimator.SetBool("IsWalk", true);
            nowPartnerCoord = Vector3.Lerp(startPartnerCoord, partnerDest, nowLerp);
            PartnerChar.transform.position = nowPartnerCoord;

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime;
            else
                break;

            yield return null;
        }

        partnerAnimator.SetBool("IsWalk", false);
        partnerAnimator.SetBool("IsBack", true);

        yield return new WaitForSeconds(1.0f);
        PartnerChar.SetActive(false);

        yield break;
    }

    
    
	
}
