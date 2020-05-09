using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimChangeTest : InteractionSystem {

    public GameObject player;
    private Animator playerAnimator;
    //public RuntimeAnimatorController controller;
    public AnimatorOverrideController ctrl;
    
    
	void Start () {
        playerAnimator = player.GetComponent<Animator>();
	}

    public override void doAction()
    {
        playerAnimator.runtimeAnimatorController = ctrl;
    }

    public override void upAction()
    {
        doAction();
    }
    public override void downAction()
    {
        doAction();
    }
}
