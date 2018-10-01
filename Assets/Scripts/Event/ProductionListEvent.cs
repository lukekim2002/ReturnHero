using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionListEvent : MonoBehaviour {

    public Button weaponButton;
    public Button potionButton;

    private Image weaponSourceImage;
    private Image potionSourceImage;

    public Sprite weaponOnImage;
    public Sprite weaponOffImage;
    public Sprite potionOnImage;
    public Sprite potionOffImage;

    private void Awake()
    {
        weaponSourceImage = weaponButton.GetComponent<Image>();
        potionSourceImage = potionButton.GetComponent<Image>();
    }

    public void PointerEnterWeaponImage()
    {
        weaponSourceImage.sprite = weaponOnImage;
        potionSourceImage.sprite = potionOffImage;
    }

    public void PointerEnterPotionImage()
    {
        weaponSourceImage.sprite = weaponOffImage;
        potionSourceImage.sprite = potionOnImage;
    }
}
