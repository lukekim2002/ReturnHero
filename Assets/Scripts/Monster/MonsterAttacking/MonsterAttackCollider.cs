using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHit"))
        {
            /* TODO : Send Damage to Player
             * 1. Check my object's animator stateinfo
             * 2. Get Damage by its stateinfo
             * 3. Send
             */
            Debug.Log("Player Hit");
        }
    }
}
