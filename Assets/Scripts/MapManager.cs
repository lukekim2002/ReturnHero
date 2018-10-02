using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public GameObject gateKeeper;

	// Use this for initialization
	void Start () {
		
	}

    public void SetChildrenIsTriggerOn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BoxCollider2D boxCollider2D = transform.GetChild(i).GetComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;
        }
    }

    public void SetChildrenIsTriggerOff()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BoxCollider2D boxCollider2D = transform.GetChild(i).GetComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = false;

            gateKeeper.SetActive(true);
        }
    }

    
}
