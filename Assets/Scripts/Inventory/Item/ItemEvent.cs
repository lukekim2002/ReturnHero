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
        if (_slot.item.itemID == 0)
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
        if (_slot.item.itemID == 0)
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
        if (_slot.item.itemID == 0)
            return;

        print("Drag end");
        Inventory.instance.draggedItem.GetChild(0).GetComponent<Image>().raycastTarget = true;
        Inventory.instance.draggedItem.GetChild(0).SetParent(this.transform);
        this.transform.GetChild(0).localPosition = Vector2.zero;

        if (Inventory.instance.enteredItemSlot == null)
        {
            print("null");
            // Item Slot을 제외한 다른 곳이라면 원래 자리로 옮긴다.
        }
        else
        {
            Item tempItem = _slot.item;
            _slot.item = Inventory.instance.enteredItemSlot.item;
            Inventory.instance.enteredItemSlot.item = tempItem;

            Inventory.instance.ChangeItem(_slot);
            Inventory.instance.ChangeItem(Inventory.instance.enteredItemSlot);
        }

        isDraging = false;
    }

    public void PointerEnterItem()
    {
        if (!isDraging)
        {
            if (_slot.item.itemID != 0)
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
}
