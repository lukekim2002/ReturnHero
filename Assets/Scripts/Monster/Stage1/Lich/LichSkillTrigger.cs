using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichSkillTrigger : MonoBehaviour
{
    GameObject root;
    LichClass rootBehaviour;

    private void Awake()
    {
        root = transform.root.gameObject;
        rootBehaviour = root.GetComponent<LichClass>();
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
