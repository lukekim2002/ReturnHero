using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class SceneUtility : MonoBehaviour {

    public GameObject portalSet;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

        try
        {
            // CurFloor(스테이지 빌드 인덱스)를 받아와 이전 층을 unload한다.
            // CurFloor == 2 이면 현재 스테이지는 Stage1_2이고 stage1_1(curFloor = 1)을 unload.
            if (SceneManager.GetSceneByBuildIndex(GameGeneralManager.instance.curFloor - 1) != null)
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(GameGeneralManager.instance.curFloor - 1));
        }
        catch (Exception e)
        {
            // 여기에 예외처리하는 구문을 적어야하는데 아직 실력이 부족해 어떻게 넘겨야하는지 모르겠음.
            Debug.LogException(e, this);
        }
        finally
        {
            HeroGeneralManager.instance.heroObject.transform.position = Vector3.zero;

            portalSet = GameObject.FindWithTag("Portal");
            Debug.Log(GameGeneralManager.instance.curFloor + " " + GameObject.FindWithTag("Portal").gameObject.name);
            Debug.Log(GameGeneralManager.instance.curFloor + " "  + portalSet.name);

            if (portalSet != null)
            {
                SetPortalInactive();
                ActiveRandomPortal();
            }
        }
        

    }

    private void SetPortalInactive()
    {
        for (int i = 0; i < portalSet.transform.childCount; i++)
        {
            portalSet.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void ActiveRandomPortal()
    {
        int i = Random.Range(0, 4);
        portalSet.transform.GetChild(i).gameObject.SetActive(true);
    }

}
