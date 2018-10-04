using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionMenuClickEvent : MonoBehaviour
{
    public Canvas option;

    // Option Button Click Event
    public void OnClickExitButton()
    {
        UIGeneralManager.isOptionOpened = false;
        option.gameObject.SetActive(false);
        PlayerPrefs.Save();
    }

    public void OnValueChangedFullScreenCheckBox()
    {
        PlayerPrefs.SetInt("FullScreenOn", this.GetComponent<Toggle>().isOn == true ? 1 : 0);
    }

    public void OnValueChangedBrightnessSlider()
    {
        PlayerPrefs.SetFloat("Brightness", this.GetComponent<Slider>().value);
    }

    public void OnValueChangeBGMSlider()
    {
        PlayerPrefs.SetFloat("BGM", this.GetComponent<Slider>().value);
    }

    public void OnValueChangeSoundEffectSlider()
    {
        PlayerPrefs.SetFloat("SoundEffect", this.GetComponent<Slider>().value);
    }

    public void OnValueChangeCameraShaketSlider()
    {
        PlayerPrefs.SetFloat("CameraShake", this.GetComponent<Slider>().value);
    }

    public void OnValueChangedVSYNCCheckBox()
    {
        PlayerPrefs.SetInt("V-SYNC", this.GetComponent<Toggle>().isOn == true ? 1 : 0);
    }
}
