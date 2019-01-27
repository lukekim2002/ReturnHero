using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHit"))
        {
            Debug.Log("Player Hit");
        }
    }
}
