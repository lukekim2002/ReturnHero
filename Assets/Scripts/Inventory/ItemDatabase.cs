using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        AddItemInList("Sword", 1, "Normal Sword", ItemType.Equipment, 15, 0);
        AddItemInList("Bronze Ring", 2, "Normal Ring", ItemType.Accessory, 0, 5);
        AddItemInList("Potion", 3, "Healing hp 10.", ItemType.Misc, 0, 0);

        for (int i = 0; i < items.Count; i++)
        {
            items[i].ShowItem();
        }
    }
    

    // 아이템 데이터베이스에 아이템을 추가한다.
    private void AddItemInList(string mItemName, int mItemID, string mItemDesc, ItemType mItemType, int mOffensePower, int mDefensePower)
    { 
        items.Add(new Item(mItemName, mItemID, mItemDesc, mItemType, mOffensePower, mDefensePower, Resources.Load<Sprite>("ItemImages/" + mItemName)));
    }
}
