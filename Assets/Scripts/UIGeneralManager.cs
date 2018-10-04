using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI 시스템을 관리하는 클래스
public class UIGeneralManager : MonoBehaviour
{
    // 이 클래스의 전역 instacne 변수로 다른 클래스에 의해 엑세스할 수 있다.
    public static UIGeneralManager instance = null;
    // Pause 상태 여부
    public static bool isPause = false;
    // Inventory Open 상태 여부
    public static bool isInventoryOpened = false;
    // Option Open 상태 여부
    public static bool isOptionOpened = false;

    public static Canvas setting;
    public Canvas settingCanvas;
    public static Canvas inventory;
    public Canvas inventoryCanvas;
    public static Canvas option;
    public Canvas optionCanvas;
    public Image map;
    public Image shadowPanel;

    private int _isFullScreenOn = 1;

    private void Awake()
    {
        // Check if instance doesn't exist
        if (instance == null)
        {
            instance = this;
        }

        // If instance already exists and it's not this
        else if (instance != this)
        {
            // then destroy this. This enforces our singleton pattern, meaning there caan only one instance of GGM.
            Destroy(this);
        }

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        // static 변수에 값 대입
        CreateStaticVariable();
        InsertUIData();
    }

    void Update()
    {
        // 퍼즈 상태가 아니라면
        if (!isPause)
            // UI 키 입력을 받는다.
            InputSystem();
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isOptionOpened == false)
            {
                isPause = false;
                setting.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
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

            if (!isPause)
            {
                isPause = true;
                setting.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else if (isPause)
            {
                isPause = false;
                setting.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        // 인벤토리 온 & 오픈 ( 한 번 누르는 키 )
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpened = !isInventoryOpened;
            inventory.gameObject.SetActive(isInventoryOpened);
        }
    }

    private void CreateStaticVariable()
    {
        setting = settingCanvas;
        inventory = inventoryCanvas;
        option = optionCanvas;
    }

    private void InsertUIData()
    {
        option.transform.GetChild(2).GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("FullScreenOn") == 1 ? true : false;
        option.transform.GetChild(3).GetComponent<Slider>().value = PlayerPrefs.GetFloat("Brightness");
        option.transform.GetChild(4).GetComponent<Slider>().value = PlayerPrefs.GetFloat("BGM");
        option.transform.GetChild(5).GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundEffect");
        option.transform.GetChild(6).GetComponent<Slider>().value = PlayerPrefs.GetFloat("CameraShake");
        option.transform.GetChild(2).GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("V-SYNC") == 1 ? true : false;
    }
}
