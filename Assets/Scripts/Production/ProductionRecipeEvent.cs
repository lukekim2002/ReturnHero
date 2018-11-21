using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProductionRecipeEvent : MonoBehaviour
{
    public int slotNum;
    private string[] readProductionRecipeCSVRow = { "Item1", "Item2", "Item3", "Item4", "Item5", "Item6" };

    private Production production;

    private void Start()
    {
        production = UIGeneralManager.instance.productionCanvas.GetComponent<Production>();
    }

    public void OnClickProductionRecipe()
    {
        for (int i = 0; i < UIGeneralManager.instance.productionMaterialsItemSlot.Length; i++)
        {
            UIGeneralManager.instance.productionMaterialsItemSlot[i].sprite
                = ItemSpriteManager.instance.BindingImageAndItemID((int)production.recipeSet[slotNum][readProductionRecipeCSVRow[i]]);
        }

        UIGeneralManager.instance.productionSelect.sprite = UIGeneralManager.instance.productionSelectOn;

        if (UIGeneralManager.instance.productionCanvas.GetComponent<Production>().productionItemType == 0)
            UIGeneralManager.instance.afterProductionImage.sprite = ProductionRecipeSpriteManager.instance.productionWeaponSprite[slotNum];
        else
            UIGeneralManager.instance.afterProductionImage.sprite = ProductionRecipeSpriteManager.instance.productionPotionSprite[slotNum];

        production.afterProductionItemID = slotNum;
    }

    //TODO : 아이템 SELECT 버튼 눌렀을 때 아이템 개수 사라지게 하고 Recipe off
    public void OnClickProductionSelct()
    {
        if(UIGeneralManager.instance.productionCanvas.GetComponent<Production>().productionItemType == 0)
            Inventory.instance.AddEquiment((int)production.recipeSet[production.afterProductionItemID]["ProductionItemID"]);
        else
            Inventory.instance.AddItem((int)production.recipeSet[production.afterProductionItemID]["ProductionItemID"]);

        Inventory.instance.RemoveItemIDCount(production.afterProductionItemID);
    }
}
