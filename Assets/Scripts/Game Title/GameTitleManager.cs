using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTitleManager : MonoBehaviour {

    public Canvas canvas;
    public Button nomarlMode;
    public Button hardMode;
    public Button obtions;
    public Button develpers;
    public Button gameExit;

    public bool isHardMode = false;

    private void Awake()
    {
        // 하드모드 활성화
        if (isHardMode == true)
        {
            Button instantiateButton;
            instantiateButton = Instantiate(nomarlMode, new Vector2(606, 260), Quaternion.identity);
            instantiateButton.transform.SetParent(canvas.transform);
            instantiateButton = Instantiate(hardMode, new Vector2(606, 220), Quaternion.identity);
            instantiateButton.transform.SetParent(canvas.transform);
            instantiateButton = Instantiate(obtions, new Vector2(606, 180), Quaternion.identity);
            instantiateButton.transform.SetParent(canvas.transform);
            instantiateButton = Instantiate(develpers, new Vector2(606, 140), Quaternion.identity);
            instantiateButton.transform.SetParent(canvas.transform);
            instantiateButton = Instantiate(gameExit, new Vector2(606, 100), Quaternion.identity);
            instantiateButton.transform.SetParent(canvas.transform);
        }

        // 하드모드 비활성화
        if (isHardMode == false)
        {
            Button instantiateButton;
            instantiateButton = Instantiate(nomarlMode, new Vector2 (606, 260), Quaternion.identity);
            instantiateButton.transform.SetParent(canvas.transform);
            instantiateButton = Instantiate(obtions, new Vector2(606, 220), Quaternion.identity);
            instantiateButton.transform.SetParent(canvas.transform);
            instantiateButton = Instantiate(develpers, new Vector2(606, 180), Quaternion.identity);
            instantiateButton.transform.SetParent(canvas.transform);
            instantiateButton = Instantiate(gameExit, new Vector2(606, 140), Quaternion.identity);
            instantiateButton.transform.SetParent(canvas.transform);
        }
    }
}
