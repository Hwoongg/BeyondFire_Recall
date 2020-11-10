using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : InteractionSystem
{
    // 아이템 종류 열거형
    public enum ItemType
    {
        ACTIVE = 0,
        PASSIVE,
        DIARY
    }
    public ItemType itemType;

    public String ItemName;
    public Sprite sprSlotImage;
    //public GameObject objInventory;
    //protected InventorySystem inventorySystem;
    [TextArea(3,5)]
    public string ItemInfo;

    private void Start()
    {
        //inventorySystem = objInventory.GetComponent<InventorySystem>();
    }
    public override void doAction()
    {
        // 아이템 타입을 체크하여 해당 타입에 맞는 슬롯에 들어갈 함수 호출
        //switch (itemType)
        //{
        //    case ItemType.ACTIVE:
        //        inventorySystem.AddItem(gameObject, inventorySystem.ActiveSlot);
        //        break;

        //    case ItemType.PASSIVE:
        //        inventorySystem.AddItem(gameObject, inventorySystem.PassiveSlot);
        //        break;
        //}
        FindObjectOfType<InventorySystem>().AddItem(this);
        Debug.Log("아이템 " + gameObject.name + " 습득");
    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    // 사용됐을때 효과
    public virtual void UseItem()
    {

    }
}
