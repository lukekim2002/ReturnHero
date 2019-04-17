using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalKingClass : MonsterBase
{
    #region PRIVATE VALUES

    int _id;
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

    //private bool _isSkill3AttackReady;
    private int _skill3Damage;
    //private float _Skill3CoolDown;

    private Vector2 attackColliderSize;
    private Vector2 attackColliderOffset;

    #endregion

    #region PUBLIC VALUES

    [HideInInspector]
    public Dictionary<string, object> myDataSet;
    [HideInInspector]
    public List<Dictionary<string, object>> myColliderSet;

    [Header("Refered Objects")]
    public MonsterBase myBase;
    public Pathfinding.AIPath aiMoveScript;
    public GameObject attackCollider;
    public BoxCollider2D attackColliderScript;
    public GameObject myMeleeAttackRange;
    public GameObject mySkill1AttackRange;
    public Animator myAnimator;
    //public GameObject skillEffect;

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

    private void FixedUpdate()
    {
        if (isAttacking == false && _isSkill2AttackReady == true && !_isMeleeAttackReady && !_isSkill1AttackReady)
            AttackSkill2();
    }

    #endregion

    #region MONSTERBASE IMPLEMENTAION

    public override void Initialize()
    {
        aiMoveScript = GetComponent<Pathfinding.AIPath>();
        playerObject = HeroGeneralManager.instance.heroObject;
        attackCollider = transform.GetChild(1).gameObject;
        attackColliderScript = attackCollider.GetComponent<BoxCollider2D>();
        myMeleeAttackRange = transform.GetChild(3).gameObject;
        mySkill1AttackRange = transform.GetChild(4).gameObject;
        myAnimator = GetComponent<Animator>();

        myBase = GetComponent<MonsterBase>();
        if (myBase == null) Debug.LogError("myBase is null.");

        myDataSet = MonsterDataManager.instance.ThrowDataIntoContainer((int)MonsterDataManager.MONSTER.ELEMENTAL_KING);
        myColliderSet = CSVReader.Read("CSV/Monster/Stage3/ReturnHero_king_AttackCollider");

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

        _isMeleeAttackReady = true;
        _isSkill1AttackReady = true;
        _isSkill2AttackReady = false;
        //_isSkill3AttackReady = false;


        StartCoroutine(CoolDownSkill2());
    }

    #region Melee

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

    public override void EndAttackMelee()
    {
        myAction = Action.Move;
        //myAttackCase = AttackCase.None;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.ResetTrigger("isMelee");
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;

        attackCollider.SetActive(false);
    }

    public override IEnumerator CoolDownMelee()
    {
        yield return new WaitForSeconds(_meleeCoolDown);
        _isMeleeAttackReady = true;
        myMeleeAttackRange.SetActive(true);
    }

    #endregion

    #region Skill1

    public override void AttackSkill1()
    {
        if (_isSkill1AttackReady == false && isAttacking == true) return;
        _isSkill1AttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        aiMoveScript.enabled = false;
        mySkill1AttackRange.SetActive(false);

        // Setting collider size and offset by its direction
        switch (myLookingDirection)
        {
            case LookingDirection.Top:

                attackColliderSize = new Vector2((float)myColliderSet[4]["Size_x"], (float)myColliderSet[4]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[4]["Offset_x"], (float)myColliderSet[4]["Offset_y"]);

                break;

            case LookingDirection.Down:

                attackColliderSize = new Vector2((float)myColliderSet[5]["Size_x"], (float)myColliderSet[5]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[5]["Offset_x"], (float)myColliderSet[5]["Offset_y"]);

                break;

            case LookingDirection.Left:

                attackColliderSize = new Vector2((float)myColliderSet[6]["Size_x"], (float)myColliderSet[6]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[6]["Offset_x"], (float)myColliderSet[6]["Offset_y"]);

                break;

            case LookingDirection.Right:

                attackColliderSize = new Vector2((float)myColliderSet[7]["Size_x"], (float)myColliderSet[7]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[7]["Offset_x"], (float)myColliderSet[7]["Offset_y"]);

                break;

        }

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isSkill1");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
        StartCoroutine(CoolDownSkill1());
    }

    public override void EndAttackSkill1()
    {
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
        mySkill1AttackRange.SetActive(true);
    }

    #endregion

    #region Skill2

    public override void AttackSkill2()
    {
        if (_isSkill2AttackReady == false && isAttacking == true) return;
        _isSkill2AttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        aiMoveScript.enabled = false;
        //mySkill2AttackRange.SetActive(false);

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isSkill2");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
        StartCoroutine(CoolDownSkill2());
    }

    public override void EndAttackSkill2()
    {
        myAction = Action.Move;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.ResetTrigger("isSkill2");
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;
    }

    public override IEnumerator CoolDownSkill2()
    {
        yield return new WaitForSeconds(_Skill2CoolDown);
        _isSkill2AttackReady = true;
    }

    #endregion

    #region Skill3

    public override void AttackSkill3()
    {
        myAction = Action.Attack;
        isAttacking = true;
        aiMoveScript.enabled = false;
        //mySkill2AttackRange.SetActive(false);

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isSkill3");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
    }

    public override void EndAttackSkill3()
    {
        gameObject.SetActive(false);
    }

    // Not Used
    public override IEnumerator CoolDownSkill3()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region Animation

    public override bool CheckAnimatorStateName(AnimatorStateInfo stateInfo)
    {
        return (stateInfo.IsName("Melee")
            || stateInfo.IsName("Skill1")
            || stateInfo.IsName("Skill2")
            || stateInfo.IsName("Skill3")
            || stateInfo.IsName("BeShot")
            || stateInfo.IsName("Die"));
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
            EndAttackSkill1();
        else if (stateInfo.IsName("Skill2"))
            EndAttackSkill2();
        else if (stateInfo.IsName("Skill3"))
            EndAttackSkill3();
        else if (stateInfo.IsName("BeShot"))
            EndGetHit();
        /*
        else if(stateInfo.IsName("Die"))
            gameObject.SetActive(false);
            */
    }

    #endregion



    public override void DyingMotion()
    {
        Debug.Log("Die");


        //myAnimator.SetInteger("actionNum", 3);
        myAnimator.SetInteger("actionNum", 4);
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        //StartCoroutine(WaitAnimationFinish());

        //gameObject.SetActive(false);
    }

    #region Hit

    public override void HitByPlayer(int damage)
    {
        myAction = Action.Idle;
        aiMoveScript.enabled = false;

        _health -= damage;
        Debug.Log("current health : " + _health);

        myAnimator.SetInteger("actionNum", 3);
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);



        StartCoroutine(WaitAnimationFinish());


    }

    public override void EndGetHit()
    {
        //Debug.Log("GateKeeper EndGetHit");

        if (_health <= 0)
        {
            // Dead
            DyingMotion();
        }

        myAction = Action.Move;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        aiMoveScript.enabled = true;
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



    #endregion

    #region NOT USED

    

    public override void AttackSkill4()
    {
        throw new System.NotImplementedException();
    }

    

    public override IEnumerator CoolDownSkill4()
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
