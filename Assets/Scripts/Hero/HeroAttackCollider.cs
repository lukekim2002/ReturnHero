using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackCollider : MonoBehaviour
{
    GameObject root;
    HeroController rootBehaviour;
    MonsterBehaviorManager collisionBehaviour;
    Animator rootAnimator;

    //bool isAttackingAlready = false;

    void Awake()
    {
        root = transform.root.gameObject;
        rootBehaviour = root.GetComponent<HeroController>();
        rootAnimator = root.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hit"))// && rootBehaviour._isAttack == false)
        {
            Debug.Log("Hit");

            if (rootAnimator.GetBool("isMelee") == true)
            {
                // Melee Attack
            }

            if (rootAnimator.GetBool("isSkillMr") == true)
            {
                // MR Attack
            }

            if (rootAnimator.GetBool("isSkillE") == true)
            {
                // E Attack
            }

            if (rootAnimator.GetBool("isSkillR") == true)
            {
                // R Attack
            }

            collision.transform.parent.gameObject.SendMessage("HitByPlayer", 250, SendMessageOptions.DontRequireReceiver);
        }
    }
}
