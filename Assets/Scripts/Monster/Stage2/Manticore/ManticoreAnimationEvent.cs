using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManticoreAnimationEvent : MonoBehaviour
{
    private Vector2 _pos;
    private Vector2 _dir;
    private Vector2 _wallPoint;
    ManticoreClass _behaviour;

    public GameObject myAttackEffect;
    public GameObject mySkillEffect_3Way;
    public GameObject mySkillEffect_4Way;

    public void AttackMelee_Ready()
    {
        _behaviour = GetComponent<ManticoreClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.16f;
        this.transform.position = _pos;
    }

    public void AttackMelee_Execute()
    {
        _dir = _behaviour.myDirection;
        Animator anim = myAttackEffect.GetComponent<Animator>();

        if (_dir == Vector2.up)
            anim.SetTrigger("isUp");
        else if (_dir == Vector2.down)
            anim.SetTrigger("isDown");
        else if (_dir == Vector2.left)
            anim.SetTrigger("isLeft");
        else if (_dir == Vector2.right)
            anim.SetTrigger("isRight");

    }

    public void AttackMelee_End()
    {
        _dir = _behaviour.myDirection;
        Animator anim = myAttackEffect.GetComponent<Animator>();

        if (_dir == Vector2.up)
            anim.ResetTrigger("isUp");
        else if (_dir == Vector2.down)
            anim.ResetTrigger("isDown");
        else if (_dir == Vector2.left)
            anim.ResetTrigger("isLeft");
        else if (_dir == Vector2.right)
            anim.ResetTrigger("isRight");
    }

    public void AttackSkill1_Execute()
    {
        _behaviour = GetComponent<ManticoreClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.16f;
        this.transform.position = _pos;
    }

    public void AttackSkill2_Execute()
    {
        _behaviour = GetComponent<ManticoreClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.16f;
        this.transform.position = _pos;
    }

    public void AttackSkill3_3way_Ready()
    {
        _behaviour = GetComponent<ManticoreClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.16f;
        this.transform.position = _pos;
    }

    public void AttackSkill3_3way_Execute()
    {
        _dir = _behaviour.myBase.direction;
        Animator myAnimator = _behaviour.GetComponent<Animator>();
        Animator anim = mySkillEffect_3Way.GetComponent<Animator>();

        myAnimator.SetFloat("actionX", _dir.x);
        myAnimator.SetFloat("actionY", _dir.y);

        if (_dir == Vector2.up)
            anim.SetTrigger("isUp");
        else if (_dir == Vector2.down)
            anim.SetTrigger("isDown");
        else if (_dir == Vector2.left)
            anim.SetTrigger("isLeft");
        else if (_dir == Vector2.right)
            anim.SetTrigger("isRight");
    }

    public void AttackSkill3_3way_End()
    {
        //_dir = _behaviour.myDirection;
        Animator anim = mySkillEffect_3Way.GetComponent<Animator>();

        //if (_dir == Vector2.up)
            anim.ResetTrigger("isUp");
        //else if (_dir == Vector2.down)
            anim.ResetTrigger("isDown");
        //else if (_dir == Vector2.left)
            anim.ResetTrigger("isLeft");
        //else if (_dir == Vector2.right)
            anim.ResetTrigger("isRight");
    }

    public void AttackSkill3_4way_Ready()
    {
        _behaviour = GetComponent<ManticoreClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.16f;
        this.transform.position = _pos;
    }

    public void AttackSkill3_4way_Execute()
    {
        _dir = _behaviour.myBase.direction;
        Animator myAnimator = _behaviour.GetComponent<Animator>();
        Animator anim = mySkillEffect_4Way.GetComponent<Animator>();

        myAnimator.SetFloat("actionX", _dir.x);
        myAnimator.SetFloat("actionY", _dir.y);

        if (_dir == Vector2.up)
            anim.SetTrigger("isUp");
        else if (_dir == Vector2.down)
            anim.SetTrigger("isDown");
        else if (_dir == Vector2.left)
            anim.SetTrigger("isLeft");
        else if (_dir == Vector2.right)
            anim.SetTrigger("isRight");
    }

    public void AttackSkill3_4way_End()
    {
        //_dir = _behaviour.myDirection;
        Animator anim = mySkillEffect_4Way.GetComponent<Animator>();

        //if (_dir == Vector2.up)
            anim.ResetTrigger("isUp");
        //else if (_dir == Vector2.down)
            anim.ResetTrigger("isDown");
        //else if (_dir == Vector2.left)
            anim.ResetTrigger("isLeft");
        //else if (_dir == Vector2.right)
            anim.ResetTrigger("isRight");
    }

}
