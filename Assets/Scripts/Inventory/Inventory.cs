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
    public RectTransform weaponSlot;
    public RectTransform itemSlot;
    public RectTransform accessroySlot;
    public List<Slot> inventorySlotScripts = new List<Slot>();
    public List<Slot> accessorySlotScripts = new List<Slot>();
    public Slot weaponSlotScripts;
    public Image itemDescBackGround;

    public const int accessorySlot_X = 3;
    public const int accessorySlot_Y = 2;
    public const int itemSlot_X = 6;
    public const int itemSlot_Y = 4;
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
        for (int y = 0; y < itemSlot_Y; y++)
        {
            for (int x = 0; x < itemSlot_X; x++)
            {
                RectTransform itemSlotPrefabs = Instantiate(itemSlot);
                var slotComponent = itemSlotPrefabs.GetComponent<Slot>();

                // ItemSlot
                itemSlotPrefabs.SetParent(this.transform.GetChild(3));
                itemSlotPrefabs.localScale = new Vector2(1, 1);

                Vector2 newSlotPos = itemSlot.position;
                newSlotPos.x = newSlotPos.x + (x * 45f);
                newSlotPos.y = newSlotPos.y - (y * 50f);
                itemSlotPrefabs.anchoredPosition = newSlotPos;

                itemSlotPrefabs.name = "Slot " + ((y * itemSlot_X) + x);

                // 각 Slot 마다 보여줄 ITemDescBackGround Pivot을 Slot 안에 담아둠
                // x (0~2) : 0 / (3~5) : 1
                // y는 0 -> 0.33 -> 0.66 -> 1
                slotComponent.itemDescBackGroundPivot.x = (x > 2) ? 1 : 0;
                slotComponent.itemDescBackGroundPivot.y = 1f - (y * (1f / (itemSlot_Y - 1)));

                slotComponent.slotNum = y * itemSlot_X + x;
                inventorySlotScripts.Add(slotComponent);
            }
        }

        for (int y = 0; y < accessorySlot_Y; y++)
        {
            for (int x = 0; x < accessorySlot_X; x++)
            {
                RectTransform accessorySlotPrefabs = Instantiate(accessroySlot);
                var accessorySlotComponent = accessorySlotPrefabs.GetComponent<Slot>();

                accessorySlotPrefabs.transform.SetParent(this.transform.GetChild(2));
                accessorySlotPrefabs.transform.localScale = new Vector2(1, 1);

                Vector2 newSlotPos = accessroySlot.transform.position;
                newSlotPos.x = newSlotPos.x + (x * 58f);
                newSlotPos.y = newSlotPos.y - (y * 58f);
                accessorySlotPrefabs.anchoredPosition = newSlotPos;

                accessorySlotPrefabs.name = "Slot " + ((y * accessorySlot_X) + x);

                accessorySlotComponent.itemDescBackGroundPivot.x = (x > 2) ? 1 : 0;
                accessorySlotComponent.itemDescBackGroundPivot.y = 1f - (y * (1f / (accessorySlot_Y - 1)));

                accessorySlotComponent.slotNum = y * accessorySlot_X + x;
                accessorySlotScripts.Add(accessorySlotComponent);
            }
        }

        // weaponSlot의 Slot 컴포넌트
        weaponSlotScripts = weaponSlot.GetComponent<Slot>();

        ItemAddTestMethodCall();
    }

    // Item 추가
    public void AddItemInInventory(int mItemID)
    {
        for (int i = 0; i < inventorySlotScripts.Count; i++)
        {
            // 슬롯에 들어간 똑같은 아이템이 하나 이상 있다면
            if (inventorySlotScripts[i].item.itemID == mItemID)
            {
                // 슬롯에 들어간 똑같은 아이템이 5개 이하라면
                if (inventorySlotScripts[i].itemCount < 5)
                {
                    inventorySlotScripts[i].itemCount += 1;
                    inventorySlotScripts[i].SetSlotItemCount();

                    break;
                }
                // 슬롯에 들어간 똑같은 아이템이 5개이라면
                else if (inventorySlotScripts[i].itemCount == 5)
                {
                    continue;
                }
            }
            // 슬롯에 들어간 똑같은 아이템이 하나도 없다면
            else if (inventorySlotScripts[i].item.itemID == 0)
            {
                inventorySlotScripts[i].item = ItemDatabase.instance.items[mItemID - 1];
                inventorySlotScripts[i].itemCount = 1;
                // 인벤토리에 아이템 이미지를 뿌림
                inventorySlotScripts[i].SetSlotImage(mItemID);

                break;
            }
        }
    }

    public void ItemAddTestMethodCall()
    {
        for (int i = 0; i < 100; i++)
        {
            AddItemInInventory(3);
        }
        //RemoveAllItem(3);
        //RemoveOneItem(1);
        ChangeAccessoryInInventory(1, 0);
        ChangeWeaponInInventory(1);
    }

    // Item 삭제
    public void RemoveAllItem(int index)
    {
        inventorySlotScripts[index].InitSlot();
        //slotScripts.RemoveAt(index);
    }

    public void RemoveOneItem(int index)
    {
        if (inventorySlotScripts[index].itemCount == 0)
        {
            RemoveAllItem(index);
        }
        else
        {
            inventorySlotScripts[index].RemoveOneItemInSlot();
        }
    }

    public void ChangeSlotPivot(Vector2 pivotPos)
    {
        // 아이템 슬롯에 따라서 ItemDescBackGround의 피벗을 바꿔준다.

        itemDescBackGround.rectTransform.pivot = pivotPos;
    }

    public void ChangeWeaponInInventory(int mItemID)
    {
        if (weaponSlotScripts.item.itemID != mItemID)
        {
            weaponSlotScripts.item = ItemDatabase.instance.items[mItemID - 1];
        }
    }

    public void ChangeAccessoryInInventory(int mItemID, int index)
    {
        // 슬롯에 들어간 똑같은 아이템이 하나도 없다면
        if (accessorySlotScripts[index].item.itemID == 0)
        {
            accessorySlotScripts[index].item = ItemDatabase.instance.items[mItemID - 1];
            accessorySlotScripts[index].itemCount = 1;
            // 인벤토리에 아이템 이미지를 뿌림
            accessorySlotScripts[index].SetSlotImage(mItemID);
        }
    }
}
