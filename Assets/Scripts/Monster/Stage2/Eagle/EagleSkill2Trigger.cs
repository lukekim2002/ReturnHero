using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSkill2Trigger : MonoBehaviour
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
            if (rootBehaviour._isSkill2TriggerOk == true)
            {
                rootBehaviour._isSkill2TriggerOk = false;
                root.SendMessage("AttackSkill2", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
