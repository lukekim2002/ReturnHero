using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeperClass : MonoBehaviour, IMonsterPropertySet, IMonsterMethodSet {

    #region PRIVATE VALUES
    private int _health;
    private float _movingSpeed;

    private int _meleeDamage;
    private float _meleeCoolDown;
    private Vector2 _meleeRange;
    private Vector2 _meleeBoxSizeUp;
    private Vector2 _meleeBoxSizedown;
    private Vector2 _meleeBoxSizeLeft;
    private Vector2 _meleeBoxSizeRight;

    private int _skill1Damage;
    private float _Skill1CoolDown;
    private  Vector2 _skill1Range;
    private Vector2 _skill1BoxSizeUp;
    private Vector2 _skill1BoxSizeDown;
    private Vector2 _skill1BoxSizeLeft;
    private Vector2 _skill1BoxSizeRight;

    private int _skill2Damage;
    private float _Skill2CoolDown;
    private Vector2 _skill2Range;
    private Vector2 _skill2BoxSizeUp;
    private Vector2 _skill2BoxSizeDown;
    private Vector2 _skill2BoxSizeLeft;
    private Vector2 _skill2BoxSizeRight;
    #endregion

    #region IMonsterPropertySet Implementation

    int IMonsterPropertySet.Health
    {
        get { return this._health; }
        set { this._health = value; }
    }

    float IMonsterPropertySet.MovingSpeed
    {
        get { return this._movingSpeed; }
        set { this._movingSpeed = value; }
    }

    #region MeleeAttack
    int IMonsterPropertySet.MeleeDamage
    {
        get { return this._meleeDamage; }
        set { this._meleeDamage = value; }
    }

    float IMonsterPropertySet.MeleeCoolDown
    {
        get { return this._meleeCoolDown; }
        set { this._meleeCoolDown = value; }
    }

    Vector2 IMonsterPropertySet.MeleeRange
    {
        get { return this._meleeRange; }
        set { this._meleeRange = value; }
    }
    
    Vector2 IMonsterPropertySet.MeleeBoxSizeUp
    {
        get { return this._meleeBoxSizeUp; }
        set { this._meleeBoxSizeUp = value; }
    }

    Vector2 IMonsterPropertySet.MeleeBoxSizeDown
    {
        get { return this._meleeBoxSizedown; }
        set { this._meleeBoxSizedown = value; }
    }

    Vector2 IMonsterPropertySet.MeleeBoxSizeLeft
    {
        get { return this._meleeBoxSizeLeft; }
        set { this._meleeBoxSizeLeft = value; }
    }

    Vector2 IMonsterPropertySet.MeleeBoxSizeRight
    {
        get { return this._meleeBoxSizeRight; }
        set { this._meleeBoxSizeRight = value; }
    }

    #endregion

    #region Skill1Attack
    int IMonsterPropertySet.Skill1Damage
    {
        get { return this._skill1Damage; }
        set { this._skill1Damage = value; }
    }

    float IMonsterPropertySet.Skill1CoolDown
    {
        get { return this._Skill1CoolDown; }
        set { this._Skill1CoolDown = value; }
    }

    Vector2 IMonsterPropertySet.Skill1Range
    {
        get { return this._skill1Range; }
        set { this._skill1Range = value; }
    }

    Vector2 IMonsterPropertySet.Skill1BoxSizeUp
    {
        get { return this._skill1BoxSizeUp; }
        set { this._skill1BoxSizeUp = value; }
    }

    Vector2 IMonsterPropertySet.Skill1BoxSizeDown
    {
        get { return this._skill1BoxSizeDown; }
        set { this._skill1BoxSizeDown = value; }
    }

    Vector2 IMonsterPropertySet.Skill1BoxSizeLeft
    {
        get { return this._skill1BoxSizeLeft; }
        set { this._skill1BoxSizeLeft = value; }
    }

    Vector2 IMonsterPropertySet.Skill1BoxSizeRight
    {
        get { return this._skill1BoxSizeRight; }
        set { this._skill1BoxSizeRight = value; }
    }
    #endregion

    #region Skill2Attack
    int IMonsterPropertySet.Skill2Damage
    {
        get { return this._skill2Damage; }
        set { this._skill2Damage = value; }
    }

    float IMonsterPropertySet.Skill2CoolDown
    {
        get { return this._Skill2CoolDown; }
        set { this._Skill2CoolDown = value; }
    }

    Vector2 IMonsterPropertySet.Skill2Range
    {
        get { return this._skill2Range; }
        set { this._skill2Range = value; }
    }

    Vector2 IMonsterPropertySet.Skill2BoxSizeUp
    {
        get { return this._skill2BoxSizeUp; }
        set { this._skill2BoxSizeUp = value; }
    }

    Vector2 IMonsterPropertySet.Skill2BoxSizeDown
    {
        get { return this._skill2BoxSizeDown; }
        set { this._skill2BoxSizeDown = value; }
    }

    Vector2 IMonsterPropertySet.Skill2BoxSizeLeft
    {
        get { return this._skill2BoxSizeLeft; }
        set { this._skill2BoxSizeLeft = value; }
    }

    Vector2 IMonsterPropertySet.Skill2BoxSizeRight
    {
        get { return this._skill2BoxSizeRight; }
        set { this._skill2BoxSizeRight = value; }
    }
    #endregion

    #region NOT USED
    int IMonsterPropertySet.Skill3Damage
    {
        get { return -1; }
        set { }
    }

    float IMonsterPropertySet.Skill3CoolDown
    {
        get { return -1; }
        set {  }
    }

    Vector2 IMonsterPropertySet.Skill3Range
    {
        get { return new Vector2(0, 0); }
        set {  }
    }

    Vector2 IMonsterPropertySet.Skill3BoxSizeUp
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterPropertySet.Skill3BoxSizeDown
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterPropertySet.Skill3BoxSizeLeft
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterPropertySet.Skill3BoxSizeRight
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    int IMonsterPropertySet.Skill4Damage
    {
        get { return -1; }
        set { }
    }

    float IMonsterPropertySet.Skill4CoolDown
    {
        get { return -1; }
        set { }
    }

    Vector2 IMonsterPropertySet.Skill4Range
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterPropertySet.Skill4BoxSizeUp
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterPropertySet.Skill4BoxSizeDown
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterPropertySet.Skill4BoxSizeLeft
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterPropertySet.Skill4BoxSizeRight
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    #endregion

    #endregion

    #region IMonsterethodSet Implementation

    public void AttackMelee(Vector2 dir)
    {
        print("\"AttackMelee\" called in GateKeeperClass");
    }

    public void AttackSkill1(Vector2 dir)
    {
        print("\"attackSkill1\" called in GateKeeperClass");
    }

    public void AttackSkill2(Vector2 dir)
    {
        print("\"AttackSkill2\" called in GateKeeperClass");
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
    #endregion

    public void DyingEvent()
    {
        //AttackSkill2()
    }

    public void HitByPlayer(int damage)
    {
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
