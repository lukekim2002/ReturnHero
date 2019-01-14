using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackCollider : MonoBehaviour
{
    GameObject root;
    HeroController rootBehaviour;
    MonsterBehaviorManager collisionBehaviour;

    //bool isAttackingAlready = false;

    void Awake()
    {
        root = transform.root.gameObject;
        rootBehaviour = root.GetComponent<HeroController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hit"))// && rootBehaviour._isAttack == false)
        {
            Debug.Log("Hit");
            //print("OnTriggerEnter2D() called by " + transform.gameObject.name);
            //print("Collided Object is : " + collision.gameObject.transform.name);
            //collisionBehaviour = collision.transform.root.GetComponent<MonsterBehaviorManager>();

            /*
            if (isAttackingAlready == false)
            {
                isAttackingAlready = true;
                Debug.Log(collision.transform.root.gameObject.name);
                
            }
            else
            {
                isAttackingAlready = false;
                return;
            }
            */

            collision.transform.root.gameObject.SendMessage("HitByPlayer", 250, SendMessageOptions.DontRequireReceiver);
        }
    }
}
