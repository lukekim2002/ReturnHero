using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonProjectile : MonoBehaviour, IMonsterAnimationEvent {



    Vector2 myPos;
    public Vector2 myDir;

    float moveSpeed = 0.05f;

    private BoxCollider2D myCollider;

    public Vector2 myColliderSize;
    public Vector2 myColliderOffset;


    public void AttackMelee_End()
    {
        Destroy(gameObject);
    }

    #region Not Used

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

    #endregion


    private void Start()
    {
        
        myCollider = GetComponent<BoxCollider2D>();
        myCollider.size = myColliderSize;
        myCollider.offset = myColliderOffset;
      
    }

    private void Update()
    {
        transform.Translate(myDir * moveSpeed);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // TODO: Damage part
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

}
