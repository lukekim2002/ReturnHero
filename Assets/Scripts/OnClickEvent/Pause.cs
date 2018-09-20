using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public Canvas setting;

    public void ClickPasueButton()
    {
        GameGeneralManager.isPause = false;
        setting.gameObject.SetActive(GameGeneralManager.isPause);
        Time.timeScale = 1f;
    }
}
