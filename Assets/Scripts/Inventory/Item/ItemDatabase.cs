using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment = 1, // 장비
    Accessory, // 장신구
    Misc // 기타
}

public class ItemDatabase : MonoBehaviour
{
    #region PRIVATE
    private string _itemName;
    private int _itemID;
    private string _itemDesc;
    private ItemType _itemType;
    private int _offensePower;
    private int _defensePower;
    #endregion

    #region PUBLIC
    public static ItemDatabase instance;
    public List<Dictionary<string, object>> itemSet;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        itemSet = CSVReader.Read("CSV/Item/Item");
    }
    
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
