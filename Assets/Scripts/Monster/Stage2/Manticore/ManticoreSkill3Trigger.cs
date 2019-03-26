using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManticoreSkill3Trigger : MonoBehaviour
{
    ManticoreClass myBehaviour;

    private void Awake()
    {
        myBehaviour = transform.root.transform.GetComponent<ManticoreClass>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myBehaviour._isSkill3TriggerOk = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        myBehaviour._isSkill3TriggerOk = true;
    }
}
