using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRangeTrigger : MonoBehaviour {

    GameObject root;
    MonsterBehaviorManager rootBehavior;

    void Awake()
    {
        root = transform.root.gameObject;
        //rootBehavior = root.gameObject.GetComponent<MonsterBehaviorManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (this.gameObject.name.Equals("MeleeAttackRange"))
            {

                root.SendMessage("AttackMelee", SendMessageOptions.DontRequireReceiver);

            }

            if (this.gameObject.name.Equals("Skill1AttackRange"))
            {

                rootBehavior.SendMessage("AttackSkill1", SendMessageOptions.DontRequireReceiver);
                
                
            }

            if (this.gameObject.name.Equals("Skill2AttackRange"))
            {

                    rootBehavior.SendMessage("AttackSkill2", SendMessageOptions.DontRequireReceiver);
                
            }
        }
    }
}
