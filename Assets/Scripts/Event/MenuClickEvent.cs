using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 메뉴 버튼 관련 이벤트 클래스
public class MenuClickEvent : MonoBehaviour
{

    // Resume Button Click Event
    public void OnClickResumeButton()
    {
        UIGeneralManager.isPause = false;
        UIGeneralManager.setting.gameObject.SetActive(UIGeneralManager.isPause);
        Time.timeScale = 1f;
    }

    // Option Button Click Event
    public void OnClickOptionButton()
    {
        UIGeneralManager.isOptionOpened = true;
        UIGeneralManager.option.gameObject.SetActive(true);
    }

    // Game Exit Button Click Event
    public void OnClickGameExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
