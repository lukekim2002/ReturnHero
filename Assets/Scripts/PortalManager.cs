using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour {

    public GameObject wayPointSet;

    private void OnEnable()
    {
        if(SceneManager.GetSceneByBuildIndex(GameGeneralManager.instance.curFloor - 1) != null)
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(GameGeneralManager.instance.curFloor-1));

        HeroGeneralManager.instance.heroObject.transform.position = Vector2.zero;

        SceneManager.sceneLoaded += OnSceenLoaded;
    }

    private void OnSceenLoaded(Scene arg0, LoadSceneMode arg1)
    {
        
    }

    private void Start()
    {
        
        SetWayPointInactive();
        SetRandomWayPoint();
    }

    private void SetWayPointInactive()
    {
        for (int i = 0; i < wayPointSet.transform.childCount; i++)
        {
            wayPointSet.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void SetRandomWayPoint()
    {
        int i = Random.Range(0, 4);
        if (i >= wayPointSet.transform.childCount)
            i = 0;
        wayPointSet.transform.GetChild(i).gameObject.SetActive(true);
    }


}
