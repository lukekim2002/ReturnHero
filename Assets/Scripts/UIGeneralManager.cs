using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGeneralManager : MonoBehaviour {

    public Canvas setting;
    public Canvas inventory;    
    public Image map;
    public Image shadowPanel;

	// Update is called once per frame
	void Update () {
        // 퍼즈 상태가 아니라면
        if (!GameGeneralManager.isPause)
            // UI 키 입력을 받는다.
            InputSystem();
    }

    private void InputSystem()
    {
        // 지도 온 & 오프 ( 꾹 누르는 키 )
        if (Input.GetKey(KeyCode.Tab))
        {
            map.gameObject.SetActive(true);
            shadowPanel.gameObject.SetActive(true);
        }
        else
        {
            map.gameObject.SetActive(false);
            shadowPanel.gameObject.SetActive(false);
        }

        // 메뉴 온 & 오프 및 퍼즈 ( 한 번 누르는 키 )
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameGeneralManager.isPause = true;
            setting.gameObject.SetActive(GameGeneralManager.isPause);
            Time.timeScale = 0;
        }

        // 인벤토리 온 & 오픈 ( 한 번 누르는 키 )
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameGeneralManager.isInventory = !GameGeneralManager.isInventory;
            inventory.gameObject.SetActive(GameGeneralManager.isInventory);
        }
    }
}
