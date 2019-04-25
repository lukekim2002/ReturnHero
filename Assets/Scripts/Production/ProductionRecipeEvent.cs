using UnityEngine;
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
        _production.currentMaterialItemID.Clear();
        _production.currentMaterialCount.Clear();


        for (int i = 0; i < UIGeneralManager.instance.productionMaterialsItemSlot.Length; i++)
        {
            var slot = UIGeneralManager.instance.productionMaterialsItemSlot[i].GetComponent<Slot>();
            var image = UIGeneralManager.instance.productionMaterialsItemSlot[i].GetComponentInChildren<Image>();

            if (slot.item.itemCount >= 1)
            {
                slot.SetSlotItemCount();
            }

            slot.item.itemCount = (int)_production.currentRecipeSet[slotNum][_production.productionRecipeItemCountRow[i]];

            image.sprite = ItemSpriteManager.instance.BindingImageAndItemID((int)_production.currentRecipeSet[slotNum][_production.productionRecipeItemIdRow[i]]);
            image.SetNativeSize();

            _production.currentMaterialItemID.Add((int)_production.currentRecipeSet[slotNum][_production.productionRecipeItemIdRow[i]]);
            _production.currentMaterialCount.Add((int)_production.currentRecipeSet[slotNum][_production.productionRecipeItemCountRow[i]]);
        }

        UIGeneralManager.instance.productionSelect.sprite = UIGeneralManager.instance.productionSelectOn;

        if (_production.productionItemType == 0)
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
                    for (int j = 0; j < _production.currentMaterialItemID.Count; j++)
                    {
                        if (_production.currentMaterialItemID[j] == Inventory.instance.itemSlotScripts[i].item.itemID)
                        {
                            if (Inventory.instance.itemSlotScripts[i].item.itemCount
                                < _production.currentMaterialCount[j])
                            {
                                _production.currentMaterialCount[j] -= Inventory.instance.itemSlotScripts[i].item.itemCount;
                                Inventory.instance.RemoveAllItemCount(Inventory.instance.itemSlotScripts[i].item.itemID, i);

                                break;
                            }
                            else if (_production.currentMaterialItemID[j] == 0)
                            {
                                continue;
                            }
                            else
                            {
                                Inventory.instance.RemoveItemCount(Inventory.instance.itemSlotScripts[i].item.itemID
                                    , i, _production.currentMaterialCount[j]);

                                _production.currentMaterialItemID.RemoveAt(j);

                                break;
                            }
                        }
                    }

                }
            }

            if (UIGeneralManager.instance.productionCanvas.GetComponent<Production>().productionItemType == 0)
                Inventory.instance.AddEquiment((int)_production.currentRecipeSet[_production.afterProductionItemID]["ID"]);
            else
                Inventory.instance.AddItem((int)_production.currentRecipeSet[_production.afterProductionItemID]["ID"]);

            _production.currentMaterialItemID.Clear();
            _production.currentMaterialCount.Clear();

            StartCoroutine(ProductionSuccessAnimationPlay());

        }

    }

    private IEnumerator ProductionSuccessAnimationPlay()
    {
        UIGeneralManager.instance.productionSuccessAnimation.gameObject.SetActive(true);

        while (UIGeneralManager.instance.productionSuccessAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }

        UIGeneralManager.instance.productionSuccessAnimation.gameObject.SetActive(false);

    }
}
