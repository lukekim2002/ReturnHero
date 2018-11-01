using UnityEngine;
using System.Collections;

public class ItemSpriteManager : MonoBehaviour
{
    public static ItemSpriteManager instance;
    public Sprite swordSprite;
    public Sprite bronzeRingSprite;
    public Sprite potionSprite;

    public Sprite SwordImage
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
            case 1: return SwordImage;
            case 2: return bronzeRingSprite;
            case 3: return PotionSprite;
            default: return null;
        }
    }
}
