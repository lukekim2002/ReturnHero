using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnteringRoom : MonoBehaviour {

   GameObject root;

	// Use this for initialization
	void Start ()
    {
        root = transform.root.gameObject;
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            root.SendMessage("SetChildrenIsTriggerOff", SendMessageOptions.DontRequireReceiver);
        }
    }
}
