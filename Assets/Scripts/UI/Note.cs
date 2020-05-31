using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    // 노트 집합 오브젝트
    GameObject objPageGroup;
    GameObject[] objPages;
    GameObject objButtonGroup;

    bool isOpen; // 현재 열린상태인지. 알림관련 변수

    MissionSlot[] missionSlots;

    private void Awake()
    {
        objPageGroup = transform.GetChild(0).gameObject;
        objButtonGroup = transform.GetChild(1).gameObject;
        objPages = new GameObject[objPageGroup.transform.childCount];
        for(int i=0; i<objPages.Length; i++)
        {
            objPages[i] = objPageGroup.transform.GetChild(i).gameObject;
        }

        missionSlots = objPages[0].GetComponentsInChildren<MissionSlot>();

        
    }

    private void Start()
    {
        Close();
    }

    // 외부 버튼을 통한 여닫기
    public void Open()
    {
        objPageGroup.SetActive(true);
        objButtonGroup.SetActive(true);

        ActivePage(0);
        FindObjectOfType<NoteButton>().OffNotify();
    }
    public void Close()
    {
        objPageGroup.SetActive(false);
        objButtonGroup.SetActive(false);
    }

    // 페이지 버튼에 들어감
    public void ActivePage(int pageNum)
    {
        objPages[pageNum].SetActive(true);

        for(int i=0; i<objPages.Length; i++)
        {
            if (i == pageNum)
                continue;
            objPages[i].SetActive(false);
        }
    }

    // 해당 미션을 슬롯에 추가한다
    public void AddMission(Mission _m)
    {
        // 하위 슬롯들에 같은 미션이 있는지 확인
        for(int i=0; i<missionSlots.Length; i++)
        {
            Mission miss = missionSlots[i].GetMission();
            if (miss == null) continue;

            if (miss.name == _m.name)
            {
                Debug.Log("중복된 미션이 있습니다.");
                return;
            }
        }

        // 빈 슬롯에 미션 등록
        for(int i=0; i<missionSlots.Length; i++)
        {
            if(missionSlots[i].GetMission() == null)
            {
                missionSlots[i].Add(_m);
                FindObjectOfType<NoteButton>().OnNotify();
                return;
            }
        }

        // 다 통과되면 슬롯이 다 꽉찬것
        Debug.Log("미션 슬롯이 꽉 찼습니다.");


    }

    // 해당 이름의 미션 슬롯을 지운다
    public void RemoveMission(string _name)
    {
        for(int i=0; i<missionSlots.Length; i++)
        {
            Mission miss = missionSlots[i].GetMission();
            if (miss == null) continue;

            if (miss.name == _name)
            {
                missionSlots[i].Remove();
            }
        }
    }

    public void SetNotify()
    {
        // 미션 변경 알림
        // ...
    }

    public void OffNotify()
    {
        // 알림 효과 끄기
        // ...
    }

    public void CountUp(string _name)
    {
        for(int i=0; i<missionSlots.Length; i++)
        {
            if (missionSlots[i].GetMission() == null)
                continue;

            if (_name == missionSlots[i].GetMission().name)
                missionSlots[i].CountUp();
        }
    }
}
