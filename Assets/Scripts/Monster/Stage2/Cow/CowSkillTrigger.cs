using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowSkillTrigger : MonoBehaviour
{
    GameObject root;
    CowClass rootBehaviour;

    private void Awake()
    {
        root = transform.root.gameObject;
        rootBehaviour = root.GetComponent<CowClass>();
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
