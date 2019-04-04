using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region PUBLIC
    public static Inventory instance;
    public RectTransform weaponSlot;
    public RectTransform itemSlot;
    public RectTransform accessroySlot;
    public RectTransform draggedItem;
    public Dictionary<int, int> inventoryItemIDCount = new Dictionary<int, int>();
    public List<Slot> itemSlotScripts = new List<Slot>();
    public List<Slot> accessorySlotScripts = new List<Slot>();
    public Slot weaponSlotScripts;
    // 자리를 바꿀 아이템 슬롯 위치
    public Slot enteredItemSlot;
    // 아이템 설명창
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
        // Item Slot 배치
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
                itemSlotScripts.Add(slotComponent);
            }
        }

        // Accessrory Slot 배치
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

                accessorySlotPrefabs.name = "AccessorySlot " + ((y * accessorySlot_X) + x);
                accessorySlotComponent.itemDescBackGroundPivot.x = (x * (1f / (accessorySlot_X - 1)));
                accessorySlotComponent.itemDescBackGroundPivot.y = 1;

                accessorySlotComponent.slotNum = y * accessorySlot_X + x;
                accessorySlotScripts.Add(accessorySlotComponent);
            }
        }

        // weaponSlot의 Slot 컴포넌트
        weaponSlotScripts = weaponSlot.GetComponent<Slot>();

        ItemAddTestMethodCall();
    }

    public void ItemAddTestMethodCall()
    {
        for (int i = 1; i <= 5; i++)
        {
            AddEquiment(i);
        }
        for (int i = 0; i < 5; i++)
        {
            AddEquiment(i);
        }

        for (int i = 0; i < 10; i++)
        {
            AddItem(11);
            AddItem(12);
            AddItem(13);
        }
    }

    // Item 추가
    public void AddItem(int mItemID)
    {
        for (int i = 0; i < itemSlotScripts.Count; i++)
        {
            // 슬롯에 들어간 똑같은 아이템이 하나 이상 있다면
            if (itemSlotScripts[i].item.itemID == mItemID)
            {
                // 슬롯에 들어간 똑같은 아이템이 5개 이하라면
                if (itemSlotScripts[i].item.itemCount < 5)
                {
                    itemSlotScripts[i].item.itemCount += 1;
                    itemSlotScripts[i].SetSlotItemCount();

                    break;
                }
                // 슬롯에 들어간 똑같은 아이템이 5개이라면
                else if (itemSlotScripts[i].item.itemCount == 5)
                {
                    continue;
                }
            }
            // 슬롯에 들어간 똑같은 아이템이 하나도 없다면
            else if (itemSlotScripts[i].item.itemID == 0)
            {
                itemSlotScripts[i].item.itemID = mItemID;
                itemSlotScripts[i].item.itemCount = 1;
                // 인벤토리에 아이템 이미지를 뿌림
                itemSlotScripts[i].SetItemSlotImage();

                break;
            }
        }

        InsertItemIDCount(mItemID);

        UIGeneralManager.instance.productionCanvas.GetComponent<Production>().CheckItemMaterials();
    }

    public void AddEquiment(int mItemID)
    {
        for (int i = 0; i < itemSlotScripts.Count; i++)
        {
            // 슬롯에 들어간 똑같은 아이템이 하나도 없다면
            if (itemSlotScripts[i].item.itemID == 0)
            {
                itemSlotScripts[i].item.itemID = mItemID;
                itemSlotScripts[i].item.itemCount = 1;
                // 인벤토리에 아이템 이미지를 뿌림
                itemSlotScripts[i].SetItemSlotImage();

                InsertItemIDCount(mItemID);

                UIGeneralManager.instance.productionCanvas.GetComponent<Production>().CheckItemMaterials();

                break;
            }
        }
    }

    // 아이템 슬롯에 따라서 ItemDescBackGround의 피벗을 바꿔준다.
    public void ChangeSlotPivot(Vector2 pivotPos)
    {
        itemDescBackGround.rectTransform.pivot = pivotPos;
    }

    // 무기 슬롯의 무기를 교체한다.
    public void ChangeWeapon(int mItemID)
    {
        if (weaponSlotScripts.item.itemID != mItemID)
        {
            weaponSlotScripts.item.itemID = mItemID;
            weaponSlotScripts.SetItemSlotImage();
        }
    }

    // 악세사리 슬롯의 악세사리를 교체한다.
    public void ChangeAccessory(int mItemID, int index)
    {
        // 슬롯에 들어간 똑같은 아이템이 하나도 없다면
        if (accessorySlotScripts[index].item.itemID == 0)
        {
            accessorySlotScripts[index].item.itemID = mItemID;
            accessorySlotScripts[index].item.itemCount = 1;
            // 인벤토리에 아이템 이미지를 뿌림
            accessorySlotScripts[index].SetItemSlotImage();
        }
    }

    // 아이템 슬롯 위치를 서로 바꿔줌
    public void ChangeItem(Slot slot)
    {
        if (slot.item.itemID == 0)
            slot.InitItemSlot();

        if (slot.slotType == 1)
            slot.SetWeaponSlotImage();

        else if (slot.slotType == 2)
            slot.SetItemSlotImage();

        else if (slot.slotType == 3)
            slot.SetItemSlotImage();

        if (slot.item.itemCount > 1)
            slot.SetSlotItemCount();
        else
            slot.InitSlotItemCount();
    }

    public void InsertItemIDCount(int mItem)
    {
        if (!inventoryItemIDCount.ContainsKey(mItem))
        {
            inventoryItemIDCount.Add(mItem, 1);
        }
        else
        {
            inventoryItemIDCount[mItem]++;
        }

        UIGeneralManager.instance.productionCanvas.GetComponent<Production>().CheckItemMaterials();
    }

    public void RemoveItemIDCount(int mItemID, int index)
    {
        if (itemSlotScripts[index].item.itemCount > 0)
        {
            itemSlotScripts[index].RemoveOneItem();
        }

        if (inventoryItemIDCount[mItemID] > 0)
        {
            inventoryItemIDCount[mItemID]--;
        }
        else if (inventoryItemIDCount[mItemID] == 0)
        {
            inventoryItemIDCount.Remove(mItemID);
        }
        UIGeneralManager.instance.productionCanvas.GetComponent<Production>().CheckItemMaterials();
    }

    public void RemoveEquimentIDCount(int mItemID, int mIndex)
    {
        if (inventoryItemIDCount[mItemID] > 0)
        {
            inventoryItemIDCount[mItemID]--;
        }
        else if (inventoryItemIDCount[mItemID] == 0)
        {
            inventoryItemIDCount.Remove(mItemID);
        }
        UIGeneralManager.instance.productionCanvas.GetComponent<Production>().CheckItemMaterials();
    }
}
