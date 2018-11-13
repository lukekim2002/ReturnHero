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
                this.transform.GetChild(0).parent = Inventory.instance.draggedItem;

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
                ChangeItemData(0);
            }
            print("Weapon or Accessory 장착!");
        }
        //}

        if (Inventory.instance.enteredItemSlot.slotType >= 3)
        {
            ChangeItemData();
            print("Inventory Swap! (2)");
            return;
        }
    }

    public void DragEndAccessory()
    {
        if (_slot.item.itemID <= 0)
            return;

        ReturnOriginalSlot();

        if (Inventory.instance.enteredItemSlot != null)
        {
            ChangeItemData(-1);
        }

        isDraging = false;
    }

    public void DragEndWeapon()
    {
        if (_slot.item.itemID <= 0)
            return;

        ReturnOriginalSlot();

        if (Inventory.instance.enteredItemSlot != null)
        {
            ChangeItemData(-2);
        }

        isDraging = false;
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

        _slot.item.itemID = itemBoxID;

        Inventory.instance.ChangeItem(_slot);
        Inventory.instance.ChangeItem(Inventory.instance.enteredItemSlot);
    }
}
