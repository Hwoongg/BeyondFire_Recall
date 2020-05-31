using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStationManager : MonoBehaviour
{
    int talkCount;

    private void Start()
    {
        talkCount = FindObjectsOfType<FS_NPC>().Length;
    }

    public void CountDown()
    {
        talkCount--;

        if(talkCount <= 0)
        {
            // 씬 종료 연출
        }
    }
}
