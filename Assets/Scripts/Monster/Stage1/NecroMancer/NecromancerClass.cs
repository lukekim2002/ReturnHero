using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerClass : MonsterBase {

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
    //private int _skill2Damage;
    private float _Skill2CoolDown;

    private Vector2 attackColliderSize;
    private Vector2 attackColliderOffset;

    #endregion

    #region PUBLIC VALUES

    public Dictionary<string, object> myDataSet;
    public List<Dictionary<string, object>> myColliderSet;

    [Header("Refered Objects")]
    public MonsterBase myBase;
    public Pathfinding.AIPath aiMoveScript;
    public Pathfinding.AIDestinationSetter aiDestinationSetter;

    public Animator myAnimator;

    public GameObject attackCollider;
    public BoxCollider2D attackColliderScript;
    public GameObject myMeleeAttackRange;

    public GameObject mySkill2Object; // Animation Object for spawning lich


    [Header("State Values")]
    public Vector2 myDirection;
    public Action myAction;
    public AttackCase myAttackCase;
    public bool isAttacking = false;


    [HideInInspector]
    public bool isCoroutineRunning;

    #endregion

    #region  MONOBEHAVIOUR CALLBACKS

    private void OnEnable()
    {
        myAction = Action.Idle;



        Initialize();

        aiMoveScript.enabled = false;

        StartCoroutine(myBase.FindLookingDirection());

        PlayerEnteredRoom();
    }


    // Update is called once per frame
    private void Update()
    {
        if (isAttacking == false)
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

        /*
        if (!isCoroutineRunning && _isSkill2AttackReady && !isAttacking)
        {
            AttackSkill2();
        }

        if (!isCoroutineRunning && _isSkill1AttackReady && !isAttacking)
        {
            AttackSkill1();
        }

       */
        if (myAction == Action.Move && !isAttacking && !isCoroutineRunning)
        {
            if (_isSkill2AttackReady)
                AttackSkill2();
            else if (_isSkill1AttackReady)
                AttackSkill1();
        }

    }

    #endregion

    #region MONSTERBASE IMPLEMENTATION

    public override void Initialize()
    {
        aiMoveScript = GetComponent<Pathfinding.AIPath>();
        playerObject = HeroGeneralManager.instance.heroObject;
        attackCollider = transform.GetChild(1).gameObject;
        attackColliderScript = attackCollider.GetComponent<BoxCollider2D>();
        myMeleeAttackRange = transform.GetChild(3).gameObject;
        myAnimator = GetComponent<Animator>();
        aiDestinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        //aiDestinationSetter.target = playerObject.transform;

        myBase = GetComponent<MonsterBase>();
        if (myBase == null) Debug.LogError("myBase is null.");

        myDataSet = MonsterDataManager.instance.ThrowDataIntoContainer((int)MonsterDataManager.MONSTER.NECROMANCER);
        myColliderSet = CSVReader.Read("CSV/Monster/Stage1/ReturnHero_Necromancer_AttackCollider");

        _id = (int)myDataSet["ID"];
        _health = (int)myDataSet["Health"];
        _movingSpeed = (float)myDataSet["MovingSpeed"];
        aiMoveScript.maxSpeed = _movingSpeed;

        _meleeDamage = (int)myDataSet["MeleeDamage"];
        _meleeCoolDown = (int)myDataSet["MeleeCoolDown"];

        _skill1Damage = (int)myDataSet["Skill1Damage"];
        _Skill1CoolDown = (int)myDataSet["Skill1CoolDown"];

        _Skill2CoolDown = (int)myDataSet["Skill2CoolDown"];

        _isMeleeAttackReady = true;
        _isSkill1AttackReady = true;
        _isSkill2AttackReady = false;
        //_isSkill1TriggerOk = false;
    }


    public override void AttackMelee()
    {
        if (_isMeleeAttackReady == false && isAttacking == true) return;
        _isMeleeAttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        //myAttackCase = AttackCase.Melee;
        aiMoveScript.enabled = false;
        myMeleeAttackRange.SetActive(false);

        // Setting collider size and offset by its direction
        switch (myLookingDirection)
        {
            case LookingDirection.Top:

                attackColliderSize = new Vector2((float)myColliderSet[0]["Size_x"], (float)myColliderSet[0]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[0]["Offset_x"], (float)myColliderSet[0]["Offset_y"]);

                break;

            case LookingDirection.Down:

                attackColliderSize = new Vector2((float)myColliderSet[1]["Size_x"], (float)myColliderSet[1]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[1]["Offset_x"], (float)myColliderSet[1]["Offset_y"]);

                break;

            case LookingDirection.Left:

                attackColliderSize = new Vector2((float)myColliderSet[2]["Size_x"], (float)myColliderSet[2]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[2]["Offset_x"], (float)myColliderSet[2]["Offset_y"]);

                break;

            case LookingDirection.Right:

                attackColliderSize = new Vector2((float)myColliderSet[3]["Size_x"], (float)myColliderSet[3]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[3]["Offset_x"], (float)myColliderSet[3]["Offset_y"]);

                break;

        }

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isMelee");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        attackCollider.SetActive(true);
        attackColliderScript.size = attackColliderSize;
        attackColliderScript.offset = attackColliderOffset;

        StartCoroutine(WaitAnimationFinish());
        StartCoroutine(CoolDownMelee());
    }

    public override void AttackSkill1()
    {
        /* Attack with 5 circles
         * 
         */

        if (_isSkill1AttackReady == false && isAttacking == true) return;
        _isSkill1AttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        //myAttackCase = AttackCase.Skill2;
        aiMoveScript.enabled = false;
        //mySkill1AttackRange.SetActive(false);

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isSkill1");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
        StartCoroutine(CoolDownSkill1());
    }

    public override void AttackSkill2()
    {
        /* Spawn 3 Liches
         * 
         */

        if (_isSkill2AttackReady == false && isAttacking == true) return;
        _isSkill2AttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        aiMoveScript.enabled = false;

        mySkill2Object.SetActive(true);
        foreach (Transform child in mySkill2Object.transform)
        {
            child.gameObject.SetActive(true);
        }
        //ebug.Log(mySkill2Object.activeSelf);

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isSkill2");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());

        StartCoroutine(CoolDownSkill2());
        //Invoke("EndAttackSkill2", 0.32f);
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
    }

    public override IEnumerator CoolDownSkill2()
    {
        yield return new WaitForSeconds(_Skill2CoolDown);
        _isSkill2AttackReady = true;
    }

    public override void EndAttackMelee()
    {
        myAction = Action.Move;
        //myAttackCase = AttackCase.None;
        myAnimator.SetInteger("actionNum", 0);
        myAnimator.ResetTrigger("isMelee");
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;

        attackCollider.SetActive(false);
    }

    public override void EndAttackSkill1()
    {
        myAction = Action.Move;
        //myAttackCase = AttackCase.None;
        myAnimator.SetInteger("actionNum", 0);
        myAnimator.ResetTrigger("isSkill1");
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;
    }

    public override void EndAttackSkill2()
    {
        myAction = Action.Move;
        //myAttackCase = AttackCase.None;
        myAnimator.SetInteger("actionNum", 0);
        //myAnimator.ResetTrigger("isSkill1");
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;

        //mySkill2Object.SetActive(false);
    }

    

    public override bool CheckAnimatorStateName(AnimatorStateInfo stateInfo)
    {
        return (stateInfo.IsName("Melee")
           || stateInfo.IsName("Skill1")
           || stateInfo.IsName("Skill2")
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

        if (stateInfo.IsName("Skill2"))
            EndAttackSkill1();

        if (stateInfo.IsName("BeShot_Alive"))
            EndGetHit();

        if (stateInfo.IsName("Die"))
            gameObject.SetActive(false);

        /*
        else if(stateInfo.IsName("Die"))
            gameObject.SetActive(false);
            */
    }

    public override void DyingMotion()
    {
        
    }

    public override void EndGetHit()
    {
        //Debug.Log("GateKeeper EndGetHit");

        myAction = Action.Move;
        myAnimator.SetInteger("actionNum", 0);
        myAnimator.ResetTrigger("BeShot");
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        aiMoveScript.enabled = true;
    }

    public override void HitByPlayer(int damage)
    {
        myAction = Action.Idle;
        aiMoveScript.enabled = false;

        if (isCoroutineRunning &&
           (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Melee") ||
           myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Skill")))
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

    #endregion

    #region NOT USED

    public override void AttackSkill3()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackSkill4()
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

    

    public override void EndAttackSkill3()
    {
        throw new System.NotImplementedException();
    }

    public override void EndAttackSkill4()
    {
        throw new System.NotImplementedException();
    }



    #endregion

    public void PlayerEnteredRoom()
    {
        myAction = Action.Move;
        aiMoveScript.enabled = true;
    }
}
