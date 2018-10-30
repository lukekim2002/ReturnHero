using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    #region PRIVATE
    #endregion

    #region PUBLIC
    public static Inventory instance;
    public RectTransform slot;
    public List<Slot> slotScripts = new List<Slot>();

    public const int slot_X = 6;
    public const int slot_Y = 4;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int y = 0; y < slot_Y; y++)
        {
            for (int x = 0; x < slot_X; x++)
            {
                RectTransform newSlotRect = Instantiate(slot);
                newSlotRect.SetParent(this.transform.GetChild(1));

                newSlotRect.localScale = new Vector2(1, 1);

                Vector2 newSlotPos = slot.position;

                newSlotPos.x = newSlotPos.x + (x * 45f);
                newSlotPos.y = newSlotPos.y - (y * 50f);
                newSlotRect.GetComponent<RectTransform>().anchoredPosition = newSlotPos;

                newSlotRect.name = "Slot " + ((y * slot_X) + x);

                slotScripts.Add(newSlotRect.GetComponent<Slot>());
                newSlotRect.GetComponent<Slot>().slotNum = y * slot_X + x;
            }
        }
        AddItemInInventory(1);
        AddItemInInventory(2);
        AddItemInInventory(3);
    }

    // Item 추가
    public void AddItemInInventory(int mItemID)
    {
        for (int i = 0; i < slotScripts.Count; i++)
        {
            if(slotScripts[i].item.itemID == 0)
            {
                slotScripts[i].item = ItemDatabase.instance.items[mItemID - 1];
                // 인벤토리에 아이템 이미지를 뿌림
                slotScripts[i].SetSlotImage(mItemID);
                
                print(slotScripts[i].item.itemName);
                break;
            }
        }
    }

    // Item 삭제
    public void RemoveItemInInventory(int mItemID)
    {
        
    }

    // 포인터 올렸을 때 아이템 정보 뿌려줌
    public void PointerOnItem()
    {

    }
}
