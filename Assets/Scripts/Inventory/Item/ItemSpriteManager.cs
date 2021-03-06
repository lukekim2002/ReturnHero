﻿using UnityEngine;
using System.Collections;

public class ItemSpriteManager : MonoBehaviour
{
    public static ItemSpriteManager instance;
    public Sprite itemSlotSprite;
    public Sprite weapon_A_Sprite;
    public Sprite weapon_B_Sprite;
    public Sprite weapon_C_Sprite;
    public Sprite weapon_D_Sprite;
    public Sprite weapon_E_Sprite;
    public Sprite ring_Damage_A;
    public Sprite ring_Damage_B;
    public Sprite ring_HP_A;
    public Sprite ring_HP_B;
    public Sprite last;
    public Sprite healingPotion_A;
    public Sprite healingPotion_B;
    public Sprite healingPotion_C;
    public Sprite drug_Immunizing_Poisoning;
    public Sprite drug_Immunizing_Burned;
    public Sprite drug_Immunizing_Frosted;
    public Sprite cure_All;
    public Sprite cheap_Iron;
    public Sprite steel;
    public Sprite leather;
    public Sprite reinforced_Leather_Fire;
    public Sprite reinforced_Leather_Ice;
    public Sprite leather_Of_Dragon;
    public Sprite medicinal_Herbs_A;
    public Sprite medicinal_Herbs_B;
    public Sprite medicinal_Herbs_C;
    public Sprite blood_Pack_A;
    public Sprite blood_Pack_B;
    public Sprite toxin_A;
    public Sprite toxin_B;
    public Sprite ghost;
    public Sprite ghost_Nightmare;
    public Sprite fragment_Of_Bone;
    public Sprite fragment_Of_Ice;
    public Sprite piece_Of_Moonlight;
    public Sprite burning_Fire;
    public Sprite element_Fire;
    public Sprite element_Ice;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public Sprite BindingImageAndItemID(int itemID)
    {
        switch (itemID)
        {
            case 0: return itemSlotSprite;
            case 1: return weapon_A_Sprite;
            case 2: return weapon_B_Sprite;
            case 3: return weapon_C_Sprite;
            case 4: return weapon_D_Sprite;
            case 5: return weapon_E_Sprite;
            case 6: return ring_Damage_A;
            case 7: return ring_Damage_B;
            case 8: return ring_HP_A;
            case 9: return ring_HP_B;
            case 10:return last;
            case 11: return healingPotion_A;
            case 12: return healingPotion_B;
            case 13: return healingPotion_C;
            case 14: return drug_Immunizing_Poisoning;
            case 15: return drug_Immunizing_Burned;
            case 16: return drug_Immunizing_Frosted;
            case 17: return cure_All;
            case 18: return cheap_Iron;
            case 19: return steel;
            case 20: return leather;
            case 21: return reinforced_Leather_Fire;
            case 22: return reinforced_Leather_Ice;
            case 23: return leather_Of_Dragon;
            case 24: return medicinal_Herbs_A;
            case 25: return medicinal_Herbs_B;
            case 26: return medicinal_Herbs_C;
            case 27: return blood_Pack_A;
            case 28: return blood_Pack_B;
            case 29: return toxin_A;
            case 30: return toxin_B;
            case 31: return ghost;
            case 32: return ghost_Nightmare;
            case 33: return fragment_Of_Bone;
            case 34: return fragment_Of_Ice;
            case 35: return piece_Of_Moonlight;
            case 36: return burning_Fire;
            case 37: return element_Fire;
            case 38: return element_Ice;
            default: return null;
        }
    }
}
