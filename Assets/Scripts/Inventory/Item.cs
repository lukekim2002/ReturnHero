using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment, // 장비
    Accessory, // 장신구
    Misc // 기타
}

[System.Serializable]
public class Item {

    public string itemName; // 아이템 이름
    public int itemID; // 아이템 ID
    public string itemDesc; // 아이템 설명
    public ItemType itemType; // 아이템 타입
    public int offensePower; // 아이템 공격력
    public int defensePower; // 아이템 방어력
    public int itemCount; // 아이템 개수

    public Item(string mItemName, int mItemID, string mItemDesc, ItemType mItemType, int mOffensePower, int mDefensePower, int mItemCount)
    {
        itemName = mItemName;
        itemID = mItemID;
        itemDesc = mItemDesc;
        itemType = mItemType;
        offensePower = mOffensePower;
        defensePower = mDefensePower;
        itemCount = mItemCount;
    }

    public Item()
    { }

    public void ShowItem()
    {
        Debug.Log("Name : " + itemName);
        Debug.Log("ID : " + itemID);
        Debug.Log("Desc : " + itemDesc);
        Debug.Log("Type : " + itemType);
        Debug.Log("OffensePower : " + offensePower);
        Debug.Log("DefensePower : " + defensePower);
        Debug.Log("------------------------------");
    }
}
