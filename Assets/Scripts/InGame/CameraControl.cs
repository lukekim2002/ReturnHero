using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    #region PRIVATE
    private Vector2 moveAxis;
    #endregion

    void Start()
    {
        if (GameGeneralManager.instance.curFloor == 0)
        {
            this.GetComponent<Camera>().orthographicSize = 3.94f;

            moveAxis = new Vector2(0, 1);
        }

        else
        {
            this.GetComponent<Camera>().orthographicSize = 2.88f;

            moveAxis = new Vector2(1, 1);
        }
    }

    private void Update()
    {
        if (GameGeneralManager.instance.curFloor == 0)
        {
            Vector2 moveCamreaPos;
            if (HeroGeneralManager.instance.heroObject.transform.position.y < 8.8f && HeroGeneralManager.instance.heroObject.transform.position.y > -7.5f)
            {
                moveCamreaPos = HeroGeneralManager.instance.heroObject.transform.position * moveAxis;

                this.transform.position = moveCamreaPos;
            }

            else if (HeroGeneralManager.instance.heroObject.transform.position.y > 8.8f)
            {
                moveCamreaPos = HeroGeneralManager.instance.heroObject.transform.position * moveAxis;
                moveCamreaPos.y = 8.8f;

                this.transform.position = moveCamreaPos;
            }

            else if (HeroGeneralManager.instance.heroObject.transform.position.y < -7.5f)
            {
                moveCamreaPos = HeroGeneralManager.instance.heroObject.transform.position * moveAxis;
                moveCamreaPos.y = -7.5f;

                this.transform.position = moveCamreaPos;
            }
        }

        if (GameGeneralManager.instance.curFloor == 1)
        {
            Vector2 moveCamreaPos = HeroGeneralManager.instance.heroObject.transform.position * moveAxis;

            if (HeroGeneralManager.instance.heroObject.transform.position.x > -1 && HeroGeneralManager.instance.heroObject.transform.position.x < 1 &&
                HeroGeneralManager.instance.heroObject.transform.position.y > -0.5f && HeroGeneralManager.instance.heroObject.transform.position.y < 6.5f)
            {
                moveCamreaPos = HeroGeneralManager.instance.heroObject.transform.position * moveAxis;

                this.transform.position = moveCamreaPos;
            }

            if (HeroGeneralManager.instance.heroObject.transform.position.x > 1)
            {
                moveCamreaPos.x = 1;

                this.transform.position = moveCamreaPos;
            }

            if (HeroGeneralManager.instance.heroObject.transform.position.x < -1)
            {
                moveCamreaPos.x = -1;

                this.transform.position = moveCamreaPos;
            }

            if (HeroGeneralManager.instance.heroObject.transform.position.y > 6.5f)
            {
                moveCamreaPos.y = 6.5f; 

                this.transform.position = moveCamreaPos;
            }

            if (HeroGeneralManager.instance.heroObject.transform.position.y < -0.5f)
            {
                moveCamreaPos.y = -0.5f;

                this.transform.position = moveCamreaPos;
            }
        }
    }
}