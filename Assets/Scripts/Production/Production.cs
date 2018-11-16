using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour
{

    private string[] readProductionRecipeCSVRow = { "Item1", "Item2", "Item3", "Item4", "Item5", "Item6" };
    private int[] productionSlotItemID = new int[6];

    //public static Production instance;
    public GameObject[] itemMaterials = new GameObject[6];
    public List<Dictionary<string, object>> recipeSet;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}

        recipeSet = CSVReader.Read("CSV/Item/ProductionRecipe");

        InsertItemIDInArray();
        SortItemID();
    }

    // 특정 아이템의 재료가 다 모였는지 검사한다.
    public void CheckMaterialItems()
    {
        InsertItemIDInArray();
        SortItemID();

        print((int)recipeSet[0][readProductionRecipeCSVRow[0]]);

        for (int i = 0; i < recipeSet.Count; i++)
        {
            if ((int)recipeSet[i][readProductionRecipeCSVRow[0]] == productionSlotItemID[0] &&
                (int)recipeSet[i][readProductionRecipeCSVRow[1]] == productionSlotItemID[1] &&
                (int)recipeSet[i][readProductionRecipeCSVRow[2]] == productionSlotItemID[2] &&
                (int)recipeSet[i][readProductionRecipeCSVRow[3]] == productionSlotItemID[3] &&
                (int)recipeSet[i][readProductionRecipeCSVRow[4]] == productionSlotItemID[4] &&
                (int)recipeSet[i][readProductionRecipeCSVRow[5]] == productionSlotItemID[5])
            {
                UIGeneralManager.instance.productionSelectButton.sprite = UIGeneralManager.instance.productionSelectButtonOn;
                break;
            }
            else
            {
                UIGeneralManager.instance.productionSelectButton.sprite = UIGeneralManager.instance.productionSelectButtonOff;
            }
        }
    }

    private void InsertItemIDInArray()
    {
        for (int i = 0; i < productionSlotItemID.Length; i++)
        {
            productionSlotItemID[i] = itemMaterials[i].GetComponent<Slot>().item.itemID;
        }
    }

    private void SortItemID()
    {
        Array.Sort(productionSlotItemID);
    }
}
