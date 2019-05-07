using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowSkillTrigger : MonoBehaviour
{
    GameObject parent;
    CowClass rootBehaviour;

    private void Awake()
    {
        parent = transform.parent.gameObject;
        rootBehaviour = parent.GetComponent<CowClass>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rootBehaviour._isSkill1TriggerOk = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rootBehaviour._isSkill1TriggerOk = true;
        }
    }
}
