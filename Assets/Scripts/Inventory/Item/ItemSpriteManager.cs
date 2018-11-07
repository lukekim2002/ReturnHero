using UnityEngine;
using System.Collections;

public class ItemSpriteManager : MonoBehaviour
{
    public static ItemSpriteManager instance;
    public Sprite slotSprite;
    public Sprite swordSprite;
    public Sprite bronzeRingSprite;
    public Sprite potionSprite;

    public Sprite SlotSprite
    {
        get { return slotSprite; }
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
            case 0: return SlotSprite;
            case 1: return SwordSprite;
            case 3: return bronzeRingSprite;
            case 9: return PotionSprite;
            default: return null;
        }
    }
}
