using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeperClass : MonsterBase {

    #region PRIVATE VALUES

    private int _id;
    private int _health;
    private float _movingSpeed;

    private bool _isMeleeAttackReady;
    private int _meleeDamage;
    public float _meleeCoolDown;

    private bool _isSkill1AttackReady;
    private int _skill1Damage;
    private float _Skill1CoolDown;

    private bool _isSkill2AttackReady;
    private int _skill2Damage;
    private float _Skill2CoolDown;

    private Vector2 attackColliderSize;
    private Vector2 attackColliderOffset;

    #endregion

    #region PUBLIC VALUES

    public Dictionary<string, object> myDataSet;
    public List<Dictionary<string, object>> myColliderSet;

    public MonsterBase myBase;
    public Pathfinding.AIPath aiMoveScript;
    public Animator myAnimator;

    public GameObject attackCollider;
    public BoxCollider2D attackColliderScript;
    public GameObject myMeleeAttackRange;
    public GameObject mySkill1AttackRange;
    public GameObject mySkill2AttackRange;



    public Vector2 myDirection;
    public Action myAction;
    public AttackCase myAttackCase;
    public bool isAttacking = false;

    #endregion

    #region MONOBEHAVIOUR CALLBACKS

    private void OnEnable()
    {
        myAction = Action.Idle;
        myAttackCase = AttackCase.None;

        

        Initialize();

        aiMoveScript.enabled = false;

        StartCoroutine(myBase.FindLookingDirection());

        PlayerEnteredRoom();
    }

    

    private void Update()
    {
        if(isAttacking == false)
            myDirection = myBase.direction;

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

    #region MONSTERBASE IMPLEMENTATION

    public override void Initialize()
    {
        aiMoveScript = GetComponent<Pathfinding.AIPath>();
        playerObject = HeroGeneralManager.instance.heroObject;
        attackCollider = transform.GetChild(0).gameObject;
        attackColliderScript = attackCollider.GetComponent<BoxCollider2D>();
        myMeleeAttackRange = transform.GetChild(3).gameObject;
        mySkill1AttackRange = transform.GetChild(4).gameObject;
        mySkill2AttackRange = transform.GetChild(5).gameObject;
        myAnimator = GetComponent<Animator>();

        myBase = GetComponent<MonsterBase>();
        if (myBase == null) Debug.LogError("myBase is null.");

        myDataSet = MonsterDataManager.instance.ThrowDataIntoContainer((int)MonsterDataManager.MONSTER.GATEKEEPER);
        myColliderSet = CSVReader.Read("CSV/Monster/Stage0/ReturnHero_GateKeeper_AttackCollider");


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


        _isMeleeAttackReady = true;
        _isSkill1AttackReady = true;
        _isSkill2AttackReady = true;
    }

    public override void AttackMelee()
    {
        if (_isMeleeAttackReady == false && isAttacking == true) return;
        _isMeleeAttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        myAttackCase = AttackCase.Melee;
        aiMoveScript.enabled = false;
        myMeleeAttackRange.SetActive(false);

        /* TODO :
         * AttackColliderSize & AttaackColliderOffset
         */

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isMelee");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
        StartCoroutine(CoolDownMelee());

    }

    public override void AttackSkill1()
    {
        if (_isSkill1AttackReady == false && isAttacking == true) return;
        _isSkill1AttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        myAttackCase = AttackCase.Skill1;
        aiMoveScript.enabled = false;
        mySkill1AttackRange.SetActive(false);

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isSkill1");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
        StartCoroutine(CoolDownSkill1());
    }

    public override void AttackSkill2()
    {
        if (_isSkill2AttackReady == false && isAttacking == true) return;
        _isSkill2AttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        myAttackCase = AttackCase.Skill2;
        aiMoveScript.enabled = false;
        mySkill2AttackRange.SetActive(false);

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isSkill2");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
        StartCoroutine(CoolDownSkill2());
    }

    


    public override void EndAttackMelee()
    {
        myAction = Action.Move;
        myAttackCase = AttackCase.None;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.ResetTrigger("isMelee");
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;

    }

    public override void EndAttackSkill1()
    {
        myAction = Action.Move;
        myAttackCase = AttackCase.None;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.ResetTrigger("isSkill1");
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;
    }

    public override void EndAttackSkill2()
    {
        myAction = Action.Move;
        myAttackCase = AttackCase.None;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.ResetTrigger("isSkill2");
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;
    }

    

    public override IEnumerator CoolDownMelee()
    {
        yield return new WaitForSeconds(_meleeCoolDown);
        _isMeleeAttackReady = true;
        myMeleeAttackRange.SetActive(true);
    }

    public override IEnumerator CoolDownSkill1()
    {
        yield return new WaitForSeconds(_Skill1CoolDown);
        _isSkill1AttackReady = true;
        mySkill1AttackRange.SetActive(true);
    }

    public override IEnumerator CoolDownSkill2()
    {
        yield return new WaitForSeconds(_Skill2CoolDown);
        _isSkill2AttackReady = true;
        mySkill2AttackRange.SetActive(true);
    }

    #region NOT USED

    public override void AttackSkill3()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackSkill4()
    {
        throw new System.NotImplementedException();
    }

    public override void EndAttackSkill3()
    {
        throw new System.NotImplementedException();
    }

    public override void EndAttackSkill4()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator CoolDownSkill3()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator CoolDownSkill4()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    public override bool CheckAnimatorStateName(AnimatorStateInfo stateInfo)
    {
        return (stateInfo.IsName("Melee")
            || stateInfo.IsName("Skill1")
            || stateInfo.IsName("Skill2")
            || stateInfo.IsName("BeShot"));
    }

    public override IEnumerator WaitAnimationFinish()
    {
        AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);

        // Wait Attack State
        while (!CheckAnimatorStateName(stateInfo))
        {
            stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        // Wait Animation Ends
        while (stateInfo.normalizedTime <= 0.95f)
        {
            stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        if (stateInfo.IsName("Melee"))
            EndAttackMelee();
        else if (stateInfo.IsName("Skill1"))
            EndGetHit();
        else if (stateInfo.IsName("Skill2"))
            EndGetHit();
        else if (stateInfo.IsName("BeShot"))
            EndGetHit();
    }

    public override void DyingMotion()
    {
        _isSkill2AttackReady = true;
        AttackSkill2();

        myAnimator.SetInteger("actionNum", 4);
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        gameObject.SetActive(false);
    }

    public override void HitByPlayer(int damage)
    {
        myAction = Action.Idle;
        aiMoveScript.enabled = false;

        myAnimator.SetInteger("actionNum", 3);
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        _health -= damage;
        if (_health <= 0)
        {
            // Dead
            DyingMotion();
        }

        StartCoroutine(WaitAnimationFinish());
    }

    public override void EndGetHit()
    {
        myAction = Action.Move;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        aiMoveScript.enabled = true;
    }

    #endregion

    private void PlayerEnteredRoom()
    {
        myAction = Action.Move;
        aiMoveScript.enabled = true;
    }

}
