using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGeneralManager : MonoBehaviour {

    public Canvas setting;
    public Image map;
    public Image shadowPanel;

	// Update is called once per frame
	void Update () {
        InputSystem();
    }

    private void InputSystem()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            map.gameObject.SetActive(true);
            shadowPanel.gameObject.SetActive(true);
            print("true");
        }
        else
        {
            map.gameObject.SetActive(false);
            shadowPanel.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameGeneralManager.isPause = true;
            setting.gameObject.SetActive(GameGeneralManager.isPause);
            Time.timeScale = 0;
        }
    }
}
