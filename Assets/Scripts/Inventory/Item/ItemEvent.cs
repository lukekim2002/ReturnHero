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

    // TODO : 무기 슬롯엔 무기만, 악세사리 슬롯엔 악세사리만 들어가게 하면서 두 타입의 아이템이 서로 교체되면 안되고 가방에 있는 아이템은 그대로여야 한다.
    // 아이템 드래그를 중지했을 때
    public void DragEndItem()
    {
        isDraging = false;

        if (_slot.item.itemID <= 0)
            return;

        ReturnOriginalSlot();

        if (Inventory.instance.enteredItemSlot == null)
            return;

        if ((int)ItemDatabase.instance.ThrowDataIntoContainer(_slot.item.itemID)["ItemType"]
            == (int)ItemDatabase.instance.ThrowDataIntoContainer(Inventory.instance.enteredItemSlot.item.itemID)["ItemType"])
        {
            if (_slot.item.itemID == Inventory.instance.enteredItemSlot.item.itemID)
            {
                ChangeItemData();
                print("0");
            }

            else
            {
                ChangeItemData(0);
                print("1");
            }
        }

        else if (0 == (int)ItemDatabase.instance.ThrowDataIntoContainer(Inventory.instance.enteredItemSlot.item.itemID)["ItemType"])
        {
            ChangeItemData(); print("2"); 
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
