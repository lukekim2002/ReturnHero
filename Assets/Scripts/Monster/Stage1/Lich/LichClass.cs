using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichClass : MonsterBase {

    #region PRIVATE VALUES

    int _id;
    private int _health;
    private float _movingSpeed;

    private bool _isMeleeAttackReady;
    private int _meleeDamage;
    public float _meleeCoolDown;

    private bool _isSkill1AttackReady;
    private float _Skill1CoolDown;
    public bool _isSkill1TriggerOk;

    //private Vector2 attackColliderSize;
    //private Vector2 attackColliderOffset;

    #endregion

    #region PUBLIC VALUES

    public Dictionary<string, object> myDataSet;
    public List<Dictionary<string, object>> myColliderSet;

    [Header("Refered Objects")]
    public MonsterBase myBase;
    public Pathfinding.AIPath aiMoveScript;
    public GameObject attackCollider;
    public BoxCollider2D attackColliderScript;
    public GameObject myMeleeAttackRange;
    public Animator myAnimator;
    public GameObject skillEffect1;
    public GameObject skillEffect2;

    [Header("State Values")]
    public Vector2 myDirection;
    public Action myAction;
    public bool isAttacking = false;

    #endregion

    #region MONOBEHAVIOUR CALLBACKS

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
        myDirection = myBase.direction;

        if (_health <= 0)
        {
            DyingMotion();
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

        if (_isSkill1AttackReady == true && _isSkill1TriggerOk == true)
        {
            AttackSkill1();
        }
    }

    #endregion

    public override void Initialize()
    {
        aiMoveScript = GetComponent<Pathfinding.AIPath>();
        playerObject = HeroGeneralManager.instance.heroObject;
        //attackCollider = transform.GetChild(1).gameObject;
        //attackColliderScript = attackCollider.GetComponent<BoxCollider2D>();
        myMeleeAttackRange = transform.GetChild(4).gameObject;
        myAnimator = GetComponent<Animator>();

        myBase = GetComponent<MonsterBase>();
        if (myBase == null)
        {
            Debug.LogError("myBase is null");
        }
        myDataSet = MonsterDataManager.instance.ThrowDataIntoContainer((int)MonsterDataManager.MONSTER.LICH);



        _id = (int)myDataSet["ID"];
        _health = (int)myDataSet["Health"];
        _movingSpeed = (float)myDataSet["MovingSpeed"];

        _meleeDamage = (int)myDataSet["MeleeDamage"];
        _meleeCoolDown = (int)myDataSet["MeleeCoolDown"];
        _isMeleeAttackReady = true;

        //_skill1Damage = (int)myDataSet["Skill1Damage"];
        _Skill1CoolDown = (int)myDataSet["Skill1CoolDown"];
        _isSkill1AttackReady = true;
        _isSkill1TriggerOk = true;

        Debug.Log("Initialized : " + _id + ", " + _health + ", " + _movingSpeed + ", " + _meleeDamage + ", " + _meleeCoolDown);

        aiMoveScript.maxSpeed = _movingSpeed;
    }

    public override void AttackMelee()
    {
        if (_isMeleeAttackReady == false && isAttacking == true) return;
        _isMeleeAttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        aiMoveScript.enabled = false;
        myMeleeAttackRange.SetActive(false);

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
        StartCoroutine(CoolDownMelee());
    }

    public override void EndAttackMelee()
    {
        myAction = Action.Move;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;
    }

    public override IEnumerator CoolDownMelee()
    {
        yield return new WaitForSeconds(_meleeCoolDown);
        myMeleeAttackRange.SetActive(true);
        _isMeleeAttackReady = true;

    }

    public override void AttackSkill1()
    {
        if (_isSkill1AttackReady == false && isAttacking == true) return;
        _isSkill1AttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        //myAttackCase = AttackCase.Skill1;
        aiMoveScript.enabled = false;
        //mySkill1AttackRange.SetActive(false);

        // Instantiate Skeleton and skill1Effect
        skillEffect1.SetActive(true);
        skillEffect2.SetActive(true);
        print("Lich AttackSkill1 Executed");

        StartCoroutine(CoolDownSkill1());
        Invoke("EndAttackSkill1", 0.5f);
    }

    public override void EndAttackSkill1()
    {
        myAction = Action.Move;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;
    }

    public override IEnumerator CoolDownSkill1()
    {
        yield return new WaitForSeconds(_Skill1CoolDown);
        _isSkill1AttackReady = true;
    }

    #region NOT USED



    public override void AttackSkill2()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackSkill3()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackSkill4()
    {
        throw new System.NotImplementedException();
    }

    

    public override void EndAttackSkill2()
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

    

    public override IEnumerator CoolDownSkill2()
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
            ||  stateInfo.IsName("BeShot")
            ||  stateInfo.IsName("Die"));
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
        else if (stateInfo.IsName("BeShot"))
            EndGetHit();
    }

    public override void DyingMotion()
    {
        myAnimator.SetInteger("actionNum", 4);
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());

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
        Debug.Log("current health : " + _health);

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

    public void PlayerEnteredRoom()
    {
        myAction = Action.Move;
        aiMoveScript.enabled = true;
    }


}
