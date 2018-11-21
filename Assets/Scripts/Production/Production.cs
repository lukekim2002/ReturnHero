using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Production : MonoBehaviour
{
    #region PRIVATE
    private string[] readProductionRecipeCSVRow = { "Item1", "Item2", "Item3", "Item4", "Item5", "Item6" };
    #endregion

    #region PUBLIC
    [HideInInspector]
    public List<Image> productionWeaponRecpieList;
    [HideInInspector]
    public List<Image> productionPotionRecpieList;
    [HideInInspector]
    public List<Dictionary<string, object>> weaponRecipeSet;
    [HideInInspector]
    public List<Dictionary<string, object>> potionRecipeSet;
    [HideInInspector]
    public List<Dictionary<string, object>> recipeSet;
    [HideInInspector]
    public Dictionary<int, int> productionRecipeDictionary = new Dictionary<int, int>();
    [HideInInspector]
    public List<int> productionRecipeKey = new List<int>();
    // 1 = Weapon, 2 = Potion
    [HideInInspector]
    public int productionItemType = 0;
    public int afterProductionItemID;
    public RectTransform productionRecipeSlot;

    #endregion

    private void Awake()
    {
        weaponRecipeSet = CSVReader.Read("CSV/Item/ProductionWeaponRecipe");
        potionRecipeSet = CSVReader.Read("CSV/Item/ProductionPotionRecipe");
    }

    private void Start()
    {
        // Weapon Recipe Image 배치
        for (int i = 0; i < weaponRecipeSet.Count; i++)
        {
            RectTransform productionRecipeSlotPrefabs = Instantiate(productionRecipeSlot);

            productionRecipeSlotPrefabs.SetParent(UIGeneralManager.instance.productionWeaponViewport.GetChild(0));
            productionRecipeSlotPrefabs.localScale = new Vector2(1, 1);
            productionRecipeSlotPrefabs.GetComponent<ProductionRecipeEvent>().slotNum = i;

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

        // Potion Recipe Image 배치
        for (int i = 0; i < potionRecipeSet.Count; i++)
        {
            RectTransform productionRecipeSlotPrefabs = Instantiate(productionRecipeSlot);

            productionRecipeSlotPrefabs.SetParent(UIGeneralManager.instance.productionPotionViewport.GetChild(0));
            productionRecipeSlotPrefabs.localScale = new Vector2(1, 1);
            productionRecipeSlotPrefabs.GetComponent<ProductionRecipeEvent>().slotNum = i;

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
        
        if (productionItemType == 0)
        {
            UIGeneralManager.instance.productionWeaponViewport.gameObject.SetActive(true);
            UIGeneralManager.instance.productionPotionViewport.gameObject.SetActive(false);
            recipeSet = weaponRecipeSet;
        }
        else
        {
            UIGeneralManager.instance.productionPotionViewport.gameObject.SetActive(true);
            UIGeneralManager.instance.productionWeaponViewport.gameObject.SetActive(false);
            recipeSet = potionRecipeSet;
        }
    }

    //TODO : 아이템 재료가 사라지면 Recipe off 시켜줘야 한다.
    // 특정 아이템의 재료가 다 모였는지 검사한다.
    public void CheckMaterialItems()
    {
        // Production Type의 개수만큼 반복
        for (int i = 0; i < recipeSet.Count; i++)
        {
            // 어떤 아이템을 조합하기 위한 조합식을 매 행마다 초기화해준다.
            productionRecipeDictionary.Clear();
            // 어떤 아이템을 조합하는데 필요한 아이템들을 매 행마다 초기화해준다.
            productionRecipeKey.Clear();

            // ProductionRecipe.CSV에서 Item1 ~ Item6까지 반복한다.
            for (int j = 0; j < readProductionRecipeCSVRow.Length; j++)
            {
                // 만약 현재 Row에 있는 현재 Column에 들어간 ItemID가 0이라면 그냥 넘어감.
                if ((int)recipeSet[i][readProductionRecipeCSVRow[j]] == 0)
                {
                    continue;
                }
                // 만약 현재 Row에 있는 현재 Column에 들어간 ItemID가 0이 아니라면 계속 검사.
                else
                {
                    // 만약 현재 Row에 있는 현재 Column에 들어간 ItemID가 productionRecipeDictinary에 들어가지 않았다면
                    if (!productionRecipeDictionary.ContainsKey((int)recipeSet[i][readProductionRecipeCSVRow[j]]))
                    {
                        // productionRecipeDictionary에 현재 CSV에서 가리키고 있는 ItemID를 키로 집어넣고 값으로 1을 집어넣음.
                        productionRecipeDictionary.Add((int)recipeSet[i][readProductionRecipeCSVRow[j]], 1);
                        // productionRecipeKey에 ItemID를 넣음.
                        productionRecipeKey.Add((int)recipeSet[i][readProductionRecipeCSVRow[j]]);
                    }
                    // 만약 현재 Row에 있는 현재 Column에 들어간 ItemID가 productionRecipeDictinary에 들어갔다면
                    else
                    {
                        // IteproductionRecipeDictionary[ItemID]에 있는 값을 하나씩 올림.
                        productionRecipeDictionary[(int)recipeSet[i][readProductionRecipeCSVRow[j]]]++;
                    }
                }
            }

            // productionRecipeKey의 개수만큼 반복함.
            for (int k = 0; k < productionRecipeKey.Count; k++)
            {
                // Inventory의 inventoryITemIDCount의 키에 productionRecipeKey[k]가 있다면
                if (Inventory.instance.inventoryItemIDCount.ContainsKey(productionRecipeKey[k]))
                {
                    if (Inventory.instance.inventoryItemIDCount[productionRecipeKey[k]] >= productionRecipeDictionary[productionRecipeKey[k]])
                    {
                        if (k == productionRecipeKey.Count - 1)
                        {
                            if (productionItemType == 0)
                            {
                                productionWeaponRecpieList[k].sprite = UIGeneralManager.instance.productionRecipeOn;
                                productionWeaponRecpieList[k].GetComponent<Button>().enabled = true;
                            }
                            else if (productionItemType == 1)
                            {
                                productionPotionRecpieList[k].sprite = UIGeneralManager.instance.productionRecipeOn;
                                productionPotionRecpieList[k].GetComponent<Button>().enabled = true;
                            }
                            else
                            {
                                if (productionItemType == 0)
                                {
                                    productionWeaponRecpieList[k].sprite = UIGeneralManager.instance.productionRecipeOff;
                                    productionWeaponRecpieList[k].GetComponent<Button>().enabled = false;
                                }
                                else if (productionItemType == 1)
                                {
                                    productionPotionRecpieList[k].sprite = UIGeneralManager.instance.productionRecipeOff;
                                    productionPotionRecpieList[k].GetComponent<Button>().enabled = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (productionItemType == 0)
                        {
                            productionWeaponRecpieList[k].sprite = UIGeneralManager.instance.productionRecipeOff;
                            productionWeaponRecpieList[k].GetComponent<Button>().enabled = false;
                        }
                        else if (productionItemType == 1)
                        {
                            productionPotionRecpieList[k].sprite = UIGeneralManager.instance.productionRecipeOff;
                            productionPotionRecpieList[k].GetComponent<Button>().enabled = false;
                        }
                    }
                }
                else
                {
                    if (productionItemType == 0)
                    {
                        productionWeaponRecpieList[k].sprite = UIGeneralManager.instance.productionRecipeOff;
                        productionWeaponRecpieList[k].GetComponent<Button>().enabled = false;
                    }
                    else if (productionItemType == 1)
                    {
                        productionPotionRecpieList[k].sprite = UIGeneralManager.instance.productionRecipeOff;
                        productionPotionRecpieList[k].GetComponent<Button>().enabled = false;
                    }
                    break;
                }
            }
        }
    }
}
