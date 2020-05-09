using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpBox : InteractionSystem
{
    // HelpBox 주체 오브젝트
    public GameObject HelpBoxUI;

    enum State
    {
        CoverOn,
        CoverOff,
        GetItem1,
        GetItem2
    }
    State nowState;

    bool isLooping;

    public GameObject cover;
    private Image imgCover;

    public GameObject[] GasMasks;
    private int maskNumber;

    public GameObject objGasMask;
    

	// Use this for initialization
	void Start ()
    {
        nowState = State.CoverOn;
        isLooping = false;
        imgCover = cover.GetComponent<Image>();
	}
	

    // 문짝 열기 이벤트
    public void CoverOff()
    {
        isLooping = true;
        StartCoroutine(CoverFadeOut());
        
    }

    // 문짝 페이드아웃 동작
    IEnumerator CoverFadeOut()
    {
        float nowLerp = 0.0f;
        float nowCoverAlpha;

        while (true)
        {
            nowCoverAlpha = Mathf.Lerp(1, 0, nowLerp);
            imgCover.color = new Color(1, 1, 1, nowCoverAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime;
            else
                break;

            yield return null;
        }

        isLooping = false;
        nowState = State.CoverOff;

        yield break;
    }

    // 방독면 획득 이벤트
    private void GetGasMask()
    {
        if (maskNumber == GasMasks.Length)
            return;

        // 세번째 획득일 때
        

        // 인벤토리에 방독면 추가
        if(FindObjectOfType<InventorySystem>().AddItem(objGasMask.GetComponent<ItemSystem>()))
        {
            GasMasks[maskNumber].SetActive(false);

            maskNumber++;
        }
        
    }

    // 구호용품 이벤트를 버튼 하나로 땡치기 위한 함수
    public void TouchEvent()
    {
        Debug.Log("구호 박스 커버 터치 발생");

        // 루프 중 (페이드 상태 등) 중복 실행 방지
        if (isLooping)
            return;

        switch(nowState)
        {
            case State.CoverOn:
                Debug.Log("커버 제거 이벤트 실행");
                CoverOff();
                break;

            case State.CoverOff:
                Debug.Log("방독면 획득 이벤트 실행");
                GetGasMask();
                break;
        }
        

    }

    public override void doAction()
    {
        HelpBoxUI.SetActive(true);
    }
    public override void upAction()
    {
        doAction();
    }
    public override void downAction()
    {
        doAction();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HelpBoxUI.SetActive(false);
    }
}
