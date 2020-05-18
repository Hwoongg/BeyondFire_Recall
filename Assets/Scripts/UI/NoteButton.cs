using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NoteButton : MonoBehaviour
{
    bool isNotify;

    float nowTime;
    [SerializeField] float blinkTime;

    GameObject notiText;
    GameObject notiOutline;
    bool outlineActive;

    private void Awake()
    {
        nowTime = 0;
        isNotify = false;

        notiText = transform.Find("NotiText").gameObject;
        notiOutline = transform.Find("NotiOutLine").gameObject;
    }
    
    private void Update()
    {
        if(isNotify)
        {
            notiText.SetActive(true);

            nowTime += Time.deltaTime;
            if (nowTime > blinkTime)
            {
                nowTime = 0;
                outlineActive = !outlineActive;
            }

            notiOutline.SetActive(outlineActive);
        }
        else
        {
            notiText.SetActive(false);
            notiOutline.SetActive(false);
        }
    }

    public void OnNotify()
    {
        isNotify = true;
    }

    public void OffNotify()
    {
        isNotify = false;
    }
}
