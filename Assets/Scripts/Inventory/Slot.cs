using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Slot : MonoBehaviour
{
    public int slotNum;
    public Item item;
    public int itemCount = 0;
    public Vector2 itemDescBackGroundPivot;

    private Image slotImage;
    private Image itemDescBackGround;
    private TextMeshProUGUI itemCountTextMeshPro;
  

    private void Awake()
    {
        slotImage = this.GetComponent<Image>();
        itemCountTextMeshPro = this.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetSlotImage(int itemID)
    {
        slotImage.sprite = ItemSpriteManager.instance.BindingImageAndItemID(itemID);
    }

    public void SetSlotItemCount()
    {
        itemCountTextMeshPro.text = "x" + itemCount;
    }

    public void InitSlotItemCount()
    {
        itemCountTextMeshPro.text = "";
    }

    public void InitSlot()
    {
        SetSlotImage(0);
        this.item = new Item();
        this.itemCount = 0;
        InitSlotItemCount();
    }

    public void RemoveOneItemInSlot()
    {
        this.itemCount--;
        SetSlotItemCount();
    }

    public void SlotOnMouseEnter()
    {
        if (this.item.itemID != 0)
        {
            Inventory.instance.itemDescBackGround.gameObject.SetActive(true);
            Inventory.instance.itemDescBackGround.transform.position = this.transform.position;
            Inventory.instance.itemDescBackGround.GetComponentInChildren<TextMeshProUGUI>().text = item.itemDesc;
            print(this.itemDescBackGroundPivot);
            Inventory.instance.ChangeSlotPivot(this.itemDescBackGroundPivot);
        }
    }

    public void SlotOnMouseExit()
    {
        Inventory.instance.itemDescBackGround.gameObject.SetActive(false);
    }
}