using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMeleeTrigger : MonoBehaviour
{
    GameObject parent;
    EagleClass rootBehaviour;

    private void Awake()
    {
        parent = transform.parent.gameObject;
        rootBehaviour = parent.GetComponent<EagleClass>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rootBehaviour._isSkill2TriggerOk = false;
            parent.SendMessage("AttackMelee", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rootBehaviour._isSkill2TriggerOk = true;
        }
    }
}
