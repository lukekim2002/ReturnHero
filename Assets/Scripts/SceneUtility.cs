using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class SceneUtility : MonoBehaviour {

    public GameObject portalSet;
    Scene prevScene;
    bool isSceneLoadedForFirst;

    private void OnEnable()
    {
        isSceneLoadedForFirst = false;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene curScene, LoadSceneMode arg1)
    {
        if (curScene.name.Contains("Stage") ||
            curScene.name.Contains("Town"))
        {
            //Debug.Log(curScene.name);

            /* flag for checking loading first scene
             * 
             */
            if (isSceneLoadedForFirst == true)
            {
                StartCoroutine(WaitForSceneUnload(prevScene));
                prevScene = curScene;

            }
            else
            {
                isSceneLoadedForFirst = true;
                prevScene = curScene;
                portalSet = GameObject.FindWithTag("Portal");


                SetPortalInactive();
                ActiveRandomPortal();
            }
            
           
        }



       

    }

    public IEnumerator WaitForSceneUnload(Scene sceneToUnload)
    {
        AsyncOperation async = SceneManager.UnloadSceneAsync(sceneToUnload);
        yield return async;

        HeroGeneralManager.instance.heroObject.transform.position = Vector3.zero;
        portalSet = GameObject.FindWithTag("Portal");
        SetPortalInactive();
        ActiveRandomPortal();

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
        int i = Random.Range(0, portalSet.transform.childCount + 1);

        if (i >= portalSet.transform.childCount)
            i = 0;

        //Debug.Log(i);
        portalSet.transform.GetChild(i).gameObject.SetActive(true);
    }

}
