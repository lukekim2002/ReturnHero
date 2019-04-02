using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Slot : MonoBehaviour
{
    #region PRIVATE
    private Image slotImage;
    private Image itemDescBackGround;
    private TextMeshProUGUI itemCountTextMeshPro;
    #endregion

    #region PUBLIC
    public int slotNum;
    public int slotType;
    public Item item = new Item();
    public Vector2 itemDescBackGroundPivot;
    #endregion

    private void Awake()
    {
        // ItemSprite Image
        slotImage = this.GetComponentInChildren<Image>();
        // ItemCount TextMeshPro
        itemCountTextMeshPro = this.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetWeaponSlotImage()
    {
        slotImage.sprite = InSlotSpriteManager.instance.BindingImageAndItemID(item.itemID);
        slotImage.SetNativeSize();
    }

    // 슬롯 아이템 이미지 세팅
    public void SetItemSlotImage()
    {
        slotImage.sprite = ItemSpriteManager.instance.BindingImageAndItemID(item.itemID);
        slotImage.SetNativeSize();
    }

    // 슬롯 아이템 수량 세팅
    public void SetSlotItemCount()
    {
        itemCountTextMeshPro.text = "x" + item.itemCount;
    }

    // 슬롯 아이템 수량 초기화
    public void InitSlotItemCount()
    {
        itemCountTextMeshPro.text = "";
    }

    // 슬롯 아이템 칸 비울 때 호출하면 됨
    public void InitSlot()
    {
        this.item = new Item();
        SetItemSlotImage();
        item.itemCount = 0;
        InitSlotItemCount();
    }

    // 해당 칸 아이템 모두 삭제
    public void RemoveAllItem()
    {
        InitSlot();
    }

    // 해당 칸 아이템 하나 삭제
    public void RemoveOneItem()
    {
        item.itemCount--;
        SetSlotItemCount();

        if (item.itemCount == 0)
        {
            RemoveAllItem();
        }
    }
}