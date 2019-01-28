using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationEvent : MonoBehaviour, IMonsterAnimationEvent {

    public GameObject skeletonProjectile;

    private Vector2 _pos;
    private Vector2 _dir;
    private Vector2 _wallPoint;
    SkeletonClass _behaviour;


    public void AttackMelee_End()
    {
        //this.GetComponent<Animator>().SetInteger("actionNum", 0);
    }

    public void AttackMelee_Execute()
    {
        // 스켈레톤 투사체 SetActive
        skeletonProjectile.SetActive(true);
    }

    public void AttackMelee_Ready()
    {
        _behaviour = GetComponent<SkeletonClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.1f;
        this.transform.position = _pos;
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
}
