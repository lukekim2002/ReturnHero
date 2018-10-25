﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeperClass : MonoBehaviour, IMonsterInterface {

    #region PRIVATE VALUES

    private int _health;
    private float _movingSpeed;

    private bool _isMeleeAttackReady;
    private int _meleeDamage;
    public float _meleeCoolDown;
    private Vector2 _meleeBoxSizeUp;
    private Vector2 _meleeBoxSizedown;
    private Vector2 _meleeBoxSizeLeft;
    private Vector2 _meleeBoxSizeRight;

    private bool _isSkill1AttackReady;
    private int _skill1Damage;
    private float _Skill1CoolDown;
    private Vector2 _skill1BoxSizeUp;
    private Vector2 _skill1BoxSizeDown;
    private Vector2 _skill1BoxSizeLeft;
    private Vector2 _skill1BoxSizeRight;

    private bool _isSkill2AttackReady;
    private int _skill2Damage;
    private float _Skill2CoolDown;
    private Vector2 _skill2BoxSizeUp;
    private Vector2 _skill2BoxSizeDown;
    private Vector2 _skill2BoxSizeLeft;
    private Vector2 _skill2BoxSizeRight;

    #endregion

    #region IMonsterPropertySet Implementation

    int IMonsterInterface.Health
    {
        get { return this._health; }
        set { this._health = value; }
    }

    float IMonsterInterface.MovingSpeed
    {
        get { return this._movingSpeed; }
        set { this._movingSpeed = value; }
    }

    #region MeleeAttack

    bool IMonsterInterface.isMeleeAttackReady
    {
        get { return this._isMeleeAttackReady; }
        set { this._isMeleeAttackReady = value; }
    }

    int IMonsterInterface.MeleeDamage
    {
        get { return this._meleeDamage; }
        set { this._meleeDamage = value; }
    }

    float IMonsterInterface.MeleeCoolDown
    {
        get { return this._meleeCoolDown; }
        set { this._meleeCoolDown = value; }
    }
    
    Vector2 IMonsterInterface.MeleeBoxSizeUp
    {
        get { return this._meleeBoxSizeUp; }
        set { this._meleeBoxSizeUp = value; }
    }

    Vector2 IMonsterInterface.MeleeBoxSizeDown
    {
        get { return this._meleeBoxSizedown; }
        set { this._meleeBoxSizedown = value; }
    }

    Vector2 IMonsterInterface.MeleeBoxSizeLeft
    {
        get { return this._meleeBoxSizeLeft; }
        set { this._meleeBoxSizeLeft = value; }
    }

    Vector2 IMonsterInterface.MeleeBoxSizeRight
    {
        get { return this._meleeBoxSizeRight; }
        set { this._meleeBoxSizeRight = value; }
    }

    #endregion

    #region Skill1Attack

    bool IMonsterInterface.isSkill1AttackReady
    {
        get { return this._isSkill1AttackReady; }
        set { this._isSkill1AttackReady = value; }
    }

    int IMonsterInterface.Skill1Damage
    {
        get { return this._skill1Damage; }
        set { this._skill1Damage = value; }
    }

    float IMonsterInterface.Skill1CoolDown
    {
        get { return this._Skill1CoolDown; }
        set { this._Skill1CoolDown = value; }
    }

    Vector2 IMonsterInterface.Skill1BoxSizeUp
    {
        get { return this._skill1BoxSizeUp; }
        set { this._skill1BoxSizeUp = value; }
    }

    Vector2 IMonsterInterface.Skill1BoxSizeDown
    {
        get { return this._skill1BoxSizeDown; }
        set { this._skill1BoxSizeDown = value; }
    }

    Vector2 IMonsterInterface.Skill1BoxSizeLeft
    {
        get { return this._skill1BoxSizeLeft; }
        set { this._skill1BoxSizeLeft = value; }
    }

    Vector2 IMonsterInterface.Skill1BoxSizeRight
    {
        get { return this._skill1BoxSizeRight; }
        set { this._skill1BoxSizeRight = value; }
    }
    #endregion

    #region Skill2Attack

    bool IMonsterInterface.isSkill2AttackReady
    {
        get { return this._isSkill2AttackReady; }
        set { this._isSkill2AttackReady = value; }
    }

    int IMonsterInterface.Skill2Damage
    {
        get { return this._skill2Damage; }
        set { this._skill2Damage = value; }
    }

    float IMonsterInterface.Skill2CoolDown
    {
        get { return this._Skill2CoolDown; }
        set { this._Skill2CoolDown = value; }
    }

    Vector2 IMonsterInterface.Skill2BoxSizeUp
    {
        get { return this._skill2BoxSizeUp; }
        set { this._skill2BoxSizeUp = value; }
    }

    Vector2 IMonsterInterface.Skill2BoxSizeDown
    {
        get { return this._skill2BoxSizeDown; }
        set { this._skill2BoxSizeDown = value; }
    }

    Vector2 IMonsterInterface.Skill2BoxSizeLeft
    {
        get { return this._skill2BoxSizeLeft; }
        set { this._skill2BoxSizeLeft = value; }
    }

    Vector2 IMonsterInterface.Skill2BoxSizeRight
    {
        get { return this._skill2BoxSizeRight; }
        set { this._skill2BoxSizeRight = value; }
    }
    #endregion

    #region NOT USED

    bool IMonsterInterface.isSkill3AttackReady
    {
        get { return false; }
        set {  }
    }

    int IMonsterInterface.Skill3Damage
    {
        get { return -1; }
        set { }
    }

    float IMonsterInterface.Skill3CoolDown
    {
        get { return -1; }
        set {  }
    }

    Vector2 IMonsterInterface.Skill3BoxSizeUp
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill3BoxSizeDown
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill3BoxSizeLeft
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill3BoxSizeRight
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    bool IMonsterInterface.isSkill4AttackReady
    {
        get { return false; }
        set { }
    }

    int IMonsterInterface.Skill4Damage
    {
        get { return -1; }
        set { }
    }

    float IMonsterInterface.Skill4CoolDown
    {
        get { return -1; }
        set { }
    }

    Vector2 IMonsterInterface.Skill4BoxSizeUp
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill4BoxSizeDown
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill4BoxSizeLeft
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill4BoxSizeRight
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    #endregion

    #endregion

    #region IMonsterethodSet Implementation

    public void AttackMelee(Vector2 dir, Animator anim)
    {
        
        if (_isMeleeAttackReady == false) return;
        _isMeleeAttackReady = false;
        //Debug.Log("\"AttackMelee\" called in GateKeeperClass");

        #region TODO LIST
        /*
        if (_isSkill1AttackReady == true)
        {
            AttackSkill1(dir);
            //return;
        }
        if (_isSkill2AttackReady == true)
        {
            AttackSkill2(dir);
            //return;
        }
        */

        /*
         * 들어가야할 내용:
         * 쿨타임 적용 -> 코루틴 -> clear
         * 콜라이더 크기 변경
         * 콜라이더에 공격력 보내기 -> 콜라이더는 공격력을 히어로로 보내기
         */
        #endregion

        //Debug.Log(anim);
        anim.SetInteger("actionNum", 2);
        anim.SetTrigger("isMelee");
        anim.SetFloat("actionX", dir.x);
        anim.SetFloat("actionY", dir.y);

        
        StartCoroutine(WaitAnimationFinish(anim));
        StartCoroutine(CoolDownMelee());
    }

    public void AttackSkill1(Vector2 dir, Animator anim)
    {
        
        if (_isSkill1AttackReady == false) return;
        _isSkill1AttackReady = false;

        //Debug.Log("\"attackSkill1\" called in GateKeeperClass");
        /*
        if (_isSkill2AttackReady == true)
        {
            AttackSkill2(dir, anim);
            //return;
        }
        */

        anim.SetInteger("actionNum", 2);
        anim.SetTrigger("isSkill1");
        anim.SetFloat("actionX", dir.x);
        anim.SetFloat("actionY", dir.y);

        StartCoroutine(WaitAnimationFinish(anim));
        StartCoroutine(CoolDownSkill1());
    }

    public void AttackSkill2(Vector2 dir, Animator anim)
    {
        
        if (_isSkill2AttackReady == false) return;
        _isSkill2AttackReady = false;
        Debug.Log("\"AttackSkill2\" called in GateKeeperClass");

        anim.SetInteger("actionNum", 2);
        anim.SetTrigger("isSkill2");
        anim.SetFloat("actionX", dir.x);
        anim.SetFloat("actionY", dir.y);

        StartCoroutine(WaitAnimationFinish(anim));

        StartCoroutine(CoolDownSkill2());
    }

    public IEnumerator CoolDownMelee()
    {
        //Debug.Log("\"CoolDownMelee\" called in GateKeeperClass");
        yield return new WaitForSeconds(this._meleeCoolDown);
        _isMeleeAttackReady = true;
    }

    public IEnumerator CoolDownSkill1()
    {
        //Debug.Log("\"CoolDownSkill1\" called in GateKeeperClass");
        yield return new WaitForSeconds(this._Skill1CoolDown);
        _isSkill1AttackReady = true;
    }

    public IEnumerator CoolDownSkill2()
    {
        Debug.Log("\"CoolDownSkill2\" called in GateKeeperClass");
        yield return new WaitForSeconds(this._Skill2CoolDown);
        _isSkill2AttackReady = true;
    }

    private bool CheckAnimatorStateName(AnimatorStateInfo stateInfo)
    {
        return (stateInfo.IsName("Melee")
            || stateInfo.IsName("Skill1")
            || stateInfo.IsName("Skill2")
            || stateInfo.IsName("BeShot"));
    }

    public IEnumerator WaitAnimationFinish(Animator anim)
    {

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // Wait Attack State
        while (!CheckAnimatorStateName(stateInfo))
        {
            stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        
        // Wait Animation Ends
        while (stateInfo.normalizedTime <= 0.95f)
        {
            stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            //print(stateInfo.normalizedTime);
            yield return null;
        }

        if(stateInfo.IsName("Melee"))
            GetComponent<MonsterBehaviorManager>().EndAttackMelee();
        else if(stateInfo.IsName("Skill1"))
            GetComponent<MonsterBehaviorManager>().EndAttackSkill1();
        else if(stateInfo.IsName("Skill2"))
            GetComponent<MonsterBehaviorManager>().EndAttackSkill2();
        else if(stateInfo.IsName("BeShot"))
            GetComponent<MonsterBehaviorManager>().EndGetHit();

        //throw new System.NotImplementedException();
    }

    #region NOT USED
    public void AttackSkill3(Vector2 dir, Animator anim)
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4(Vector2 dir, Animator anim)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator CoolDownSkill3()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator CoolDownSkill4()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    public void DyingEvent(Vector2 dir, Animator anim)
    {
        AttackSkill2(dir, anim);

        anim.SetInteger("actionNum", 3);
        anim.SetFloat("actionX", dir.x);
        anim.SetFloat("actionY", dir.y);

        StartCoroutine(WaitAnimationFinish(anim));

        this.gameObject.SetActive(false);
    }

    public void HitByPlayer(Vector2 dir, Animator anim,int damage)
    {
        anim.SetInteger("actionNum", 3);
        anim.SetFloat("actionX", dir.x);
        anim.SetFloat("actionY", dir.y);

        StartCoroutine(WaitAnimationFinish(anim));

        // BeShot Animation
        _health -= damage;
        if (_health < 0)
        {
            DyingEvent(dir, anim);
        }
        
    }

    public void Initialize()
    {
        // Initialization part
    }

    #endregion
}
