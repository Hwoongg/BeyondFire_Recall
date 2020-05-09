using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryUI : MonoBehaviour
{
    enum MenuType
    {
        Quest,
        Option,
        Return,
        ListMax
    }
    public GameObject[] objMenuList;

    //private void Awake()
    //{
    //    objMenuList = new GameObject[(int)MenuType.ListMax];
    //}

    public void MenuOn(int _MenuNumber)
    {
        objMenuList[_MenuNumber].SetActive(true);

        for(int i=0;i<objMenuList.Length;i++)
        {
            if(i == _MenuNumber)
                continue;

            objMenuList[i].SetActive(false);
        }
    }

}
