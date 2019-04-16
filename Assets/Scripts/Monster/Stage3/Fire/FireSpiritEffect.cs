using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritEffect : MonoBehaviour
{
    int health;
    int damage;
    float moveSpeed;
    Pathfinding.AIDestinationSetter aiDestinationSetter;
    Animator myAnimator;


    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        aiDestinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        aiDestinationSetter.target = HeroGeneralManager.instance.heroObject.transform;

        health = 1000;
    }

    

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
            myAnimator.SetTrigger("isExplosion");
        }
    }

    public void HitByPlayer(int damage)
    {


        health -= damage;
        Debug.Log("current health : " + health);

        if (health <= 0)
        {
            myAnimator.SetTrigger("isDead");
        }


    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
