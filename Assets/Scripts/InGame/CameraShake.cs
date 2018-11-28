using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    //TODO : 카메라 세이크 아름답게 할 것

    #region PRIVATE
    private const float cameraShakeAmount = 0.9f;
    private const float cameraShakeDuration = 0.2f;
    #endregion

    #region PUBLIC
    public Camera mainCamera;

    #endregion

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = this.GetComponent<Camera>();   
    }

    public void DoShake()
    {
        StartCoroutine(BeginShake());
    }

    public IEnumerator BeginShake()
    {
        Vector2 mainCameraPos = transform.localPosition;
        float elapsed = 0.0f;

        while(elapsed < cameraShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * PlayerPrefs.GetFloat("CameraShake");
            float y = Random.Range(-1f, 1f) * PlayerPrefs.GetFloat("CameraShake");

            transform.localPosition = new Vector2(x, y);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = mainCameraPos;
    }
}
