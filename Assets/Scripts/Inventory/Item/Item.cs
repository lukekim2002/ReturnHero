using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment = 1, // 장비
    Accessory, // 장신구
    Misc // 기타
}

[System.Serializable]
public class Item
{
    public int itemID = 0; // 아이템 ID
    public int itemCount = 0;

    public Item()
    {
        itemID = 0;
        itemCount = 0;
    }
}
