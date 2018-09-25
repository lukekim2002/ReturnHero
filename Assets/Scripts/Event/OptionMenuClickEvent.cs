using UnityEngine;
using System.Collections;

public class OptionMenuClickEvent : MonoBehaviour
{
    public Canvas option;

    public void OnClickExitButton()
    {
        option.gameObject.SetActive(false);
    }
}
