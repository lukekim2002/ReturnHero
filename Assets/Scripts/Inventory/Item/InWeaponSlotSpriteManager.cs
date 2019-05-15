using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWeaponSlotSpriteManager : MonoBehaviour
{
    public static InWeaponSlotSpriteManager instance;
    public Sprite weapon_Slot;
    public Sprite weapon_A_Sprite;
    public Sprite weapon_B_Sprite;
    public Sprite weapon_C_Sprite;
    public Sprite weapon_D_Sprite;
    public Sprite weapon_E_Sprite;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public Sprite BindingImageAndItemID(int itemID)
    {
        switch (itemID)
        {
            case 0: return weapon_Slot;
            case 1: return weapon_A_Sprite;
            case 2: return weapon_B_Sprite;
            case 3: return weapon_C_Sprite;
            case 4: return weapon_D_Sprite;
            case 5: return weapon_E_Sprite;
            default: return null;
        }
    }
}
