using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveInBossStage : MonoBehaviour
{

    #region PRIVATE
    #endregion

    #region PUBLIC
    public float duration = 5.0f;
    #endregion

    // Use this for initialization
    void Start()
    {
        StartCoroutine(MoveCamera());
    }

    // 보스신 카메라 움직임
    public IEnumerator MoveCamera()
    {
        var timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            this.transform.position = Vector2.Lerp(new Vector2(0, 0), new Vector2(0, 13), timer / duration);

            yield return null;
        }

        yield break;
    }
}
