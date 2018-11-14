using UnityEngine;
using System.Collections;

public class ItemSpriteManager : MonoBehaviour
{
    public static ItemSpriteManager instance;
    public Sprite itemSlotSprite;
    public Sprite accessorySlotSprite;
    public Sprite weaponSlotSprite;
    public Sprite bronzeSwordSprite;
    public Sprite silverSwordSprite;
    public Sprite bronzeRingSprite;
    public Sprite silverRingSprite;
    public Sprite bookSprite;
    public Sprite cigaSprite;
    

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

    public Sprite BronzeSwordSprite
    {
        get { return bronzeSwordSprite; }
    }

    public Sprite SilverSwordSprite
    {
        get { return silverSwordSprite; }
    }

    public Sprite BronzeRingSprite
    {
        get { return bronzeRingSprite; }
    }

    public Sprite SilverRingSprite
    {
        get { return silverRingSprite; }
    }

    public Sprite BookSprite
    {
        get { return bookSprite; }
    }

    public Sprite CIgaSprite
    {
        get { return cigaSprite; }
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
            case 1: return BronzeSwordSprite;
            case 2: return SilverSwordSprite;
            case 3: return bronzeRingSprite;
            case 4: return silverRingSprite;
            case 5: return BookSprite;
            case 6: return CIgaSprite;
            default: return null;
        }
    }
}
