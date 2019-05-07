using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSkill2Trigger : MonoBehaviour
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
            if (rootBehaviour._isSkill2TriggerOk == true)
            {
                rootBehaviour._isSkill2TriggerOk = false;

                if (parent.transform.name.Equals("Skill2AttackRange"))
                {
                    parent.transform.parent.SendMessage("AttackSkill2", SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    parent.SendMessage("AttackSkill2", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
