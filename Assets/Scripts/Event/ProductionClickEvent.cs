using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProductionClickEvent : MonoBehaviour
{
    public Sprite productionListOpenImage;
    public Sprite productionListCloseImage;
    public Image productionList;

    private bool isProductionListChange = false;
    private Image productionListImage;

    private void Awake()
    {
        productionListImage = GetComponent<Image>();
    }

    public void OnClickProductionList()
    {
        isProductionListChange = !isProductionListChange;

        if (isProductionListChange == true)
        {
            productionListImage.sprite = productionListOpenImage;
            productionList.gameObject.SetActive(true);
        }
        else
        {
            productionListImage.sprite = productionListCloseImage;
            productionList.gameObject.SetActive(false);
         }
    }
}
