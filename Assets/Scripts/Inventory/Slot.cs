using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotNum;
    public Item item;

    private Image slotImage;

    private void Awake()
    {
        slotImage = this.GetComponent<Image>();
    }

    public void SetSlotImage(int itemID)
    {
        slotImage.sprite = ItemSpriteManager.instance.BindingImageAndItemID(itemID);
    }
}
