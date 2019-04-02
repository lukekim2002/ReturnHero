using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSlotSpriteManager : MonoBehaviour
{
    public static InSlotSpriteManager instance;
    public Sprite itemSlotSprite;
    public Sprite weapon_A_Sprite;
    public Sprite weapon_B_Sprite;
    public Sprite weapon_C_Sprite;
    public Sprite weapon_D_Sprite;
    public Sprite weapon_E_Sprite;

    public Sprite ItemSlotSprite
    {
        get { return itemSlotSprite; }
    }
    
    public Sprite Weapon_A_Sprite
    {
        get { return weapon_A_Sprite; }
    }

    public Sprite Weapon_B_Sprite
    {
        get { return weapon_B_Sprite; }
    }

    public Sprite Weapon_C_Sprite
    {
        get { return weapon_C_Sprite; }
    }

    public Sprite Weapon_D_Sprite
    {
        get { return weapon_D_Sprite; }
    }

    public Sprite Weapon_E_Sprite
    {
        get { return weapon_E_Sprite; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public Sprite BindingImageAndItemID(int itemID)
    {
        switch (itemID)
        {
            case 0: return ItemSlotSprite;
            case 1: return Weapon_A_Sprite;
            case 2: return Weapon_B_Sprite;
            case 3: return Weapon_C_Sprite;
            case 4: return Weapon_D_Sprite;
            case 5: return Weapon_E_Sprite;
            default: return null;
        }
    }
}
