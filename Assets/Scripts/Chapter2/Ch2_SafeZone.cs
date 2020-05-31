using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch2_SafeZone : SafeZone
{
    public override void doAction()
    {
        if (playerMover.charState == CharacterMover.CharState.RESCUE)
        {
            FindObjectOfType<Note>().CountUp("M1");
        }

        base.doAction();
    }
}
