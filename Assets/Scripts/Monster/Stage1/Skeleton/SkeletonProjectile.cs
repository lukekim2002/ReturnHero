using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonProjectile : MonoBehaviour, IMonsterAnimationEvent {

    MonsterBehaviorManager rootBehaviour;

    Vector2 myPos;
    Vector2 myDir;

    float moveSpeed = 0.1f;
    float moveDelayTime = (1 / 60);
    int delayFrame = 2;

    private Transform skeletonObject;
    private int skeletonObjectMovePx = 0;
    private Vector2 tempSkeletonProjectileObjectPos;

    public void AttackMelee_End()
    {
        this.transform.parent = skeletonObject;
        this.gameObject.SetActive(false);
    }

    public void AttackMelee_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackMelee_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill2_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill2_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill2_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill3_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill3_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill3_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4_Ready()
    {
        throw new System.NotImplementedException();
    }

    private void OnEnable()
    {
        rootBehaviour = transform.root.GetComponent<MonsterBehaviorManager>();
        //myPos = transform.position;
        //myDir = rootBehaviour.direction;

        myDir = Vector2.down;

        skeletonObject = this.transform.parent;
        // 이펙트를 스켈레톤 좌표랑 똑같이 한다.
        this.transform.position = skeletonObject.position;
        // 이펙트의 부모를 이펙트의 부모오브젝트의 부모오브젝트로 바꾼다. (Skeleton에서 해제한다.)
        this.transform.parent = this.transform.parent.parent;

        if (myDir == Vector2.up)
        {
            this.GetComponent<Animator>().SetTrigger("isSkeletonAttackUp");
        }
        else if (myDir == Vector2.left)
        {
            this.GetComponent<Animator>().SetTrigger("isSkeletonAttackLeft");
        }
        else if (myDir == Vector2.right)
        {
            this.GetComponent<Animator>().SetTrigger("isSkeletonAttackRight");
        }
        else if (myDir == Vector2.down)
        {
            this.GetComponent<Animator>().SetTrigger("isSkeletonAttackDown");
        }

        StartCoroutine("MoveSkeletonProjectile");
    }

    /*
    IEnumerator WaitDelay()
    {
        
        yield return new WaitForSeconds(delayFrame * moveDelayTime);
    }
    */

    IEnumerator MoveSkeletonProjectile()
    {
        while (skeletonObjectMovePx < 18)
        {
            Debug.Log(skeletonObjectMovePx);
            tempSkeletonProjectileObjectPos = this.transform.position;
            tempSkeletonProjectileObjectPos += myDir * 0.1f;

            this.transform.position = tempSkeletonProjectileObjectPos;
            skeletonObjectMovePx++;

            yield return new WaitForSeconds(1 / 30);
        }
    }
}
