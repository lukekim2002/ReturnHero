using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour {

    public static Production instance;
    public GameObject[] itemMaterials = new GameObject[6];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // 특정 아이템의 재료가 다 모였는지 검사한다.
    public void CheckMaterialItems()
    {
        
    }
}
