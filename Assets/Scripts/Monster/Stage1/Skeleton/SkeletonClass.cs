using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonClass : MonsterBase {

    #region PRIVATE VALUES

    private int _id;
    private int _health;
    private float _movingSpeed;

    private bool _isMeleeAttackReady;
    private int _meleeDamage;
    private float _meleeCoolDown;

    private Vector2 attackColliderSize;
    private Vector2 attackColliderOffset;

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

    [Header("State Values")]
    public Vector2 myDirection;
    public Action myAction;
    public bool isAttacking = false;

    #endregion

    #region MonoBahvaiour CallBacks

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
    }

    #endregion

    public override void Initialize()
    {
        aiMoveScript = GetComponent<Pathfinding.AIPath>();
        playerObject = HeroGeneralManager.instance.heroObject;
        attackCollider = transform.GetChild(1).gameObject;
        attackColliderScript = attackCollider.GetComponent<BoxCollider2D>();
        myMeleeAttackRange = transform.GetChild(3).gameObject;
        myAnimator = GetComponent<Animator>();

        myBase = GetComponent<MonsterBase>();
        if (myBase == null)
        {
            Debug.LogError("myBase is null");
        }
        myDataSet = MonsterDataManager.instance.ThrowDataIntoContainer((int)MonsterDataManager.MONSTER.SKELETON);
        myColliderSet = CSVReader.Read("CSV/Monster/Stage1/ReturnHero_Skeleton_AttackCollider");


        _id = (int)myDataSet["ID"];
        _health = (int)myDataSet["Health"];
        _movingSpeed = (float)myDataSet["MovingSpeed"];

        _meleeDamage = (int)myDataSet["MeleeDamage"];
        _meleeCoolDown = (int)myDataSet["MeleeCoolDown"];

        _isMeleeAttackReady = true;

        Debug.Log("Initialized : " + _id + ", " + _health + ", " + _movingSpeed + ", " + _meleeDamage + ", " + _meleeCoolDown);

        //aiMoveScript.maxSpeed = _movingSpeed;
    }

    public override void AttackMelee()
    {
        if (_isMeleeAttackReady == false && isAttacking == true) return;
        _isMeleeAttackReady = false;

        myAction = Action.Attack;
        isAttacking = true;
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
        myAnimator.SetInteger("actionNum", 1);
        myAnimator.SetFloat("moveX", myDirection.x);
        myAnimator.SetFloat("moveY", myDirection.y);
        isAttacking = false;
        aiMoveScript.enabled = true;

        attackCollider.SetActive(false);
    }

    public override IEnumerator CoolDownMelee()
    {
        yield return new WaitForSeconds(_meleeCoolDown);
        myMeleeAttackRange.SetActive(true);
        _isMeleeAttackReady = true;
    }



    #region NOT USED

    public override void AttackSkill1()
    {
        throw new System.NotImplementedException();
    }

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

    public override void EndAttackSkill1()
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

    public override IEnumerator CoolDownSkill1()
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
