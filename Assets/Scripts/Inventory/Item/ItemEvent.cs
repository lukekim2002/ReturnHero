using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemEvent : EventTrigger
{

    #region PRIVATE
    private Slot _slot;
    #endregion

    #region PUBLIC
    public static bool isDraging = false;
    // 아이템이 ProductionSlot에 들어갈 때마다 검사
    public static bool isItemInProduction = false;
    #endregion

    // Use this for initialization
    void Start()
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
                isDraging = true;

                Inventory.instance.itemDescBackGround.gameObject.SetActive(false);
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
                            Inventory.instance.enteredItemSlot = Inventory.instance.itemSlotScripts[i];
                            ChangeItemData(Inventory.instance.enteredItemSlot.item.itemID);
                            Inventory.instance.InsertItemIDCount(Inventory.instance.enteredItemSlot.item.itemID);
                            break;
                        }
                    }
                }

                if (_slot.slotType == 3)
                {
                    if ((int)ItemDatabase.instance.ThrowDataIntoContainer(_slot.item.itemID)["ItemType"] == 1)
                    {
                        Inventory.instance.enteredItemSlot = Inventory.instance.weaponSlot.GetComponent<Slot>();
                        ChangeItemData(Inventory.instance.enteredItemSlot.item.itemID);

                        Inventory.instance.RemoveEquimentIDCount(Inventory.instance.enteredItemSlot.item.itemID, _slot.slotNum);

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
                    ChangeItemData(Inventory.instance.enteredItemSlot.item.itemID);
                    Inventory.instance.RemoveEquimentIDCount(Inventory.instance.enteredItemSlot.item.itemID, Inventory.instance.enteredItemSlot.slotNum);
                    return;

                }
            }

            // 바꿀 슬롯이 Item 슬롯이라면
            else if (Inventory.instance.enteredItemSlot.slotType == 3)
            {
                if (_slot.slotType == 1)
                {
                    ChangeItemData(Inventory.instance.enteredItemSlot.item.itemID);
                    Inventory.instance.InsertItemIDCount(Inventory.instance.enteredItemSlot.item.itemID);
                }
                else if (_slot.slotType == 2)
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
            }

            Inventory.instance.enteredItemSlot = null;
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
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

    public override void OnPointerExit(PointerEventData eventData)
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

    private void ChangeItemData(int itemBoxID)
    {
        Item tempItem = _slot.item;
        _slot.item = Inventory.instance.enteredItemSlot.item;
        Inventory.instance.enteredItemSlot.item = tempItem;

        if (itemBoxID == 0)
        {
            _slot.item.itemID = itemBoxID;
        }

        Inventory.instance.ChangeItem(_slot);
        Inventory.instance.ChangeItem(Inventory.instance.enteredItemSlot);
    }

    public void InsetInProductionSlot()
    {
        if (_slot.slotType >= 3)
            isItemInProduction = true;
    }
}
