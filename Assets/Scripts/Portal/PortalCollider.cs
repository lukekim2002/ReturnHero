using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalCollider : MonoBehaviour {

    int nextSceneNum;
    bool isCalledAlready = false;

    private void Start()
    {
        isCalledAlready = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = Vector2.zero;
            if (isCalledAlready == false)
            {
                isCalledAlready = true;
                nextSceneNum = GameGeneralManager.instance.curFloor + 1;
                GameGeneralManager.instance.curFloor = nextSceneNum;

                SceneManager.LoadScene(nextSceneNum, LoadSceneMode.Additive);
            }


        }
    }
}
