﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRangeTrigger : MonoBehaviour {

    GameObject root;
    MonsterBehaviorManager rootBehavior;

    void Awake()
    {
        root = transform.root.gameObject;
        rootBehavior = root.gameObject.GetComponent<MonsterBehaviorManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (rootBehavior.myMonsterInfo.isMeleeAttackReady == true)
            {
                print("OnTriggerEnter2D() called by " + transform.gameObject.name);
                print("Collided Object is : " + collision.gameObject.transform.name);

                rootBehavior.SendMessage("AttackMeleeFacade", SendMessageOptions.DontRequireReceiver);
                //collision.SendMessage("EnemyHit", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
