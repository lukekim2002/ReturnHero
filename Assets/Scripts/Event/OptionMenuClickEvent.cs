using UnityEngine;
using System.Collections;

public class OptionMenuClickEvent : MonoBehaviour
{
    public Canvas option;

    // Option Button Click Event
    public void OnClickExitButton()
    {
        UIGeneralManager.isOptionOpened = false;
        option.gameObject.SetActive(false);
    }
}
