using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<Dictionary<string, object>> itemSet;
    // public List<Item> items = new List<Item>();

    private string _itemName;
    private int _itemID;
    private string _itemDesc;
    private ItemType _itemType;
    private int _offensePower;
    private int _defensePower;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        itemSet = CSVReader.Read("CSV/Item/Item");

        //for (int index = 0; index < itemSet.Count; index++)
        //{
        //    _itemName = (string)itemSet[index]["Name"];
        //    _itemID = (int)itemSet[index]["ID"];
        //    _itemDesc = (string)itemSet[index]["Desc"];
        //    _itemType = (ItemType)itemSet[index]["ItemType"];
        //    _offensePower = (int)itemSet[index]["OffensePower"];
        //    _defensePower = (int)itemSet[index]["DefensePower"];

        //    AddItemInList(_itemName, _itemID, _itemDesc, _itemType, _offensePower, _defensePower);
        //}
    }
    

    // 아이템 데이터베이스에 아이템을 추가한다.
    //private void AddItemInList(string mItemName, int mItemID, string mItemDesc, ItemType mItemType, int mOffensePower, int mDefensePower)
    //{
    //    items.Add(new Item(mItemName, mItemID, mItemDesc, mItemType, mOffensePower, mDefensePower));
    //}
    
    public Dictionary<string, object> ThrowDataIntoContainer(int itemID)
    {
        for (int index = 0; index < itemSet.Count; index++)
        {
            if (itemID == (int)itemSet[index]["ID"])
            {
                return this.itemSet[index];
            }
        }

        return null;
    }
}
