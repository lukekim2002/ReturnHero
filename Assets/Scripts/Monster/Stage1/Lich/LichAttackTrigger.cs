using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttackTrigger : MonoBehaviour
{
    GameObject parent;
    LichClass rootBehaviour;

    private void Awake()
    {
        parent = transform.parent.gameObject;

        if (transform.parent.name.Equals("MeleeAttackRange"))
        {
            rootBehaviour = parent.transform.parent.GetComponent<LichClass>();
        }
        else
        {
            rootBehaviour = parent.GetComponent<LichClass>();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.parent.name.Equals("MeleeAttackRange"))
            {
                parent.transform.parent.SendMessage("AttackMelee", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                parent.SendMessage("AttackMelee", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    
}
