using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProductionRecipeSpriteManager : MonoBehaviour
{
    public static ProductionRecipeSpriteManager instance;
    public Sprite[] productionWeaponSprite;
    public Sprite[] productionPotionSprite;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
