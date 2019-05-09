using System.Collections;
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

        //Debug.Log(_isSkill3TriggerOk);

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

        Debug.Log(_Skill3CoolDown);

        _isMeleeAttackReady = true;
        _isSkill1AttackReady = true;
        _isSkill2AttackReady = true;
        _isSkill3AttackReady = true;
        _isSkill3TriggerOk = true;
    }

    #region ATTACK

    #region Melee
    public override void AttackMelee()
    {
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

        attackCollider.SetActive(true);
        attackColliderScript.size = attackColliderSize;
        attackColliderScript.offset = attackColliderOffset;

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

        attackCollider.SetActive(false);
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
        mySkill2AttackRange.SetActive(false);

        switch (myLookingDirection)
        {
            case LookingDirection.Top:

                attackColliderSize = new Vector2((float)myColliderSet[8]["Size_x"], (float)myColliderSet[8]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[8]["Offset_x"], (float)myColliderSet[8]["Offset_y"]);

                break;

            case LookingDirection.Down:

                attackColliderSize = new Vector2((float)myColliderSet[9]["Size_x"], (float)myColliderSet[9]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[9]["Offset_x"], (float)myColliderSet[9]["Offset_y"]);

                break;

            case LookingDirection.Left:

                attackColliderSize = new Vector2((float)myColliderSet[10]["Size_x"], (float)myColliderSet[10]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[10]["Offset_x"], (float)myColliderSet[10]["Offset_y"]);

                break;

            case LookingDirection.Right:

                attackColliderSize = new Vector2((float)myColliderSet[11]["Size_x"], (float)myColliderSet[11]["Size_y"]);
                attackColliderOffset = new Vector2((float)myColliderSet[11]["Offset_x"], (float)myColliderSet[11]["Offset_y"]);

                break;

        }

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isSkill2");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        attackCollider.SetActive(true);
        attackColliderScript.size = attackColliderSize;
        attackColliderScript.offset = attackColliderOffset;

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

        attackCollider.SetActive(false);
    }

    public override IEnumerator CoolDownSkill2()
    {
        yield return new WaitForSeconds(_Skill2CoolDown);
        _isSkill2AttackReady = true;
        mySkill2AttackRange.SetActive(true);
    }
    #endregion

    #region Skill3
    public override void AttackSkill3()
    {
        if (_isSkill3AttackReady == false && isAttacking == true) return;
        _isSkill3AttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
        aiMoveScript.enabled = false;

        myAnimator.SetInteger("actionNum", 2);
        myAnimator.SetTrigger("isSkill3");
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);

        StartCoroutine(WaitAnimationFinish());
        StartCoroutine(CoolDownSkill3());
    }

    public override void EndAttackSkill3()
    {
        myAction = Action.Move;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.ResetTrigger("isSkill3");
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;
    }

    public override IEnumerator CoolDownSkill3()
    {
        yield return new WaitForSeconds(_Skill3CoolDown);
        _isSkill3AttackReady = true;
        _isSkill3TriggerOk = true;
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

    #region HIT
    public override void HitByPlayer(int damage)
    {
        myAction = Action.Idle;
        aiMoveScript.enabled = false;

        _health -= damage;
        Debug.Log("current health : " + _health);

        StartCoroutine(WaitAnimationFinish());

        if (_health <= 0)
        {
            // Dead
            DyingMotion();
        }

        myAnimator.SetInteger("actionNum", 3);
        myAnimator.SetFloat("actionX", myDirection.x);
        myAnimator.SetFloat("actionY", myDirection.y);
    }

    public override void EndGetHit()
    {
        myAction = Action.Move;
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        aiMoveScript.enabled = true;
    }

    public override void DyingMotion()
    {
        Debug.Log("Die");

        myAnimator.SetInteger("actionNum", 4);

        StartCoroutine(WaitAnimationFinish());

        gameObject.SetActive(false);
    }
    #endregion

    #region Animator Operation

    public override bool CheckAnimatorStateName(AnimatorStateInfo stateInfo)
    {
        return (stateInfo.IsName("Melee")
            || stateInfo.IsName("Skill1")
            || stateInfo.IsName("Skill2")
            || stateInfo.IsName("Skill3_3Phase")
            || stateInfo.IsName("BeShot"))
            || stateInfo.IsName("Die");
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
        else if (stateInfo.IsName("Skill3_3Phase"))
            EndAttackSkill3();
        else if (stateInfo.IsName("BeShot"))
            EndGetHit();
        /*
        else if(stateInfo.IsName("Die"))
            gameObject.SetActive(false);
            */
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

    public override void ResetAnimatorTrigger()
    {
        throw new System.NotImplementedException();
    }
}
