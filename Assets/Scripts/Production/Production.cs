using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Production : MonoBehaviour
{
    #region PRIVATE
    #endregion

    #region PUBLIC
    [HideInInspector]
    public List<Image> weaponRecpieList;
    [HideInInspector]
    public List<Image> potionRecpieList;
    [HideInInspector]
    public List<Dictionary<string, object>> weaponRecipeSet;
    [HideInInspector]
    public List<Dictionary<string, object>> potionRecipeSet;
    [HideInInspector]
    public List<Dictionary<string, object>> currentRecipeSet;
    [HideInInspector]
    public Dictionary<int, int> recipeMaterialItemID = new Dictionary<int, int>();
    [HideInInspector]
    public List<int> recipeKey = new List<int>();
    [HideInInspector]
    public string[] productionRecipeItemIdRow = { "Item1ID", "Item2ID", "Item3ID", "Item4ID", "Item5ID", "Item6ID" };
    [HideInInspector]
    public string[] productionRecipeItemCountRow = { "Item1Count", "Item2Count", "Item3Count", "Item4Count", "Item5Count", "Item6Count" };
    [HideInInspector]
    public int productionItemType = 0;
    [HideInInspector]
    public int afterProductionItemID;
    public RectTransform productionRecipeSlot;
    [HideInInspector]
    public List<int> currentMaterialItemID = new List<int>();
    [HideInInspector]
    public List<int> currentMaterialCount = new List<int>();
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

            weaponRecpieList.Add(productionRecipeSlotPrefabs.GetComponent<Image>());
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

            potionRecpieList.Add(productionRecipeSlotPrefabs.GetComponent<Image>());
            var productionImage = productionRecipeSlotPrefabs.transform.GetChild(0);
            productionImage.GetComponent<Image>().sprite = ProductionRecipeSpriteManager.instance.productionPotionSprite[i];
            productionImage.GetComponent<Image>().SetNativeSize();
            productionImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        
        if (productionItemType == 0)
        {
            UIGeneralManager.instance.productionWeaponViewport.gameObject.SetActive(true);
            UIGeneralManager.instance.productionPotionViewport.gameObject.SetActive(false);
            currentRecipeSet = weaponRecipeSet;
        }
        else
        {
            UIGeneralManager.instance.productionPotionViewport.gameObject.SetActive(true);
            UIGeneralManager.instance.productionWeaponViewport.gameObject.SetActive(false);
            currentRecipeSet = potionRecipeSet;
        }
    }

    // 특정 아이템의 재료가 다 모였는지 검사한다.
    public void CheckProductionRecipe()
    {
        for (int i = 0; i < currentRecipeSet.Count; i++)
        {
            recipeMaterialItemID.Clear();
            recipeKey.Clear();

            for (int j = 0; j < productionRecipeItemIdRow.Length; j++)
            {
                if ((int)currentRecipeSet[i][productionRecipeItemIdRow[j]] == 0)
                {
                    break;
                }
                else
                {
                    if (!recipeMaterialItemID.ContainsKey((int)currentRecipeSet[i][productionRecipeItemIdRow[j]]))
                    {
                        recipeMaterialItemID.Add((int)currentRecipeSet[i][productionRecipeItemIdRow[j]]
                            , (int)currentRecipeSet[i][productionRecipeItemCountRow[j]]);
                        // productionRecipeKey에 ItemID를 넣음.
                        recipeKey.Add((int)currentRecipeSet[i][productionRecipeItemIdRow[j]]);
                    }
                }
            }

            // productionRecipeKey의 개수만큼 반복함.
            for (int k = 0; k < recipeKey.Count; k++)
            {
                // Inventory의 inventoryITemIDCount의 키에 productionRecipeKey[k]가 있다면
                if (Inventory.instance.inventoryItemIDCount.ContainsKey(recipeKey[k]))
                {
                    // 아이템에 있는 제작 재료의 개수가 레시피에 필요한 제작 개수보다 많거나 같으면
                    if (Inventory.instance.inventoryItemIDCount[recipeKey[k]] >= recipeMaterialItemID[recipeKey[k]])
                    {
                        // 끝까지 검사를 다 했고 조건에 맞는다면
                        if (k == recipeKey.Count - 1)
                        {
                            if (productionItemType == 0)
                            {
                                weaponRecpieList[i].sprite = UIGeneralManager.instance.productionRecipeOn;
                                weaponRecpieList[i].GetComponent<Button>().enabled = true;
                            }
                            else if (productionItemType == 1)
                            {
                                potionRecpieList[i].sprite = UIGeneralManager.instance.productionRecipeOn;
                                potionRecpieList[i].GetComponent<Button>().enabled = true;
                            }
                            break;
                        }
                    }
                    else
                    {
                        if (productionItemType == 0)
                        {
                            weaponRecpieList[i].sprite = UIGeneralManager.instance.productionRecipeOff;
                            weaponRecpieList[i].GetComponent<Button>().enabled = false;
                        }
                        else if (productionItemType == 1)
                        {
                            potionRecpieList[i].sprite = UIGeneralManager.instance.productionRecipeOff;
                            potionRecpieList[i].GetComponent<Button>().enabled = false;
                        }
                        break;
                    }
                }
                else
                {
                    if (productionItemType == 0)
                    {
                        weaponRecpieList[i].sprite = UIGeneralManager.instance.productionRecipeOff;
                        weaponRecpieList[i].GetComponent<Button>().enabled = false;
                    }
                    else if (productionItemType == 1)
                    {
                        potionRecpieList[i].sprite = UIGeneralManager.instance.productionRecipeOff;
                        potionRecpieList[i].GetComponent<Button>().enabled = false;
                    }
                    break;
                }
            }
        }
    }
}
