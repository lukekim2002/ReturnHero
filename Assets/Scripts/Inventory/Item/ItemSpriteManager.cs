using UnityEngine;
using System.Collections;

public class ItemSpriteManager : MonoBehaviour
{
    public static ItemSpriteManager instance;
    public Sprite itemSlotSprite;
    public Sprite accessorySlotSprite;
    public Sprite weaponSlotSprite;
    public Sprite swordSprite;
    public Sprite bronzeRingSprite;
    public Sprite potionSprite;

    public Sprite ItemSlotSprite
    {
        get { return itemSlotSprite; }
    }

    public Sprite AccessorySlotSprite
    {
        get { return accessorySlotSprite; }
    }

    public Sprite WeaponSlotSprite
    {
        get { return weaponSlotSprite; }
    }

    public Sprite SwordSprite
    {
        get { return swordSprite; }
    }

    public Sprite BronzeRingSprite
    {
        get { return bronzeRingSprite; }
    }

    public Sprite PotionSprite
    {
        get { return potionSprite; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public Sprite BindingImageAndItemID(int itemID)
    {
        switch(itemID)
        {
            case -2: return WeaponSlotSprite;
            case -1: return AccessorySlotSprite;
            case 0: return ItemSlotSprite;
            case 1: return SwordSprite;
            case 3: return bronzeRingSprite;
            case 9: return PotionSprite;
            default: return null;
        }
    }
}
