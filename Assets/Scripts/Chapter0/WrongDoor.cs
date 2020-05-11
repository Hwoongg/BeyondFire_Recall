using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongDoor : InteractionSystem
{
    public GameObject objInvenSys;
    public GameObject objFadeEfx;
    private FadeEffect fadeEfx;
    private InventorySystem invenSys;

    public GameObject playerCharacter;

    public GameObject TestFire;

    public Dialogue dialogue1;
    public Dialogue dialogue2;

    public GameObject ToiletDoor;
    public GameObject FireEx;

    public GameObject objFireFocus;
    public GameObject objFireFight;

    public GameObject BGM;
    public AudioClip AlarmBGM;

    public RuntimeAnimatorController changeAnimator;
    private Animator playerAnimator;


    // Use this for initialization
    void Start()
    {
        invenSys = FindObjectOfType<InventorySystem>().GetComponent<InventorySystem>();
        fadeEfx = objFadeEfx.GetComponent<FadeEffect>();
        playerAnimator = playerCharacter.GetComponent<Animator>();
    }

    public override void doAction()
    {
        Debug.Log("아이템 체크 시도");
        if (invenSys.CheckPassiveItem("Key"))
        {
            Debug.Log("아이템 체크 성공");
            StartCoroutine("NextEventSetting");
        }

    }

    public override void upAction()
    {
        doAction();
    }
    public override void downAction()
    {
        doAction();
    }

    IEnumerator NextEventSetting()
    {
        // 열쇠가 안맞는 대사 출력
        DialogueManager.Instance().StartDialogue(dialogue1);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        // 페이드 아웃
        fadeEfx.FadeOut();

        yield return new WaitForSeconds(1.0f);
        // 캐릭터 우측 동아리실로 좌표 이동
        playerCharacter.transform.position = new Vector3(63.7f, -7.0f, 0);
        // 우측방향, 앉기 세팅
        playerCharacter.transform.localScale = new Vector3(1, 1, 1);
        playerCharacter.GetComponent<CharacterMover>().myAnimator.SetBool("IsSit", true);
        FindObjectOfType<CameraSystem>().changeViewport();

        // 불+연기 세팅
        TestFire.SetActive(true);

        // 지하 남자화장실 문 트리거 제거
        //ToiletDoor.GetComponent<ToiletDoorInteraction>().enabled = false;
        Destroy(ToiletDoor.GetComponent<ToiletDoorInteraction>());
        Destroy(ToiletDoor.GetComponent<Collider2D>());
        FireEx.GetComponent<FireExinguisher>().enabled = false;
        FireEx.GetComponent<ItemSystem>().enabled = true;
        // 화재 발견 트리거 활성화
        objFireFocus.SetActive(true);

        // 화재 진압 트리거 활성화
        objFireFight.SetActive(true);

        // BGM 교체
        BGM.GetComponent<AudioSource>().clip = AlarmBGM;
        BGM.GetComponent<AudioSource>().Play();

        // 애니메이터 교체
        playerAnimator.runtimeAnimatorController = changeAnimator;



        yield return new WaitForSeconds(2.0f);
        fadeEfx.FadeIn();


        // 뭔가 이상함을 느낀 대사
        DialogueManager.Instance().StartDialogue(dialogue2);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        // 기립 상태
        playerCharacter.GetComponent<CharacterMover>().myAnimator.SetBool("IsSit", false);

        yield break;
    }

}
