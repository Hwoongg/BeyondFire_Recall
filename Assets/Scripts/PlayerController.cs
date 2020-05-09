using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterMover mover;
    GetInteraction interaction;

    GameObject controllCanvas;

    private void Start()
    {
        controllCanvas = transform.GetChild(0).gameObject;
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
