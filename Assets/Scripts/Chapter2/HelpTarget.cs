using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpTarget : InteractionSystem {

    private GameObject player;
    private Animator playerAnimator;

    public AnimatorOverrideController HelpAnimator;

    public Dialogue dialogue;

    private float LifeTime;
    public enum State
    {
        Normal,
        Limit
    }
    public State state;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        LifeTime = 0;
    }
    private void Update()
    {
        LifeTime += Time.deltaTime;
    }

    public override void doAction()
    {
        // 체류시간이 120이 넘어가면 구조할 수 없다.
        if(state == State.Limit && LifeTime > 120)
        {
            // 구출 불가 대사 출력
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            return;
        }

        playerAnimator.runtimeAnimatorController = HelpAnimator;
        player.GetComponent<CharacterMover>().beforeState = CharacterMover.CharState.RESCUE;
        player.GetComponent<CharacterMover>().charState = CharacterMover.CharState.RESCUE;

        // 방독면 있는지 체크
        if (FindObjectOfType<InventorySystem>().CheckActiveItem("GasMask"))
        {
            
            FindObjectOfType<O2Gauge>().dropIdleParam *= 1.2f;
            FindObjectOfType<InventorySystem>().
                RemoveItem(ItemSystem.ItemType.ACTIVE, "GasMask");

        }
        else
        {
            FindObjectOfType<O2Gauge>().dropIdleParam *= 2;
        }

        player.GetComponent<CharacterMover>().IconStateClear();
        gameObject.SetActive(false);
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
