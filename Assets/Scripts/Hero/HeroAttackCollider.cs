using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackCollider : MonoBehaviour
{
    GameObject root;

    void Awake()
    {
        root = transform.root.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            print("OnTriggerEnter2D() called by " + transform.gameObject.name);
            print("Collided Object is : " + collision.gameObject.transform.name);

            collision.SendMessage("EnemyHit", SendMessageOptions.DontRequireReceiver);
        }
    }

}
