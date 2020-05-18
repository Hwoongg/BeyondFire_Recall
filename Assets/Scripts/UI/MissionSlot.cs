using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionSlot : MonoBehaviour
{
    Text desc;
    Text count;

    Mission mission;

    private void Awake()
    {
        mission = null;
        desc = transform.Find("Text_Desc").GetComponent<Text>();
        count = transform.Find("Text_Count").GetComponent<Text>();

        desc.text = " ";
        count.text = " ";
    }
    
    public void Add(Mission _m)
    {
        mission = _m;

        desc.text = mission.description;

        if(mission.isCountable)
        {
            Counting();
        }
    }

    public void Remove()
    {
        mission = null;
        desc.text = " ";
        count.text = " ";
    }

    public void CountUp()
    {
        mission.nowCount++;
        Counting();
    }

    void Counting()
    {
        count.text ="(" + mission.nowCount.ToString() + "/" + mission.maxCount.ToString() + ")";
    }

    public Mission GetMission()
    {
        return mission;
    }
}
