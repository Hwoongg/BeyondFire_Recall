using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// 아이템 슬롯 클래스
// 아이템 슬롯의 이미지를 송출될 버튼이미지에 연결해주는 것이 관건
//
public class ItemSlot
{
    public GameObject[] ItemArray = new GameObject[3];
    //public List<GameObject> ItemArray;
    public int iItemCount;

    // 초기엔 해당 오브젝트의 Image 컴포넌트 배열을 얻어왔으나, 구조에 안맞음
    public GameObject[] objButtonsUI;

    // //////////////////

    public ItemSlot()
    {
        //ItemArray = new List<GameObject>();

        iItemCount = 0;
        objButtonsUI = new GameObject[5];
        
    }

    public ItemSlot(int length)
    {
        //ItemArray = new List<GameObject>();

        iItemCount = 0;
        objButtonsUI = new GameObject[length];

    }

    static public ItemSlot CreateSlot()
    {
        return new ItemSlot();
    }

}

public class InventorySystem : MonoBehaviour
{
    // 인벤토리 활성화 상태. 클릭가능 판별
    private enum State
    {
        DISABLE,
        ENABLE,
        MOVING
    }
    private State state;
    // 아이템 정보 저장 리스트. 배열로 구현 변경
    //private GameObject[] ActiveItem = new GameObject[3];
    //private int iActiveItemCount;
    //private GameObject[] PassiveItem = new GameObject[3];
    //private int iPassiveItemCount;
    public ItemSlot itemSlots;

    // 수첩창 오브젝트
    public GameObject objDiaryUI;

    // 아이템이 보여질 패널 변수
    public GameObject slotPanel;
    public GameObject objSlotButtonGroup;
    //private GameObject[] objActiveButtonsUI; // 슬롯 클래스에 병합

    // 인벤토리 여닫는 버튼 변수. 활성화 상태 제어용이므로 게임오브젝트형으로 받음
    public GameObject btnInvenCall;
    public GameObject btnLightPanel;
    private Image imageLightPanel; // 빛패널 알파제어용
    public GameObject objPopupUI; // 팝업창 오브젝트


    // 카메라 뷰포트 조작용 카메라 시스템
    private CameraSystem cameraSystem;
    public float moveTime;
    float fTempDeltatime; // 인벤토리 움직임에 필요한 dTime 백업용

    // 움직일 오브젝트들 시작, 도착 좌표 정보
    private Vector2 startPlayercamCoord = new Vector2(0, 0); // 플레이어 카메라
    private Vector2 goalPlayercamCoord = new Vector2(0, 0.1f);
    private Vector3 startSlotCoord = new Vector3(0, 0, 0); // 인벤토리 슬롯
    private Vector3 goalSlotCoord = new Vector3(0, 35, 0);

    public Dialogue dialogue;
    public Sprite nullImage;
    [SerializeField] GameObject prfO2Gauge;

    // /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        itemSlots = new ItemSlot(objSlotButtonGroup.transform.childCount);

