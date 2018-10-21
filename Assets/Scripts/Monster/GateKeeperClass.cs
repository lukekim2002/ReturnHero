using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeperClass : MonoBehaviour, IMonsterInterface {

    #region PRIVATE VALUES

    private int _health;
    private float _movingSpeed;

    private bool _isMeleeAttackReady;
    private int _meleeDamage;
    public float _meleeCoolDown;
    private Vector2 _meleeRange;
    private Vector2 _meleeBoxSizeUp;
    private Vector2 _meleeBoxSizedown;
    private Vector2 _meleeBoxSizeLeft;
    private Vector2 _meleeBoxSizeRight;

    private bool _isSkill1AttackReady;
    private int _skill1Damage;
    private float _Skill1CoolDown;
    private  Vector2 _skill1Range;
    private Vector2 _skill1BoxSizeUp;
    private Vector2 _skill1BoxSizeDown;
    private Vector2 _skill1BoxSizeLeft;
    private Vector2 _skill1BoxSizeRight;

    private bool _isSkill2AttackReady;
    private int _skill2Damage;
    private float _Skill2CoolDown;
    private Vector2 _skill2Range;
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

    Vector2 IMonsterInterface.MeleeRange
    {
        get { return this._meleeRange; }
        set { this._meleeRange = value; }
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

    Vector2 IMonsterInterface.Skill1Range
    {
        get { return this._skill1Range; }
        set { this._skill1Range = value; }
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

    Vector2 IMonsterInterface.Skill2Range
    {
        get { return this._skill2Range; }
        set { this._skill2Range = value; }
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

    Vector2 IMonsterInterface.Skill3Range
    {
        get { return new Vector2(0, 0); }
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

    Vector2 IMonsterInterface.Skill4Range
    {
        get { return new Vector2(0, 0); }
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
         * 쿨타임 적용 -> 코루틴
         * 콜라이더 크기 변경
         * 콜라이더에 공격력 보내기 -> 콜라이더는 공격력을 히어로로 보내기
         */

        /*
        if (dir == Vector2.up)
        {
            //anim.Play("");
        }
        else if (dir == Vector2.down)
        {

        }
        else if (dir == Vector2.left)
        {

        }
        else if (dir == Vector2.right)
        {

        }
        else
        {
            return;
        }
        */

        anim.SetInteger("actionNum", 2);
        anim.SetTrigger("isMelee");
        anim.SetFloat("actionX", dir.x);
        anim.SetFloat("actionY", dir.y);

        Debug.Log("\"AttackMelee\" called in GateKeeperClass");
        _isMeleeAttackReady = false;
        StartCoroutine(CoolDownMelee());
    }

    public void AttackSkill1(Vector2 dir)
    {
        
        if (_isSkill1AttackReady == false) return;
        Debug.Log("\"attackSkill1\" called in GateKeeperClass");
        if (_isSkill2AttackReady == true)
        {
            AttackSkill2(dir);
            //return;
        }


        
        _isSkill1AttackReady = false;
        StartCoroutine(CoolDownSkill1());
    }

    public void AttackSkill2(Vector2 dir)
    {
        
        if (_isSkill2AttackReady == false) return;
        Debug.Log("\"AttackSkill2\" called in GateKeeperClass");

        _isSkill2AttackReady = false;
        StartCoroutine(CoolDownSkill2());
    }

    public IEnumerator CoolDownMelee()
    {
        Debug.Log("\"CoolDownMelee\" called in GateKeeperClass");
        yield return new WaitForSeconds(this._meleeCoolDown);
        _isMeleeAttackReady = true;
    }

    public IEnumerator CoolDownSkill1()
    {
        Debug.Log("\"CoolDownSkill1\" called in GateKeeperClass");
        yield return new WaitForSeconds(_Skill1CoolDown);
        _isSkill1AttackReady = true;
    }

    public IEnumerator CoolDownSkill2()
    {
        Debug.Log("\"CoolDownSkill2\" called in GateKeeperClass");
        yield return new WaitForSeconds(_Skill2CoolDown);
        _isSkill2AttackReady = true;
    }

    #region NOT USED
    public void AttackSkill3(Vector2 dir)
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4(Vector2 dir)
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

    public void DyingEvent()
    {
        //AttackSkill2()
    }

    public void HitByPlayer(int damage)
    {
        // BeShot Animation
        _health -= damage;
        if (_health < 0)
        {
            DyingEvent();
        }
        
    }

    public void Initialize()
    {
        // Initialization part
    }

    #endregion
}
