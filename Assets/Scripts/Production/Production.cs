using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Production : MonoBehaviour
{
    #region PRIVATE
    private int countProductionType = 0;
    private string[] readProductionRecipeCSVRow = { "Item1", "Item2", "Item3", "Item4", "Item5", "Item6" };
    #endregion

    #region PUBLIC
    public List<Image> productionWeaponRecpieList;
    public List<Image> productionPotionRecpieList;
    public List<int> recipeIndex = new List<int>();
    public List<Dictionary<string, object>> recipeSet;
    public Dictionary<int, int> productionRecipeDictionary = new Dictionary<int, int>();
    public List<int> productionRecipeKey = new List<int>();
    // 1 = Weapon, 2 = Potion
    public int productionItemType = 1;
    public RectTransform productionRecipeSlot;

    #endregion

    private void Awake()
    {
        recipeSet = CSVReader.Read("CSV/Item/ProductionRecipe");
    }

    private void Start()
    {
        for (int i = 0; i < ProductionRecipeSpriteManager.instance.productionWeaponSprite.Length; i++)
        {
            RectTransform productionRecipeSlotPrefabs = Instantiate(productionRecipeSlot);

            productionRecipeSlotPrefabs.SetParent(UIGeneralManager.instance.productionWeaponViewportContent);
            productionRecipeSlotPrefabs.localScale = new Vector2(1, 1);

            Vector2 newSlotPos = productionRecipeSlot.position;
            newSlotPos.y = newSlotPos.y - (i * 37);
            productionRecipeSlotPrefabs.anchoredPosition = newSlotPos;

            productionRecipeSlotPrefabs.name = "ProductionRecipe" + i;

            productionWeaponRecpieList.Add(productionRecipeSlotPrefabs.GetComponent<Image>());
            var productionImage = productionRecipeSlotPrefabs.transform.GetChild(0);
            productionImage.GetComponent<Image>().sprite = ProductionRecipeSpriteManager.instance.productionWeaponSprite[i];
            productionImage.GetComponent<Image>().SetNativeSize();
            productionImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }

        for (int i = 0; i < ProductionRecipeSpriteManager.instance.productionPotionSprite.Length; i++)
        {
            RectTransform productionRecipeSlotPrefabs = Instantiate(productionRecipeSlot);

            productionRecipeSlotPrefabs.SetParent(UIGeneralManager.instance.productionPotionViewportContent);
            productionRecipeSlotPrefabs.localScale = new Vector2(1, 1);

            Vector2 newSlotPos = productionRecipeSlot.position;
            newSlotPos.y = newSlotPos.y - (i * 37);
            productionRecipeSlotPrefabs.anchoredPosition = newSlotPos;

            productionRecipeSlotPrefabs.name = "ProductionRecipe" + i;

            productionPotionRecpieList.Add(productionRecipeSlotPrefabs.GetComponent<Image>());
            var productionImage = productionRecipeSlotPrefabs.transform.GetChild(0);
            productionImage.GetComponent<Image>().sprite = ProductionRecipeSpriteManager.instance.productionPotionSprite[i];
            productionImage.GetComponent<Image>().SetNativeSize();
            productionImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }

        if (productionItemType == 1)
        {
            UIGeneralManager.instance.productionWeaponViewportContent.gameObject.SetActive(true);
            UIGeneralManager.instance.productionPotionViewportContent.gameObject.SetActive(false);
        }
        else
        {
            UIGeneralManager.instance.productionPotionViewportContent.gameObject.SetActive(true);
            UIGeneralManager.instance.productionWeaponViewportContent.gameObject.SetActive(false);
        }
    }

    //TODO : ItemID가 5인 아이템이 6개 들어갔을 때 PotionRecipe 이미지를 on으로 바꿀 것
    // 특정 아이템의 재료가 다 모였는지 검사한다.
    public void CheckMaterialItems()
    {
        // Production Type의 갯수를 센다.
        CountProductionType();

        for (int i = 0; i < countProductionType; i++)
        {
            productionRecipeDictionary.Clear();
            productionRecipeKey.Clear();

            for (int j = 0; j < readProductionRecipeCSVRow.Length; j++)
            {

                if ((int)recipeSet[recipeIndex[i]][readProductionRecipeCSVRow[j]] == 0)
                {
                    continue;
                }
                else
                {
                    if (!productionRecipeDictionary.ContainsKey((int)recipeSet[recipeIndex[i]][readProductionRecipeCSVRow[j]]))
                    {
                        productionRecipeDictionary.Add((int)recipeSet[recipeIndex[i]][readProductionRecipeCSVRow[j]], 1);
                        productionRecipeKey.Add((int)recipeSet[recipeIndex[i]][readProductionRecipeCSVRow[j]]);
                    }
                    else
                    {
                        productionRecipeDictionary[(int)recipeSet[recipeIndex[i]][readProductionRecipeCSVRow[j]]]++;
                    }
                }
            }

            
            for (int k = 0; k < productionRecipeKey.Count; k++)
            {
                if (Inventory.instance.inventoryItemIDCount.ContainsKey(productionRecipeKey[k]))
                {
                    if (Inventory.instance.inventoryItemIDCount[productionRecipeKey[k]] >= productionRecipeDictionary[productionRecipeKey[k]])
                    {
                        if (k == productionRecipeKey.Count - 1)
                        {
                            if (productionItemType == 1)
                            {
                                if (productionWeaponRecpieList[k].sprite != UIGeneralManager.instance.productionRecipeOn)
                                {
                                    productionWeaponRecpieList[k].sprite = UIGeneralManager.instance.productionRecipeOn;
                                }
                            }
                            else
                            {
                                if (productionPotionRecpieList[k].sprite != UIGeneralManager.instance.productionRecipeOn)
                                {
                                    productionPotionRecpieList[k].sprite = UIGeneralManager.instance.productionRecipeOn;
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }

    // CSV에서 제작 타입 갯수를 센다.
    private void CountProductionType()
    {
        countProductionType = 0;

        for (int i = 0; i < recipeSet.Count; i++)
        {
            if (productionItemType == (int)recipeSet[i]["ProuctionType"])
            {
                countProductionType++;
                recipeIndex.Add(i);

            }
        }
    }
}
