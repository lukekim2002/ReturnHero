using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public int slotNum;
    public Item item;
    public int itemCount = 0;

    private Image slotImage;
    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        slotImage = this.GetComponent<Image>();
        textMeshProUGUI = this.transform.GetComponentInChildren <TextMeshProUGUI>();
    }

    public void SetSlotImage(int itemID)
    {
        slotImage.sprite = ItemSpriteManager.instance.BindingImageAndItemID(itemID);
    }

    public void SetSlotItemCount()
    {
        textMeshProUGUI.text = "x" + itemCount;
    }
}
