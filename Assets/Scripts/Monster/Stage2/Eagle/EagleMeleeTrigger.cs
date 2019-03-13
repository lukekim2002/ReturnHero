using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMeleeTrigger : MonoBehaviour
{
    GameObject root;
    EagleClass rootBehaviour;

    private void Awake()
    {
        root = transform.root.gameObject;
        rootBehaviour = root.GetComponent<EagleClass>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rootBehaviour._isSkill2TriggerOk = false;
            root.SendMessage("AttackMelee", SendMessageOptions.DontRequireReceiver);
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
