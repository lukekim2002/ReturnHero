using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManticoreAttackRange : MonoBehaviour
{
    GameObject root;

    void Awake()
    {
        root = transform.root.gameObject;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (root.transform.GetComponent<ManticoreClass>()._isSkill3TriggerOk == true)
            {
                root.transform.GetComponent<ManticoreClass>()._isSkill3TriggerOk = false;
                root.SendMessage("AttackSkill3", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                root.SendMessage("AttackMelee", SendMessageOptions.DontRequireReceiver);
            }
                
        }
    }
}
