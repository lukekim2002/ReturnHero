using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region PRIVATE

    #endregion

    #region PUBLIC
    public GameObject prefHero;
    #endregion

    void Start () {
        Instantiate(prefHero, new Vector2(0, 0), Quaternion.identity);
	}
}
