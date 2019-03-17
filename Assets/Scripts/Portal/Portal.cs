using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
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
            // 비활성화된 Portal Animator를 활성화 시키면 애니메이션 자동 실행
            this.GetComponent<Animator>().enabled = true;
        }
    }

    // 포탈 애니메이션의 맨 마지막에 Animation Event 박아주고 이 함수를 실행
    public void WarpTotheNextScene()
    {
        if (isCalledAlready == false)
        {
            isCalledAlready = true;
            nextSceneNum = GameGeneralManager.instance.curFloor + 1;
            GameGeneralManager.instance.curFloor = nextSceneNum;

            HeroGeneralManager.instance.heroObject.transform.position = Vector2.zero;
            SceneManager.LoadScene(nextSceneNum, LoadSceneMode.Additive);
        }
    }
}
