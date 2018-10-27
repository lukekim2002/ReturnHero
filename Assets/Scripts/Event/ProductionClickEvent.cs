using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionClickEvent : MonoBehaviour
{

    #region PRIVATE
    #endregion

    #region PUBLIC
    #endregion

    // Production Left Image를 클릭할 때 Production Right Image가 드러남
    public void OnClickProductionImage()
    {
        UIGeneralManager.instance.productionCanvas.transform.GetChild(2).gameObject.SetActive(true);
        Debug.Log("Open");
    }

    public void OnClickBackground()
    {
        UIGeneralManager.instance.isInventoryOpened = false;

        


        UIGeneralManager.instance.inventoryCanvas.gameObject.SetActive(false);
        UIGeneralManager.instance.productionCanvas.gameObject.SetActive(false);
        UIGeneralManager.instance.productionCanvas.transform.GetChild(2).gameObject.SetActive(false);

        Debug.Log("Close");
    }
}
