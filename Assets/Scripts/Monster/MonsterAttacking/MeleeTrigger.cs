﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTrigger : MonoBehaviour
{
    GameObject root;

    private void Awake()
    {
        root = transform.root.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            root.SendMessage("AttackMelee", SendMessageOptions.DontRequireReceiver);
        }
    }
}
