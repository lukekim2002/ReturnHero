using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTrigger : MonoBehaviour
{
    GameObject parent;

    private void Awake()
    {
        parent = transform.parent.gameObject;
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
