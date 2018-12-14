﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProductionRecipeEvent : MonoBehaviour
{
    #region PRIVATE
    private string[] readProductionRecipeCSVRow = { "Item1", "Item2", "Item3", "Item4", "Item5", "Item6" };
    private Production production;
    private static bool isSelectOn = false;
    #endregion

    #region
    public int slotNum;
    #endregion

    private void Start()
    {
        production = UIGeneralManager.instance.productionCanvas.GetComponent<Production>();
    }

    public void OnClickProductionRecipe()
    {
        production.productionMaterialItemsID.Clear();

        for (int i = 0; i < UIGeneralManager.instance.productionMaterialsItemSlot.Length; i++)
        {
            UIGeneralManager.instance.productionMaterialsItemSlot[i].sprite
                = ItemSpriteManager.instance.BindingImageAndItemID((int)production.recipeSet[slotNum][readProductionRecipeCSVRow[i]]);
            production.productionMaterialItemsID.Add((int)production.recipeSet[slotNum][readProductionRecipeCSVRow[i]]);
        }

        UIGeneralManager.instance.productionSelect.sprite = UIGeneralManager.instance.productionSelectOn;

        if (UIGeneralManager.instance.productionCanvas.GetComponent<Production>().productionItemType == 0)
            UIGeneralManager.instance.afterProductionImage.sprite = ProductionRecipeSpriteManager.instance.productionWeaponSprite[slotNum];
        else
            UIGeneralManager.instance.afterProductionImage.sprite = ProductionRecipeSpriteManager.instance.productionPotionSprite[slotNum];

        production.afterProductionItemID = slotNum;

        isSelectOn = true;
    }
     
    public void OnClickProductionSelect()
    {
        if (isSelectOn)
        {
            for (int i = 0; i < 6; i++)
            {
                UIGeneralManager.instance.productionMaterialsItemSlot[i].sprite = ItemSpriteManager.instance.BindingImageAndItemID(0);
            }
            UIGeneralManager.instance.afterProductionImage.sprite = ItemSpriteManager.instance.BindingImageAndItemID(0);

            isSelectOn = false;
            UIGeneralManager.instance.productionSelect.sprite = UIGeneralManager.instance.productionSelectOff;

            for (int i = Inventory.instance.itemSlotScripts.Count - 1; i >= 0; i--)
            {
                if (Inventory.instance.itemSlotScripts[i].item.itemID == 0)
                {
                    continue;
                }
                else 
                {
                    for (int j = 0; j < production.productionMaterialItemsID.Count; j++)
                    {
                        if (Inventory.instance.itemSlotScripts[i].item.itemID == production.productionMaterialItemsID[j])
                        {
                            Inventory.instance.RemoveItemIDCount(Inventory.instance.itemSlotScripts[i].item.itemID, i);
                            production.productionMaterialItemsID.RemoveAt(j);
                            // 수량이 있는 아이템이라면
                            if (Inventory.instance.itemSlotScripts[i].item.itemCount > 1)
                            {
                                i++;
                            }
                            break;
                        }
                    }
                }
            }

            if (UIGeneralManager.instance.productionCanvas.GetComponent<Production>().productionItemType == 0)
                Inventory.instance.AddEquiment((int)production.recipeSet[production.afterProductionItemID]["ProductionItemID"]);
            else
                Inventory.instance.AddItem((int)production.recipeSet[production.afterProductionItemID]["ProductionItemID"]);

            production.productionMaterialItemsID.Clear();
            StartCoroutine(ProductionSuccessAnimationPlay());

        }

    }

    private IEnumerator ProductionSuccessAnimationPlay()
    {
        UIGeneralManager.instance.productionSuccessAnimation.gameObject.SetActive(true);

        while (UIGeneralManager.instance.productionSuccessAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.98f)
        {
            yield return null;
        }

        UIGeneralManager.instance.productionSuccessAnimation.gameObject.SetActive(false);

    }
}