using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAccessorySlotSpriteManager : MonoBehaviour
{
    public static InAccessorySlotSpriteManager instance;
    public Sprite accessory_Slot;
    public Sprite ring_Damage_A;
    public Sprite ring_Damage_B;
    public Sprite ring_HP_A;
    public Sprite ring_HP_B;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public Sprite BindingImageAndItemID(int itemID)
    {
        switch (itemID)
        {
            case 0: return accessory_Slot;
            case 6: return ring_Damage_A;
            case 7: return ring_Damage_B;
            case 8: return ring_HP_A;
            case 9: return ring_HP_B;
            default: return null;
        }
    }
}
