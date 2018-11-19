using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionListEvent : MonoBehaviour
{
    #region PRIVATE
    // 현재 포커스가 Weapon Button인지 Potion Button인지 여부
    // static 키워드를 통해 미리 메모리에 할당시켜서 각 UI Component가 하나의 Bool 변수 상태를 공유할 수 있음.
    private static bool isProductionListChange = false;
    #endregion

    #region PUBLIC
    public Button weaponButton;
    public Button potionButton;

    public Image productionListImage;
    public Image productionButtonImage;

    public Sprite weaponOnImage;
    public Sprite weaponOffImage;
    public Sprite potionOnImage;
    public Sprite potionOffImage;
    public Sprite productionListOpenImage;
    public Sprite productionListCloseImage;
    #endregion

    // Weapon Button PointerEnter Event
    public void PointerEnterWeaponButton()
    {
        // Weapon Button 이미지를 WeaponOn으로 교체한다.
        weaponButton.GetComponent<Image>().sprite = weaponOnImage;
        // Potion Button 이미지를 PotionOff로 교체한다.
        potionButton.GetComponent<Image>().sprite = potionOffImage;
    }

    // Potion Button PointerEnter Event
    public void PointerEnterPotionButton()
    {
        // Weapon Button 이미지를 WeaponOff로 교체한다.
        weaponButton.GetComponent<Image>().sprite = weaponOffImage;
        // Potion Button 이미지를 PotionOn으로 교체한다.
        potionButton.GetComponent<Image>().sprite = potionOnImage;
    }

    // ProductionList Button Click Event
    public void OnClickProductionListButton()
    {
        // ProuctionList Button의 활성화/ 비활성화를 서로 바꿔준다.
        isProductionListChange = !isProductionListChange;

        // 활성화 상태라면
        if (isProductionListChange == true)
        {
            // ProductionList Button 이미지를 productionList_On으로 바꾼다.
            productionButtonImage.sprite = productionListOpenImage;
            // ProductionList 이미지를 활성화.
            productionListImage.gameObject.SetActive(true);
        }
        else
        {
            // ProductionList Button 이미지를 productionList_On으로 바꾼다.
            productionButtonImage.sprite = productionListCloseImage;
            // ProductionList 이미지를 비활성화.
            productionListImage.gameObject.SetActive(false);
        }
    }

    // Weapon Button Click Event
    public void OnClickWeaponButton()
    {
        weaponButton.GetComponent<Image>().sprite = weaponOnImage;
        potionButton.GetComponent<Image>().sprite = potionOffImage;

        var productionComponent = UIGeneralManager.instance.productionCanvas.GetComponent<Production>();
        productionComponent.productionItemType = 1;
        UIGeneralManager.instance.productionWeaponViewportContent.gameObject.SetActive(true);
        UIGeneralManager.instance.productionPotionViewportContent.gameObject.SetActive(false);
        productionComponent.CheckMaterialItems();

        OnClickProductionListButton();
    }

    // Potion Button Click Event
    public void OnClickPotionButton()
    {
        weaponButton.GetComponent<Image>().sprite = weaponOffImage;
        potionButton.GetComponent<Image>().sprite = potionOnImage;

        var productionComponent = UIGeneralManager.instance.productionCanvas.GetComponent<Production>();
        productionComponent.productionItemType = 2;
        UIGeneralManager.instance.productionPotionViewportContent.gameObject.SetActive(true);
        UIGeneralManager.instance.productionWeaponViewportContent.gameObject.SetActive(false);
        productionComponent.CheckMaterialItems();

        OnClickProductionListButton();
    }
}
