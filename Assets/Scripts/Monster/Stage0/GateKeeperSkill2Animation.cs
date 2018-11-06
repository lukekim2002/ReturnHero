using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeperSkill2Animation : MonoBehaviour {

    BoxCollider2D myCollider;
    Transform oldParentTransform;


    private void OnEnable()
    {
        oldParentTransform = transform.parent;
        transform.parent = transform.parent.parent;
    }

    private void OnDisable()
    {
        transform.parent = oldParentTransform;
    }

    public void TurnOnBoxCollider()
    {
        myCollider = transform.GetComponent<BoxCollider2D>();
        myCollider.enabled = true;
    }

    public void TurnOffBoxCollider()
    {
        myCollider = transform.GetComponent<BoxCollider2D>();
        myCollider.enabled = false;
        gameObject.SetActive(false);
    }


}
