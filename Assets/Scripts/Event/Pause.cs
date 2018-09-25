using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public Canvas setting;

    // Resume 버튼 누를 때
    public void ClickResumeButton()
    {
        GameGeneralManager.isPause = false;
        setting.gameObject.SetActive(GameGeneralManager.isPause);
        Time.timeScale = 1f;
    }
}
