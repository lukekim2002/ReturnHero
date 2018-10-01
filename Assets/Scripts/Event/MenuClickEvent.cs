using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClickEvent : MonoBehaviour
{
    public Canvas option;

    public void OnClickOption()
    {
        option.gameObject.SetActive(true);
    }

    public void OnClickGameExitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
