using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public Canvas setting;

    public void ClickPasueButton()
    {
        setting.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
