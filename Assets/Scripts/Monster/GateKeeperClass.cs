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
    private Vector2 _meleeBoxSize;

    private int _skill1Damage;
    private float _Skill1CoolDown;
    private  Vector2 _skill1Range;
    private Vector2 _skill1BoxSize;

    private int _skill2Damage;
    private float _Skill2CoolDown;
    private Vector2 _skill2Range;
    private Vector2 _skill2BoxSize;
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
    
    Vector2 IMonsterPropertySet.MeleeBoxSize
    {
        get { return this._meleeBoxSize; }
        set { this._meleeBoxSize = value; }
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

    Vector2 IMonsterPropertySet.Skill1BoxSize
    {
        get { return this._skill1BoxSize; }
        set { this._skill1BoxSize = value; }
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

    Vector2 IMonsterPropertySet.Skill2BoxSize
    {
        get { return this._skill2BoxSize; }
        set { this._skill2BoxSize = value; }
    }
    #endregion

    #region NOT USED IN THIS CLASS BUT FOR DECLARATION
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

    Vector2 IMonsterPropertySet.Skill3BoxSize
    {
        get { return new Vector2(0, 0); }
        set { }
    }
 
    int IMonsterPropertySet.Skill4Damage
    {
        get { return -1; }
        set { this._skill1Damage = value; }
    }

    float IMonsterPropertySet.Skill4CoolDown
    {
        get { return -1; }
        set { this._Skill1CoolDown = value; }
    }

    Vector2 IMonsterPropertySet.Skill4Range
    {
        get { return new Vector2(0, 0); }
        set { this._skill1Range = value; }
    }

    Vector2 IMonsterPropertySet.Skill4BoxSize
    {
        get { return new Vector2(0, 0); }
        set { this._skill1BoxSize = value; }
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
