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
    public bool isPause = false;
    // Inventory Open 상태 여부
    public bool isInventoryOpened = false;
    // Option Open 상태 여부
    public bool isOptionOpened = false;

    //public static Canvas setting;
    public Canvas displayUICanvas;
    public Canvas settingCanvas;
    public Canvas inventoryCanvas;
    public Canvas optionCanvas;
    public Image map;
    public Image shadowPanel;
    public Image skill_Space_Image;
    public Image skill_Mr_Image;
    public Image skill_E_Image;
    public Image skill_R_Image;

    private const int FULLSCREEN = 2;
    private const int BRIGHTNESS = 3;
    private const int BGM = 4;
    private const int SOUNTEFFECT = 5;
    private const int CAMERASAHKE = 6;
    private const int V_SYNC = 7;

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
                settingCanvas.gameObject.SetActive(false);
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
                settingCanvas.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else if (isPause)
            {
                isPause = false;
                settingCanvas.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        // 인벤토리 온 & 오픈 ( 한 번 누르는 키 )
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpened = !isInventoryOpened;
            inventoryCanvas.gameObject.SetActive(isInventoryOpened);
        }
    }

    private void InsertUIData()
    {
        optionCanvas.transform.GetChild(FULLSCREEN).GetComponent<Toggle>().isOn = (PlayerPrefs.GetInt("FullScreenOn") == 1 ? true : false);
        optionCanvas.transform.GetChild(BRIGHTNESS).GetComponent<Slider>().value = PlayerPrefs.GetFloat("Brightness");
        optionCanvas.transform.GetChild(BGM).GetComponent<Slider>().value = PlayerPrefs.GetFloat("BGM");
        optionCanvas.transform.GetChild(SOUNTEFFECT).GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundEffect");
        optionCanvas.transform.GetChild(CAMERASAHKE).GetComponent<Slider>().value = PlayerPrefs.GetFloat("CameraShake");
        optionCanvas.transform.GetChild(V_SYNC).GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("V-SYNC") == 1 ? true : false;
    }
}