        // 슬롯의 버튼 UI 내용물 할당
        // -1은 수첩슬롯 
        for (int i = 0; i < objSlotButtonGroup.transform.childCount; i++)
        {
            itemSlots.objButtonsUI[i] = objSlotButtonGroup.transform.GetChild(i).gameObject;
        }

    }
    private void Start()
    {
        cameraSystem = FindObjectOfType<CameraSystem>();

        state = State.DISABLE;
        imageLightPanel = btnLightPanel.GetComponent<Image>();
        

        


        // 슬롯 패널의 이미지 컴포넌트 참조 취득
        // 슬롯 오브젝트가 자신의 슬롯이미지 오브젝트를 보여질 버튼UI 이미지 컴포넌트에 링크
        // ex) 액티브 0번 버튼 UI의 이미지 = 액티브 0번슬롯 오브젝트의 슬롯이미지
    }

    // 인벤토리 호출 함수. 코루틴은 자체적으로 버튼이벤트에서 실행 불가능하므로 래핑.
    public void OpenInventory()
    {
        // 중복 실행 방지 코드
        if (state != State.DISABLE) // DISABLE 일때만 실행
            return;

        StartCoroutine(OpenInventoryMove());
    }
    // 인벤토리 호출 동작 함수 본체. 코루틴
    IEnumerator OpenInventoryMove()
    {
        // 인벤토리 활성화 상태 체크. 중복 루틴 발생 방지 코드.
        if (state != State.DISABLE) yield break; // DISABLE 일때만 실행

        state = State.MOVING; // 무빙 상태로 전환
        fTempDeltatime = Time.deltaTime;
        Time.timeScale = 0;
        btnInvenCall.SetActive(false); // 열기버튼 비활성화
        btnLightPanel.SetActive(true); // 닫기버튼, 밝기패널 활성화

        // 플레이어 카메라 위치 체크. 상하단 상태변수 생성.
        bool isPlayercamDown /*= cameraSystem.playerCamera.rect.y == 0 ? true : false*/;
        if (cameraSystem.playerCamera.rect.y == 0)
            isPlayercamDown = true;
        else isPlayercamDown = false;



        // 보간 이동을 위한 데이터 세팅
        float nowLerp = 0.0f; // 보간될 거리 매개변수
        Vector2 nowPlayercamCoord; // 현재 보간된 좌표들 저장용 변수
        Vector3 nowSlotCoord;
        float nowLightpanelAlpha;

        // 움직임 시행 루틴
        while (true)
        {
            if (isPlayercamDown) // 플레이어 카메라가 하단에 위치한다면
            {

                // 뷰포트 점차 상승. 좌표 보간 진행
                nowPlayercamCoord = Vector2.Lerp(startPlayercamCoord, goalPlayercamCoord, nowLerp);
                // 보간된 뷰포트 좌표 대입. rect 구조체의 y값만 수정할 방법이 필요.
                cameraSystem.playerCamera.rect =
                    new Rect(nowPlayercamCoord.x, nowPlayercamCoord.y,
                    cameraSystem.playerCamera.rect.width, cameraSystem.playerCamera.rect.height);

                // 상단카메라 비추는 좌표 변경
                // ...

            }

            // 인벤토리 슬롯 위치 상승 보간, 대입
            nowSlotCoord = Vector3.Lerp(startSlotCoord, goalSlotCoord, nowLerp);
            slotPanel.transform.localPosition = nowSlotCoord;

            // 빛 패널 알파값 상승 보간, 대입
            nowLightpanelAlpha = Mathf.Lerp(0, 0.6f, nowLerp);
            imageLightPanel.color = new Color(0, 0, 0, nowLightpanelAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 이동완료
                nowLerp += fTempDeltatime / moveTime;

            else
                break;


            yield return null;

        }

        // 이동 완료 후
        state = State.ENABLE;
        Debug.Log("인벤토리 오픈 완료");

        yield break;
    }

    // 인벤토리 닫기 래핑함수
    public void CloseInventory()
    {
        Debug.Log("인벤토리 닫기 실행");
        if (state != State.ENABLE) // 활성화상태가 아니면 종료
            return;

        StartCoroutine("CloseInventoryMove");
    }
    // 인벤토리 닫기 본체 코루틴
    IEnumerator CloseInventoryMove()
    {
        // 중복 루틴 발생 방지 코드
        if (state != State.ENABLE) // ENABLE상태가 아니면 종료. 즉, ENABLE일때만 실행
            yield break;

        state = State.MOVING; // 무빙상태로 전환


        // 플레이어 카메라 위치 체크. 상하단 상태변수 생성.
        bool isPlayercamDown /*= cameraSystem.playerCamera.rect.y == 0 ? true : false*/;
        if (cameraSystem.playerCamera.rect.y == 0.1f)
            isPlayercamDown = true;
        else isPlayercamDown = false;


        // 보간 이동을 위한 데이터 세팅
        float nowLerp = 1.0f; // 보간될 거리 매개변수
        Vector2 nowPlayercamCoord; // 현재 보간된 좌표들 저장용 변수
        Vector3 nowSlotCoord;
        float nowLightpanelAlpha;

        // 움직임 시행 루틴
        while (true)
        {
            if (isPlayercamDown) // 플레이어 카메라가 하단에 위치한다면
            {

                // 뷰포트 점차 상승. 좌표 보간 진행
                nowPlayercamCoord = Vector2.Lerp(startPlayercamCoord, goalPlayercamCoord, nowLerp);
                // 보간된 뷰포트 좌표 대입. rect 구조체의 y값만 수정할 방법이 필요.
                cameraSystem.playerCamera.rect =
                    new Rect(nowPlayercamCoord.x, nowPlayercamCoord.y,
                    cameraSystem.playerCamera.rect.width, cameraSystem.playerCamera.rect.height);

                // 상단카메라 비추는 좌표 변경
                // ...

            }

            // 인벤토리 슬롯 위치 하강 보간, 대입
            nowSlotCoord = Vector3.Lerp(startSlotCoord, goalSlotCoord, nowLerp);
            slotPanel.transform.localPosition = nowSlotCoord;

            // 빛 패널 알파값 하강 보간, 대입
            nowLightpanelAlpha = Mathf.Lerp(0, 0.6f, nowLerp);
            imageLightPanel.color = new Color(0, 0, 0, nowLightpanelAlpha);


            if (nowLerp > 0.0f) // 보간수치가 0 이상이면 하강, 넘어가면 이동완료
                nowLerp -= fTempDeltatime / moveTime;
            else
                break;

            yield return null;

        }

        btnLightPanel.SetActive(false); // 바로 사라지면 안됨. 시작조건이 안전하니 코드를 마지막으로 이동함
        btnInvenCall.SetActive(true);
        state = State.DISABLE;
        Time.timeScale = 1;
        Debug.Log("인벤토리 닫기 완료");

        yield break;

    }

    // 슬롯에 등록된 아이템 비추기.. AddItem에서 이미지 교체를 통해 필요성 사라질 듯
    void ItemView()
    {

    }

    // 아이템 데이터 추가 함수
    public bool AddItem(GameObject _objItem, ItemSlot _itemSlot)
    {
        Debug.Log("아이템 추가 함수 호출");

        // 꽉차있는경우
        if(_itemSlot.iItemCount > 5)
        {
            DialogueManager.Instance().StartDialogue(dialogue);
            return false;
        }

        
        //// 습득된 아이템 인벤토리 데이터로 옮겨짐.
        //_itemSlot.ItemArray[_itemSlot.iItemCount] = _objItem;
        ////_itemSlot.ItemArray.Add(_objItem);

        //// 현재 카운트의 슬롯 이미지 교체
        //_itemSlot.objButtonsUI[_itemSlot.iItemCount].GetComponent<Image>().sprite
        //    = _objItem.GetComponent<ItemSystem>().sprSlotImage;

        // itemCount 위치가 아닌 순회를 돌아 들어갈 위치를 검색하여 교체한다.
        for(int i=0; i<_itemSlot.ItemArray.Length; i++)
        {
            if(_itemSlot.ItemArray[i] == null)
            {
                _itemSlot.ItemArray[i] = _objItem;
                _itemSlot.objButtonsUI[i].GetComponent<Image>().sprite = 
                    _objItem.GetComponent<ItemSystem>().sprSlotImage;

                break;
            }
        }

        // 개수 카운트 상승
        _itemSlot.iItemCount++;

        // 아이템 감추기. 비활성화로 구현. 빼야될 필요 있음
        _objItem.SetActive(false);

        return true;
    }

    public bool AddItem(ItemSystem _ItemSys)
    {
        bool isSucsess = false;

        switch(_ItemSys.itemType)
        {
            case ItemSystem.ItemType.ACTIVE: // 아이템 타입 구분 제거
            case ItemSystem.ItemType.PASSIVE:
                isSucsess = AddItem(_ItemSys.gameObject, itemSlots);
                break;
        }

        return isSucsess;
    }

    // 버튼을 통해 호출되는 액티브 아이템 사용 함수
    public void UseActiveItem(int slotNum)
    {
        // 인벤토리 상태 체크
        if (state != State.ENABLE) return;

        // 슬롯 번호를 아이템 타입과 변환
        ItemSystem.ItemType itemType;
        if (slotNum > 2)
            itemType = ItemSystem.ItemType.PASSIVE;
        else
            itemType = ItemSystem.ItemType.ACTIVE;

        slotNum = slotNum % 3;
        // 아이템 사용. 매개변수로 슬롯 판단
        //switch(itemType)
        //{
        //    case ItemSystem.ItemType.ACTIVE:
        //        //ItemSystem itemSystem = ActiveItem[slotNum].GetComponent<ItemSystem>();
        //        ActiveItem[slotNum].GetComponent<ItemSystem>().UseItem();
        //        break;


        //    case ItemSystem.ItemType.PASSIVE:
        //        PassiveItem[slotNum].GetComponent<ItemSystem>().UseItem();
        //        break;

        //}
    }

    // 문자열을 입력받아 해당 패시브 아이템을 소지중인지 리턴하는 함수
    public bool CheckPassiveItem(string _itemName)
    {
        for(int i = 0; i < 3; i++)
        {
            if (itemSlots.ItemArray[i] == null) // 비어있는 슬롯일 때 Pass
                continue;

            if(itemSlots.ItemArray[i].GetComponent<ItemSystem>().ItemName == _itemName)
            {
                return true;
            }

        }


        return false;
    }

    public bool CheckActiveItem(string _itemName)
    {
        for (int i = 0; i < 3; i++)
        {
            if (itemSlots.ItemArray[i] == null) // 비어있는 슬롯일 때 Pass
                continue;

            if (itemSlots.ItemArray[i].GetComponent<ItemSystem>().ItemName == _itemName)
            {
                return true;
            }

        }
        
        return false;
    }

    // 수첩 호출 함수
    public void OpenDiary()
    {
        objDiaryUI.SetActive(true);
        objDiaryUI.GetComponent<DiaryUI>().MenuOn(0);
    }
    public void CloseDiary()
    {
        objDiaryUI.SetActive(false);
    }


    public void ItemPopupOn(int num)
    {
        // 해당 슬롯 아이템 정보가 들어있는지 체크
        //if (num < 3)
        //{
            if (itemSlots.ItemArray[num] == null)
                return;
            else
            {
                //ItemPopup.SetActive(true);
                objPopupUI.GetComponentInChildren<Text>().text =
                    itemSlots.ItemArray[num].GetComponent<ItemSystem>().ItemInfo;
            }
        //}
        //else
        //{
        //    if (PassiveSlot.ItemArray[num % 3] == null)
        //        return;
        //    else
        //    {
        //        //ItemPopup.SetActive(true);
        //        ItemPopup.GetComponentInChildren<Text>().text =
        //            PassiveSlot.ItemArray[num % 3].GetComponent<ItemSystem>().ItemInfo;
        //    }
        //}
        

        // 해당 슬롯의 아이템 팝업정보로 교체
        // ...

        // 팝업창 활성화
        objPopupUI.SetActive(true);

    }

    public void ItemPopupOff()
    {
        // 슬롯 아이템 정보 유무 체크
        // ...

        // 슬롯 이미지 크기 축소
        // ...

        // 팝업창 비활성화
        objPopupUI.SetActive(false);
    }

    public int CheckItemCount(ItemSystem.ItemType _itemType, string _itemName)
    {
        int count = 0;

        ItemSlot slot;
        if(_itemType == ItemSystem.ItemType.ACTIVE)
        {
            slot = itemSlots;
        }
        else
        {
            slot = itemSlots;
        }

        for (int i = 0; i < 3; i++)
        {
            if (slot.ItemArray[i] == null) // 비어있는 슬롯일 때 Pass
                continue;

            if (slot.ItemArray[i].GetComponent<ItemSystem>().ItemName == _itemName)
            {
                count++;
            }

        }

        return count;
    }
    public void RemoveItem(ItemSystem.ItemType _itemType, string _itemName)
    {
        ItemSlot slot;
        if (_itemType == ItemSystem.ItemType.ACTIVE)
        {
            slot = itemSlots;
        }
        else
        {
            slot = itemSlots;
        }

        for (int i = 0; i < 3; i++)
        {
            if (slot.ItemArray[i] == null) // 비어있는 슬롯일 때 Pass
                continue;

            if (slot.ItemArray[i].GetComponent<ItemSystem>().ItemName == _itemName)
            {
                //slot.ItemArray.Remove(slot.ItemArray[i]);
                //Destroy(slot.ItemArray[i]);
                slot.ItemArray[i] = null;                
                slot.objButtonsUI[i].GetComponent<Image>().sprite = nullImage;
                slot.iItemCount--;
                break;
            }

        }
    }

    public void CreateO2Gauge()
    {
        GameObject o2 = Instantiate(prfO2Gauge, itemSlots.objButtonsUI[0].transform);
        // 아이템 등록처리
        AddItem(o2.GetComponent<ItemSystem>());
        o2.SetActive(true);
    }
}
