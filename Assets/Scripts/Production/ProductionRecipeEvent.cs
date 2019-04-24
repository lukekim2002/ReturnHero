﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProductionRecipeEvent : MonoBehaviour
{
    #region PRIVATE
    private Production _production;
    private static bool _isSelectOn = false;
    #endregion

    #region PUBLIC
    public int slotNum;
    #endregion

    private void Start()
    {
        _production = UIGeneralManager.instance.productionCanvas.GetComponent<Production>();
    }

    public void OnClickProductionRecipe()
    {
        _production.currentProductionMaterialItemsID.Clear();
        _production.currentProductionMaterialItemCount.Clear();

        for (int i = 0; i < UIGeneralManager.instance.productionMaterialsItemSlot.Length; i++)
        {
            UIGeneralManager.instance.productionMaterialsItemSlot[i].GetComponentInChildren<Image>().sprite
                = ItemSpriteManager.instance.BindingImageAndItemID((int)_production.recipeSet[slotNum][_production.readProductionRecipeCSVRow[i]]);

            UIGeneralManager.instance.productionMaterialsItemSlot[i].GetComponent<Slot>().item.itemCount
                = (int)_production.recipeSet[slotNum][_production.readProductionRecipeCSVItemCountRow[i]];

            if (UIGeneralManager.instance.productionMaterialsItemSlot[i].GetComponent<Slot>().item.itemCount >= 1)
            {
                UIGeneralManager.instance.productionMaterialsItemSlot[i].GetComponent<Slot>().SetSlotItemCount();
            }


            UIGeneralManager.instance.productionMaterialsItemSlot[i].GetComponentInChildren<Image>().SetNativeSize();
            _production.currentProductionMaterialItemsID.Add((int)_production.recipeSet[slotNum][_production.readProductionRecipeCSVRow[i]]);

            _production.currentProductionMaterialItemCount.Add((int)_production.recipeSet[slotNum][_production.readProductionRecipeCSVItemCountRow[i]]);
        }

        UIGeneralManager.instance.productionSelect.sprite = UIGeneralManager.instance.productionSelectOn;

        if (UIGeneralManager.instance.productionCanvas.GetComponent<Production>().productionItemType == 0)
            UIGeneralManager.instance.afterProductionImage.sprite = ProductionRecipeSpriteManager.instance.productionWeaponSprite[slotNum];
        else
            UIGeneralManager.instance.afterProductionImage.sprite = ProductionRecipeSpriteManager.instance.productionPotionSprite[slotNum];

        _production.afterProductionItemID = slotNum;

        _isSelectOn = true;
    }

    public void OnClickProductionSelect()
    {
        if (_isSelectOn)
        {
            for (int i = 0; i < 6; i++)
            {
                UIGeneralManager.instance.productionMaterialsItemSlot[i].GetComponent<Slot>().InitItemSlot();
            }

            // NOTE : ItemSlot -> AccessorySlot으로 교체할 것
            UIGeneralManager.instance.afterProductionImage.sprite = ItemSpriteManager.instance.BindingImageAndItemID(0);

            _isSelectOn = false;
            UIGeneralManager.instance.productionSelect.sprite = UIGeneralManager.instance.productionSelectOff;

            for (int i = Inventory.instance.itemSlotScripts.Count - 1; i >= 0; i--)
            {
                if (Inventory.instance.itemSlotScripts[i].item.itemID == 0)
                {
                    continue;
                }
                else
                {
                    for (int j = 0; j < _production.currentProductionMaterialItemsID.Count; j++)
                    {
                        if (_production.currentProductionMaterialItemsID[j] == Inventory.instance.itemSlotScripts[i].item.itemID)
                        {
                            if (Inventory.instance.itemSlotScripts[i].item.itemCount
                                < _production.currentProductionMaterialItemCount[j])
                            {
                                _production.currentProductionMaterialItemCount[j] -= Inventory.instance.itemSlotScripts[i].item.itemCount;
                                Inventory.instance.RemoveAllItemCount(Inventory.instance.itemSlotScripts[i].item.itemID, i);

                                break;
                            }
                            else if (_production.currentProductionMaterialItemsID[j] == 0)
                            {
                                continue;
                            }
                            else
                            {
                                Inventory.instance.RemoveItemCount(Inventory.instance.itemSlotScripts[i].item.itemID
                                    , i, _production.currentProductionMaterialItemCount[j]);

                                _production.currentProductionMaterialItemsID.RemoveAt(j);

                                break;
                            }
                        }
                    }

                }
            }

            if (UIGeneralManager.instance.productionCanvas.GetComponent<Production>().productionItemType == 0)
                Inventory.instance.AddEquiment((int)_production.recipeSet[_production.afterProductionItemID]["ID"]);
            else
                Inventory.instance.AddItem((int)_production.recipeSet[_production.afterProductionItemID]["ID"]);

            _production.currentProductionMaterialItemsID.Clear();
            _production.currentProductionMaterialItemCount.Clear();
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
