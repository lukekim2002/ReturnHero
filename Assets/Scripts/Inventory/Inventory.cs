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
    public Image itemDescBackGround;

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

                var slotComponent = newSlotRect.GetComponent<Slot>();

                // 각 Slot 마다 보여줄 ITemDescBackGround Pivot을 Slot 안에 담아둠
                // x (0~2) : 0 / (3~5) : 1
                // y는 0 -> 0.33 -> 0.66 -> 1
                slotComponent.itemDescBackGroundPivot.x = (x > 2) ? 1 : 0;
                slotComponent.itemDescBackGroundPivot.y = 1f - (y * (1f / (slot_Y - 1)));

                slotScripts.Add(slotComponent);
                newSlotRect.GetComponent<Slot>().slotNum = y * slot_X + x;
            }
        }

        ItemAddTestMethodCall();
    }

    // Item 추가
    public void AddItemInInventory(int mItemID)
    {
        for (int i = 0; i < slotScripts.Count; i++)
        {
            // 슬롯에 들어간 똑같은 아이템이 하나 이상 있다면
            if (slotScripts[i].item.itemID == mItemID)
            {
                // 슬롯에 들어간 똑같은 아이템이 5개 이하라면
                if (slotScripts[i].itemCount < 5)
                {
                    slotScripts[i].itemCount += 1;
                    slotScripts[i].SetSlotItemCount();
                    print(slotScripts[i].item.itemName + " : " + slotScripts[i].itemCount);
                    print("index : " + i);

                    break;
                }
                // 슬롯에 들어간 똑같은 아이템이 5개이라면
                else if (slotScripts[i].itemCount == 5)
                {
                    continue;
                }
            }
            // 슬롯에 들어간 똑같은 아이템이 하나도 없다면
            else if (slotScripts[i].item.itemID == 0)
            {
                slotScripts[i].item = ItemDatabase.instance.items[mItemID - 1];
                slotScripts[i].itemCount = 1;
                // 인벤토리에 아이템 이미지를 뿌림
                slotScripts[i].SetSlotImage(mItemID);

                break;
            }
        }
    }

    public void ItemAddTestMethodCall()
    {
        AddItemInInventory(1);
        AddItemInInventory(1);
        AddItemInInventory(1);
        AddItemInInventory(1);
        AddItemInInventory(2);
        AddItemInInventory(2);
        AddItemInInventory(2);
        AddItemInInventory(2);
        AddItemInInventory(3);
        AddItemInInventory(3);
        AddItemInInventory(3);
        AddItemInInventory(3);
        AddItemInInventory(3);
        AddItemInInventory(3);
        AddItemInInventory(2);
        AddItemInInventory(2);
        AddItemInInventory(2);
        AddItemInInventory(2);
        AddItemInInventory(3);
        AddItemInInventory(3);
        AddItemInInventory(2);
        AddItemInInventory(3);
        AddItemInInventory(3);
        AddItemInInventory(3);

        for (int i = 0; i < 100; i++)
        {
            AddItemInInventory(3);
        }
        //RemoveAllItem(3);
        //RemoveOneItem(1);
    }

    // Item 삭제
    public void RemoveAllItem(int index)
    {
        slotScripts[index].InitSlot();
        //slotScripts.RemoveAt(index);
    }

    public void RemoveOneItem(int index)
    {
        if (slotScripts[index].itemCount == 0)
        {
            RemoveAllItem(index);
        }
        else
        {
            slotScripts[index].RemoveOneItemInSlot();
        }
    }

    public void ChangeSlotPivot(Vector2 pivotPos)
    {
        // 아이템 슬롯에 따라서 ItemDescBackGround의 피벗을 바꿔준다.

        itemDescBackGround.rectTransform.pivot = pivotPos;
    }

    private void ItemDescBackGroundPivotInsertIntoSlot()
    { 
    }
}
