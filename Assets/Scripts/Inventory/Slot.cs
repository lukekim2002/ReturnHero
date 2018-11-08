using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Slot : MonoBehaviour
{
    public int slotNum;
    public Item item = new Item();
    //public int itemCount = 0;
    public Vector2 itemDescBackGroundPivot;

    private Image slotImage;
    private Image itemDescBackGround;
    private TextMeshProUGUI itemCountTextMeshPro;
  

    private void Awake()
    {
        slotImage = this.GetComponentInChildren<Image>();
        itemCountTextMeshPro = this.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetSlotImage()
    {
        slotImage.sprite = ItemSpriteManager.instance.BindingImageAndItemID(item.itemID);
    }

    public void SetSlotItemCount()
    {
        itemCountTextMeshPro.text = "x" + item.itemCount;
    }

    public void InitSlotItemCount()
    {
        itemCountTextMeshPro.text = "";
    }

    public void InitSlot()
    {
        this.item = new Item();
        SetSlotImage();
        item.itemCount = 0;
        InitSlotItemCount();
    }

    public void RemoveOneItemInSlot()
    {
        item.itemCount--;
        SetSlotItemCount();
    }
}