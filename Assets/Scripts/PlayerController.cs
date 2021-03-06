﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterMover mover;
    GetInteraction interaction;

    GameObject controllCanvas;

    private void Awake()
    {
        controllCanvas = transform.GetChild(0).gameObject;

        // 씬 시작시 스스로 캔버스 활성화.
        controllCanvas.SetActive(true);
    }
    private void Start()
    {
        
        mover = FindObjectOfType<CharacterMover>();
        interaction = mover.transform.GetComponentInChildren<GetInteraction>();
        
    }

    public void InputLeft()
    {
        mover.MoveLeft();

    }
    public void InputRight()
    {
        mover.MoveRight();
    }

    public void InputUp()
    {
        interaction.upAction();
    }

    public void InputDown()
    {
        interaction.downAction();
    }

    public void IsActive(bool _b)
    {
        controllCanvas.SetActive(_b);
    }
}
