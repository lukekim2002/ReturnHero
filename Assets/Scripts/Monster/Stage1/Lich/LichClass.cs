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
    [SerializeField]
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
    public Pathfinding.AIDestinationSetter aiDestinationSetter;

    public GameObject attackCollider;
    public BoxCollider2D attackColliderScript;
    public GameObject myMeleeAttackRange;
    public Animator myAnimator;

    //public GameObject skillEffect;

    [Header("State Values")]
    public Vector2 myDirection;
    public Action myAction;
    public bool isAttacking = false;

    [HideInInspector]
    public bool isCoroutineRunning;

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
        if(isAttacking == false)
            myDirection = myBase.direction;

        
        /*
        if (myAction == Action.Idle && isAttacking == false)
        {
            myAnimator.SetInteger("actionNum", 0);
            myAnimator.SetFloat("actionX", myDirection.x);
            myAnimator.SetFloat("actionY", myDirection.y);
        }
        */
        if (myAction == Action.Move && isAttacking == false)
        {
            myAnimator.SetInteger("actionNum", 1);
            myAnimator.SetFloat("moveX", myDirection.x);
            myAnimator.SetFloat("moveY", myDirection.y);
        }

        
        if (_isSkill1AttackReady && !isAttacking && _isSkill1TriggerOk == true)
        {
            AttackSkill1();
        }
        
    }

    #endregion

    public override void Initialize()
    {
        print("Lich Initialize");
        aiMoveScript = GetComponent<Pathfinding.AIPath>();
        playerObject = HeroGeneralManager.instance.heroObject;
        //attackCollider = transform.GetChild(1).gameObject;
        //attackColliderScript = attackCollider.GetComponent<BoxCollider2D>();
        myMeleeAttackRange = transform.GetChild(3).gameObject;
        myAnimator = GetComponent<Animator>();
        aiDestinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        //aiDestinationSetter.target = playerObject.transform;

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

        //Debug.Log("Initialized : " + _id + ", " + _health + ", " + _movingSpeed + ", " + _meleeDamage + ", " + _meleeCoolDown);

        aiMoveScript.maxSpeed = _movingSpeed;
    }

    #region Attack

    #region Melee

    public override void AttackMelee()
    {
        print("Lich Attack Melee");
        if (_isMeleeAttackReady == false && isAttacking == true) return;
        _isMeleeAttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        aiMoveScript.enabled = false;
        myMeleeAttackRange.SetActive(false);

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isMelee");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
        StartCoroutine(CoolDownMelee());
    }

    public override void EndAttackMelee()
    {
        print("Lich End Attack Melee");
        myAction = Action.Move;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.ResetTrigger("isMelee");
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

    #endregion

    #region Skill1

    public override void AttackSkill1()
    {
        print("Lich Attack Skill1");
        if (_isSkill1AttackReady == false && isAttacking == true) return;
        _isSkill1AttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        aiMoveScript.enabled = false;
        //mySkill1AttackRange.SetActive(false);

        // Instantiate Skeleton and skill1Effect
        //skillEffect.SetActive(true);
        //print("Lich AttackSkill1 Executed");

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isSkill1");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
        StartCoroutine(CoolDownSkill1());
    }

    public override void EndAttackSkill1()
    {
        print("Lich End Attack Skill1");
        myAction = Action.Move;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.ResetTrigger("isSkill1");
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

    #endregion

    #endregion

    #region Animator Control

    public override bool CheckAnimatorStateName(AnimatorStateInfo stateInfo)
    {
        return (stateInfo.IsName("Melee")
            || stateInfo.IsName("Skill1")
            || stateInfo.IsName("BeShot_Alive")
            || stateInfo.IsName("Die"));
    }

    public override IEnumerator WaitAnimationFinish()
    {
        isCoroutineRunning = true;
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

        isCoroutineRunning = false;

        if (stateInfo.IsName("Melee"))
            EndAttackMelee();

        if (stateInfo.IsName("Skill1"))
            EndAttackSkill1();

        if (stateInfo.IsName("BeShot_Alive"))
            EndGetHit();

        if (stateInfo.IsName("Die"))
            gameObject.SetActive(false);
    }

    #endregion

    #region Hit

    public override void HitByPlayer(int damage)
    {
        print("Lich Hit");
        myAction = Action.Idle;
        aiMoveScript.enabled = false;

        if (isCoroutineRunning &&
            (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Melee") || 
            myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Skill1")))
        {
            //print("Animator is in Melee state already");
            StopCoroutine(WaitAnimationFinish());
        }

        _health -= damage;
        Debug.Log("current health : " + _health);

        if (_health > 0)
        {
            myAnimator.SetTrigger("BeShot");
        }
        else
        {
            myAnimator.SetTrigger("Dead");
        }

        myAnimator.SetInteger("actionNum", 3);
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
    }

    public override void EndGetHit()
    {
        print("Lich End Hit");
        myAction = Action.Move;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.ResetTrigger("BeShot");
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        aiMoveScript.enabled = true;
    }

    public override void DyingMotion()
    {
        /*
        myAnimator.SetInteger("actionNum", 4);
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());

        gameObject.SetActive(false);
        */
    }

    #endregion


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

    

    

    

    

    public override void GetHealed(GameGeneralManager.HealInfo myHeal)
    {

        if (myHeal.option == GameGeneralManager.NumericTypeOption.Fixed) // 고정 값
        {
            _health += myHeal.value;
        }

        else if (myHeal.option == GameGeneralManager.NumericTypeOption.Percentage) // 퍼센트 값
        {
            float pValue = myHeal.value * 0.01f;
            _health += (int)(_health * pValue);
        }
    }

    public void PlayerEnteredRoom()
    {
        myAction = Action.Move;
        aiMoveScript.enabled = true;
    }


}
