using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichSkillTrigger : MonoBehaviour
{
    GameObject parent;
    LichClass rootBehaviour;

    private void Awake()
    {
        parent = transform.parent.gameObject;

        /*
        if (transform.parent.name.Equals("MeleeAttackRange"))
        {
            rootBehaviour = parent.transform.parent.GetComponent<LichClass>();
        }
        else
        {
            rootBehaviour = parent.GetComponent<LichClass>();
        }
        */
        rootBehaviour = parent.GetComponent<LichClass>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rootBehaviour._isSkill1TriggerOk = false;
            //root.SendMessage("AttackMelee", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rootBehaviour._isSkill1TriggerOk = true;
            //root.SendMessage("AttackSkill1", SendMessageOptions.DontRequireReceiver);
        }
    }
}
