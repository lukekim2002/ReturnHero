﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManticoreClass : MonsterBase
{
    #region PRIVATE VALUES

    private int _id;
    private int _health;
    private float _movingSpeed;

    private bool _isMeleeAttackReady;
    private int _meleeDamage;
    private float _meleeCoolDown;

    private bool _isSkill1AttackReady;
    private int _skill1Damage;
    private float _Skill1CoolDown;

    private bool _isSkill2AttackReady;
    private int _skill2Damage;
    private float _Skill2CoolDown;

    private bool _isSkill3AttackReady;
    public bool _isSkill3TriggerOk;        // for checking hero out of attack range
    private int _skill3Damage;
    private float _Skill3CoolDown;


    private Vector2 attackColliderSize;
    private Vector2 attackColliderOffset;

    #endregion

    #region PUBLIC VALUES

    public Dictionary<string, object> myDataSet;
    public List<Dictionary<string, object>> myColliderSet;

    [Header("Refered Objects")]
    public MonsterBase myBase;
    public Pathfinding.AIPath aiMoveScript;
    public Animator myAnimator;

    public GameObject attackCollider;
    public BoxCollider2D attackColliderScript;
    public GameObject myMeleeAttackRange;
    public GameObject mySkill1AttackRange;
    public GameObject mySkill2AttackRange;


    [Header("State Values")]
    public Vector2 myDirection;
    public Action myAction;
    public bool isAttacking = false;

    #endregion

    #region MonoBehaviour CallBacks

    private void OnEnable()
    {
        myAction = Action.Idle;

        Initialize();

        aiMoveScript.enabled = false;

        StartCoroutine(myBase.FindLookingDirection());

        PlayerEnteredRoom();
    }

    private void Update()
    {
        if (isAttacking == false)
        {
            myDirection = myBase.direction;
            myAction = Action.Move;
        }
            
        if (myAction == Action.Idle && isAttacking == false)
        {
            myAnimator.SetInteger("actionNum", 0);
            myAnimator.SetFloat("actionX", myDirection.x);
            myAnimator.SetFloat("actionY", myDirection.y);
        }
        else if (myAction == Action.Move && isAttacking == false)
        {
            myAnimator.SetInteger("actionNum", 1);
            myAnimator.SetFloat("moveX", myDirection.x);
            myAnimator.SetFloat("moveY", myDirection.y);
        }
    }

    #endregion

    #region MonsterBase Implementation

    public override void Initialize()
    {
        aiMoveScript = GetComponent<Pathfinding.AIPath>();
        playerObject = HeroGeneralManager.instance.heroObject;
        attackCollider = transform.GetChild(1).gameObject;
        attackColliderScript = attackCollider.GetComponent<BoxCollider2D>();
        myMeleeAttackRange = transform.GetChild(3).gameObject;
        mySkill1AttackRange = transform.GetChild(4).gameObject;
        mySkill2AttackRange = transform.GetChild(5).gameObject;
        myAnimator = GetComponent<Animator>();

        myBase = GetComponent<MonsterBase>();
        if (myBase == null) Debug.LogError("myBase is null.");

        myDataSet = MonsterDataManager.instance.ThrowDataIntoContainer((int)MonsterDataManager.MONSTER.MANTICORE);
        myColliderSet = CSVReader.Read("CSV/Monster/Stage2/ReturnHero_manticore_AttackCollider");


        _id = (int)myDataSet["ID"];
        _health = (int)myDataSet["Health"];
        _movingSpeed = (float)myDataSet["MovingSpeed"];
        aiMoveScript.maxSpeed = _movingSpeed;

        _meleeDamage = (int)myDataSet["MeleeDamage"];
        _meleeCoolDown = (int)myDataSet["MeleeCoolDown"];

        _skill1Damage = (int)myDataSet["Skill1Damage"];
        _Skill1CoolDown = (int)myDataSet["Skill1CoolDown"];

        _skill2Damage = (int)myDataSet["Skill2Damage"];
        _Skill2CoolDown = (int)myDataSet["Skill2CoolDown"];

        _skill3Damage = (int)myDataSet["Skill3Damage"];
        _Skill3CoolDown = (int)myDataSet["Skill3CoolDown"];


        _isMeleeAttackReady = true;
        _isSkill1AttackReady = true;
        _isSkill2AttackReady = true;
        _isSkill3AttackReady = true;
    }

    #region ATTACK

    #region Melee
    public override void AttackMelee()
    {
        throw new System.NotImplementedException();
    }

    public override void EndAttackMelee()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator CoolDownMelee()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Skill1
    public override void AttackSkill1()
    {
        throw new System.NotImplementedException();
    }

    public override void EndAttackSkill1()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator CoolDownSkill1()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Skill2
    public override void AttackSkill2()
    {
        throw new System.NotImplementedException();
    }

    public override void EndAttackSkill2()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator CoolDownSkill2()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Skill3
    public override void AttackSkill3()
    {
        throw new System.NotImplementedException();
    }

    public override void EndAttackSkill3()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator CoolDownSkill3()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    public override void GetHealed(GameGeneralManager.HealInfo myHeal)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region HIT
    public override void HitByPlayer(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override void EndGetHit()
    {
        throw new System.NotImplementedException();
    }

    public override void DyingMotion()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Animator Operation

    public override bool CheckAnimatorStateName(AnimatorStateInfo stateInfo)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator WaitAnimationFinish()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #endregion

    #region NOT USED

    public override void AttackSkill4()
    {
        throw new System.NotImplementedException();
    }

    public override void EndAttackSkill4()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator CoolDownSkill4()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    private void PlayerEnteredRoom()
    {
        myAction = Action.Move;
        aiMoveScript.enabled = true;
    }
}
