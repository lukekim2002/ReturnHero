using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManticoreAttackRange : MonoBehaviour
{
    GameObject parent;

    void Awake()
    {
        parent = transform.parent.gameObject;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (parent.transform.GetComponent<ManticoreClass>()._isSkill3TriggerOk == true)
            {
                parent.transform.GetComponent<ManticoreClass>()._isSkill3TriggerOk = false;
                parent.SendMessage("AttackSkill3", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                parent.SendMessage("AttackMelee", SendMessageOptions.DontRequireReceiver);
            }
                
        }
    }
}
