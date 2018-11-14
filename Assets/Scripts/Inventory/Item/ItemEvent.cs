using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEvent : MonoBehaviour
{

    #region PRIVATE
    private Slot _slot;
    #endregion

    #region PUBLIC
    public static bool isDraging = false;
    #endregion

    // Use this for initialization
    void Start()
    {
        _slot = this.GetComponent<Slot>();
    }

    // 아이템을 눌렀을 때
    public void PointerDownItem()
    {
        if (_slot.item.itemID <= 0)
        {
            return;
        }
        else
        {
            isDraging = true;

            Inventory.instance.itemDescBackGround.gameObject.SetActive(false);
        }
    }

    // 아이템을 드래그할 때
    public void DragItem()
    {
        if (_slot.item.itemID <= 0)
        {
            return;
        }
        else
        {
            if (transform.childCount > 0)
                this.transform.GetChild(0).SetParent(Inventory.instance.draggedItem);

            Inventory.instance.draggedItem.GetChild(0).position = Input.mousePosition;
            Inventory.instance.draggedItem.GetChild(0).GetComponent<Image>().raycastTarget = false;
        }
    }

    // 아이템 드래그를 중지했을 때
    public void DragEndItem()
    {
        isDraging = false;

        if (_slot.item.itemID <= 0)
            return;

        ReturnOriginalSlot();

        if (Inventory.instance.enteredItemSlot == null)
            return;

        /*
         * WeaponSlot SlotType = 1
         * AccessotySlot SlotType = 2
         * ItemSlot SlotType = 3
         */

        // 바꿀 슬롯이 WeaponSlot이나 Accessory 슬롯이라면
        if (Inventory.instance.enteredItemSlot.slotType < 3)
        {
            // Weapon 혹은 Accessory가 아닌 다른 아이템이 Weapon Slot이나 Accessory 슬롯에 들어가는 것을 방지
            // 드래그한 아이템의 ItemType이 Weapon이나 Accessroy의 slotType이랑 똑같은가?
            if ((int)ItemDatabase.instance.ThrowDataIntoContainer(_slot.item.itemID)["ItemType"]
             == Inventory.instance.enteredItemSlot.slotType)
            {
                // 그 슬롯이 비어있는가?
                if (Inventory.instance.enteredItemSlot.item.itemID < 0)
                {
                    ChangeItemData(0);
                    return;
                }
                else
                {
                    ChangeItemData(Inventory.instance.enteredItemSlot.item.itemID);
                    return;
                }
            }
        }

        // 바꿀 슬롯이 Item 슬롯이라면
        if (Inventory.instance.enteredItemSlot.slotType >= 3)
        {
            if (_slot.slotType == 1)
            {
                // 슬롯이 비어 있다면
                if (Inventory.instance.enteredItemSlot.item.itemID == 0)
                {
                    ChangeItemData(-2);
                    return;
                }
                else
                {
                    ChangeItemData(Inventory.instance.enteredItemSlot.item.itemID);
                    return;
                }
            }
            else if (_slot.slotType == 2)
            {
                // 슬롯이 비어 있다면
                if (Inventory.instance.enteredItemSlot.item.itemID == 0)
                {
                    ChangeItemData(-1);
                    return;
                }
                else
                {
                    ChangeItemData(Inventory.instance.enteredItemSlot.item.itemID);
                    return;
                }
            }
            else if (_slot.slotType == 3)
            {
                // 슬롯이 비어 있다면
                if (Inventory.instance.enteredItemSlot.item.itemID == 0)
                {
                    ChangeItemData(0);
                }
                else
                {
                    ChangeItemData(Inventory.instance.enteredItemSlot.item.itemID);
                }
            }
            print("Inventory Swap!");
            return;
        }
    }

    public void PointerEnterItem()
    {
        if (!isDraging)
        {
            if (_slot.item.itemID > 0)
            {
                Inventory.instance.itemDescBackGround.gameObject.SetActive(true);
                Inventory.instance.itemDescBackGround.transform.position = this.transform.position;
                Inventory.instance.itemDescBackGround.GetComponentInChildren<TextMeshProUGUI>().text = (string)ItemDatabase.instance.ThrowDataIntoContainer(_slot.item.itemID)["Desc"];
                Inventory.instance.ChangeSlotPivot(_slot.itemDescBackGroundPivot);
            }
        }
        else
        {
            Inventory.instance.enteredItemSlot = _slot;
        }
    }

    public void PointerExitItem()
    {
        Inventory.instance.itemDescBackGround.gameObject.SetActive(false);
        Inventory.instance.enteredItemSlot = null;
    }

    private void ReturnOriginalSlot()
    {
        // Item Slot을 제외한 다른 곳이라면 원래 자리로 옮긴다.
        Inventory.instance.draggedItem.GetChild(0).GetComponent<Image>().raycastTarget = true;
        Inventory.instance.draggedItem.GetChild(0).SetParent(this.transform);
        this.transform.GetChild(0).localPosition = Vector2.zero;
    }

    private void ChangeItemData()
    {
        Item tempItem = _slot.item;
        _slot.item = Inventory.instance.enteredItemSlot.item;
        Inventory.instance.enteredItemSlot.item = tempItem;

        Inventory.instance.ChangeItem(_slot);
        Inventory.instance.ChangeItem(Inventory.instance.enteredItemSlot);
    }

    private void ChangeItemData(int itemBoxID)
    {
        Item tempItem = _slot.item;
        _slot.item = Inventory.instance.enteredItemSlot.item;
        Inventory.instance.enteredItemSlot.item = tempItem;

        // 바꾸기 전에 원래 슬롯에 있던 아이템의 ID를 넘김
        _slot.item.itemID = itemBoxID;

        Inventory.instance.ChangeItem(_slot);
        print(_slot);
        Inventory.instance.ChangeItem(Inventory.instance.enteredItemSlot);
        print(Inventory.instance.enteredItemSlot);
    }
}
