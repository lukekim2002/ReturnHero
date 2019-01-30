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
    private int _skill2Damage;
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
    public Animator myAnimator;

    public GameObject attackCollider;
    public BoxCollider2D attackColliderScript;
    public GameObject myMeleeAttackRange;


    [Header("State Values")]
    public Vector2 myDirection;
    public Action myAction;
    public AttackCase myAttackCase;
    public bool isAttacking = false;

    #endregion

    #region  MONOBEHAVIOUR CALLBACKS

    private void OnEnable()
    {
        myAction = Action.Idle;
        myAttackCase = AttackCase.None;


    }


    // Update is called once per frame
    private void Update()
    {

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

        myBase = GetComponent<MonsterBase>();
        if (myBase == null) Debug.LogError("myBase is null.");


    }


    public override void AttackMelee()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackSkill1()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackSkill2()
    {
        throw new System.NotImplementedException();
    }

    
    public override IEnumerator CoolDownMelee()
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

    public override void EndAttackMelee()
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

    public override void EndGetHit()
    {
        throw new System.NotImplementedException();
    }

    public override bool CheckAnimatorStateName(AnimatorStateInfo stateInfo)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator WaitAnimationFinish()
    {
        throw new System.NotImplementedException();
    }

    public override void DyingMotion()
    {
        throw new System.NotImplementedException();
    }

    public override void HitByPlayer(int damage)
    {
        throw new System.NotImplementedException();
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
}
