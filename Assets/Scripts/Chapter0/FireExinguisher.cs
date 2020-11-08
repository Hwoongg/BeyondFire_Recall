using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExinguisher : InteractionSystem {

    // 소화기 UI
    public GameObject FireExinUI;
    bool isCheck = false;

    private void Update()
    {
        
    }
    public override void doAction()
    {
        // 소화기 UI 활성화
        if(FireExinUI)
            FireExinUI.SetActive(true);
        if (!isCheck)
        {
            FindObjectOfType<Note>().CountUp("CheckFireEx");
            isCheck = true;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 소화기 UI 비활성화
        if (FireExinUI)
        {
            FireExinUI.SetActive(false);
        }
    }
}
