using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemEvent : EventTrigger
{
    #region PRIVATE
    private Slot _slot;
    // 아이템이 드래그 중인지 아닌지 체크함.
    private static bool _isItemDraging = false;
    #endregion

    #region PUBLIC

    #endregion

    private void Awake()
    {
        _slot = this.GetComponent<Slot>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (_slot.item.itemID <= 0)
            {
                return;
            }
            else
            {
                _isItemDraging = true;

                Inventory.instance.itemDescWindow.gameObject.SetActive(false);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (_slot.item.itemID <= 0)
            {
                return;
            }
            else
            {
                if (_slot.slotType == 1)
                {
                    for (int i = 0; i < Inventory.instance.itemSlotScripts.Count; i++)
                    {
                        if (Inventory.instance.itemSlotScripts[i].item.itemID == 0)
                        {
                            Inventory.instance.changeItem = Inventory.instance.itemSlotScripts[i];
                            ChangeItemData();
                            Inventory.instance.InsertItemIDCount(Inventory.instance.changeItem.item.itemID);
                            break;
                        }
                    }
                }

                if (_slot.slotType == 3)
                {
                    if ((int)ItemDatabase.instance.ThrowDataIntoContainer(_slot.item.itemID)["ItemType"] == 1)
                    {
                        Inventory.instance.changeItem = Inventory.instance.weaponSlot.GetComponent<Slot>();
                        ChangeItemData();

                        Inventory.instance.RemoveItemIDCount(Inventory.instance.changeItem.item.itemID, _slot.slotNum, Inventory.instance.changeItem.slotType);

                        if (_slot.item.itemID > 0)
                        {
                            Inventory.instance.InsertItemIDCount(_slot.item.itemID);
                        }

                        return;

                    }

                    //else if ((int)ItemDatabase.instance.ThrowDataIntoContainer(_slot.item.itemID)["ItemType"] == 2)
                    //{
                    //    for (int i = 0; i < 6; i++)
                    //    {
                    //        Inventory.instance.enteredItemSlot = Inventory.instance.accessorySlotScripts[i].GetComponent<Slot>();

                    //        // 그 슬롯이 비어있는가?
                    //        if (Inventory.instance.enteredItemSlot.item.itemID <= 0)
                    //        {
                    //            ChangeItemData(0);
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }
        }
    }


    public override void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (_slot.item.itemID <= 0)
            {
                return;
            }
            else
            {
                if (transform.childCount > 0)
                    this.transform.GetChild(0).SetParent(Inventory.instance.draggedItem);

                var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, UIGeneralManager.instance.displayUICanvas.planeDistance);

                Inventory.instance.draggedItem.GetChild(0).position = UIGeneralManager.instance.uiCamera.ScreenToWorldPoint(screenPoint);
                Inventory.instance.draggedItem.GetChild(0).GetComponent<Image>().raycastTarget = false;
            }
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _isItemDraging = false;

            if (_slot.item.itemID <= 0)
                return;

            ReturnToOriginalSlot();

            if (Inventory.instance.changeItem == null)
            {
                Inventory.instance.RemoveAllItemCount(_slot.item.itemID, _slot.slotNum);
                return;
            }

            /*
             * WeaponSlot SlotType = 1
             * AccessotySlot SlotType = 2
             * ItemSlot SlotType = 3
             */

            // 바꿀 슬롯이 WeaponSlot이나 Accessory 슬롯이라면
            if (Inventory.instance.changeItem.slotType < 3)
            {
                // Weapon 혹은 Accessory가 아닌 다른 아이템이 Weapon Slot이나 Accessory 슬롯에 들어가는 것을 방지
                // 드래그한 아이템의 ItemType이 Weapon이나 Accessroy의 slotType이랑 똑같은가?
                if ((int)ItemDatabase.instance.ThrowDataIntoContainer(_slot.item.itemID)["ItemType"]
                 == Inventory.instance.changeItem.slotType)
                {
                    ChangeItemData();
                    Inventory.instance.RemoveItemIDCount(Inventory.instance.changeItem.item.itemID, Inventory.instance.changeItem.slotNum, Inventory.instance.changeItem.slotType);
                    return;

                }
            }

            // 바꿀 슬롯이 Item 슬롯이라면
            else if (Inventory.instance.changeItem.slotType == 3)
            {
                if (_slot.slotType == 1)
                {
                    ChangeItemData();
                    Inventory.instance.InsertItemIDCount(Inventory.instance.changeItem.item.itemID);
                }
                else if (_slot.slotType == 2)
                {
                    // 슬롯이 비어 있다면
                    if (Inventory.instance.changeItem.item.itemID == 0)
                    {
                        ChangeItemData();
                    }
                    else
                    {
                        ChangeItemData();
                    }
                }
                else if (_slot.slotType == 3)
                {
                    // 슬롯이 비어 있다면
                    if (Inventory.instance.changeItem.item.itemID == 0)
                    {
                        ChangeItemData();
                    }
                    else
                    {
                        ChangeItemData();
                    }
                }
            }

            Inventory.instance.changeItem = null;
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isItemDraging)
        {
            if (_slot.item.itemID > 0)
            {
                Inventory.instance.itemDescWindow.gameObject.SetActive(true);
                Inventory.instance.itemDescWindow.transform.position = this.transform.position;
                Inventory.instance.itemDescWindow.GetComponentInChildren<TextMeshProUGUI>().text = (string)ItemDatabase.instance.ThrowDataIntoContainer(_slot.item.itemID)["Desc"];
                Inventory.instance.ChangeSlotPivot(_slot.itemDescBackGroundPivot);
            }
        }
        else
        {
            Inventory.instance.changeItem = _slot;
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        Inventory.instance.itemDescWindow.gameObject.SetActive(false);
        Inventory.instance.changeItem = null;
    }

    private void ReturnToOriginalSlot()
    {
        Inventory.instance.draggedItem.GetChild(0).GetComponent<Image>().raycastTarget = true;
        Inventory.instance.draggedItem.GetChild(0).SetParent(this.transform);
        this.transform.GetChild(0).localPosition = Vector2.zero;
    }

    private void ChangeItemData()
    {
        Item tempItem = _slot.item;
        _slot.item = Inventory.instance.changeItem.item;
        Inventory.instance.changeItem.item = tempItem;

        Inventory.instance.ChangeItem(_slot);
        Inventory.instance.ChangeItem(Inventory.instance.changeItem);
    }
}
