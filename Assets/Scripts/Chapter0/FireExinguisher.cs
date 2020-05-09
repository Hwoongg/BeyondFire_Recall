using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExinguisher : InteractionSystem {

    // 소화기 UI
    public GameObject FireExinUI;

    public override void doAction()
    {
        // 소화기 UI 활성화
        FireExinUI.SetActive(true);
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
        FireExinUI.SetActive(false);
    }
}
