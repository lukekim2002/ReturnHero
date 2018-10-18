using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 메뉴 버튼 관련 이벤트 클래스
public class MenuClickEvent : MonoBehaviour
{

    // Resume Button Click Event
    public void OnClickResumeButton()
    {
        UIGeneralManager.instance.isPause = false;
        UIGeneralManager.instance.settingCanvas.gameObject.SetActive(UIGeneralManager.instance.isPause);
        Time.timeScale = 1f;
    }

    // Option Button Click Event
    public void OnClickOptionButton()
    {
        UIGeneralManager.instance.isOptionOpened = true;
        UIGeneralManager.instance.optionCanvas.gameObject.SetActive(true);
    }

    // Game Exit Button Click Event
    public void OnClickGameExitButton()
    {
        PlayerPrefs.Save();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
