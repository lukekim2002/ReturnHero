using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISkill_Sword : MonoBehaviour, IWeaponInterface, ISkillInterface {

    #region PRIVATE
    private HeroController _heroController;
    private Animator _heroAnimator;

    private Vector2 moveColliderSize;
    private float colliderDiagonalLength;
    private Vector2 colliderSize;
    private Vector2 colliderOffset;

    int i = 0;

    private float sEReadyDist = 0.18f;
    private float sEExecuteDist = 1.78f;
    #endregion

    #region PUBLIC
    public GameObject skillMREffect;
    public GameObject skillREffect;

    public enum Skill_E { UP = 12, DOWN = 13, LEFT = 14, RIGHT = 15 };
    public enum Skill_MR { UP = 16, DOWN = 17, LEFT = 18, RIGHT = 19 };
    public enum Skill_R { UP = 20, DOWN = 21, LEFT = 22, RIGHT = 23 };
    #endregion

    private void Awake()
    {
        _heroController = GetComponent<HeroController>();
        _heroAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        moveColliderSize = HeroGeneralManager.instance.colliderSize;
        colliderDiagonalLength = HeroGeneralManager.instance.colliderDiagonalLength;
    }

    #region Skill Interface Methods Definition

    #region Mouse Right Button Methods
    void ISkillInterface.Skill_MR_Ready()
    {
        Vector2 heroPos = transform.position;
        heroPos += _heroController.direction * 0.09f;
        this.transform.position = heroPos;
    }

    void ISkillInterface.Skill_MR_Execute()
    {
        Vector2 direction = _heroController.direction;

        if (direction == Vector2.up) i = (int)Skill_MR.UP;
        else if (direction == Vector2.down) i = (int)Skill_MR.DOWN;
        else if (direction == Vector2.left) i = (int)Skill_MR.LEFT;
        else if (direction == Vector2.right) i = (int)Skill_MR.RIGHT;

        //colliderSize = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Height"]);
        //colliderOffset = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Height"]);
        //HeroGeneralManager.instance.SetAttackColliderActive();
        //HeroGeneralManager.instance.atkCollider.size = colliderSize;
        //HeroGeneralManager.instance.atkCollider.offset = colliderOffset;

        Vector2 heroPos = transform.position;

        heroPos += direction * 0.18f;
        this.transform.position = heroPos;
    }

    void ISkillInterface.Skill_MR_EffectOn()
    {
        Skill_MR_EffectManager.isSkillMr = true;
        skillMREffect.SetActive(true);
        skillMREffect.transform.position = this.transform.position;
    }

    void ISkillInterface.Skill_MR_End()
    {
        HeroController.heroState = HeroController.HEROSTATE.IDLE;
        HeroController.heroAttackState = HeroController.HEROATTACKSTATE.NONE;

        _heroAnimator.SetFloat("actionX", _heroController.direction.x);
        _heroAnimator.SetFloat("actionY", _heroController.direction.y);

        // 어택하고 나서 남은 moveAxis 값을 0으로 되돌려줘야 다음 Move가 어색하지 않음.
        _heroController.moveAxis = Vector2.zero;

        HeroGeneralManager.instance.SetAttackColliderInactive();
    }
    #endregion

    #region Keyboard E Button Methods
    void ISkillInterface.Skill_E_Ready()
    {
        Vector2 heroPos = transform.position;
        Vector2 direction = _heroController.direction;
        Vector2 hitPoint;
        
        hitPoint = GameGeneralManager.instance.IsWallInFrontOfCharacter(heroPos, direction, sEReadyDist);


        if (hitPoint != Vector2.zero)
        {
            float magnitude = (hitPoint - heroPos).magnitude;

            if (magnitude <= colliderDiagonalLength)
                direction = Vector2.zero;

            direction = direction * (magnitude - moveColliderSize.magnitude);

        }
        else
        {
            direction = direction * sEReadyDist;
        }


        heroPos += direction;
        this.transform.position = heroPos;
    }

    void ISkillInterface.Skill_E_Execute()
    {
        Vector2 heroPos = transform.position;
        Vector2 direction = _heroController.direction;
        Vector2 hitPoint;

        if (direction == Vector2.up) i = (int)Skill_E.UP;
        else if (direction == Vector2.down) i = (int)Skill_E.DOWN;
        else if (direction == Vector2.left) i = (int)Skill_E.LEFT;
        else if (direction == Vector2.right) i = (int)Skill_E.RIGHT;

        hitPoint = GameGeneralManager.instance.IsWallInFrontOfCharacter(heroPos, direction, sEExecuteDist);


        if (hitPoint != Vector2.zero)
        {
            float magnitude = (hitPoint - heroPos).magnitude;

            if (magnitude <= colliderDiagonalLength)
                direction = Vector2.zero;

            direction = direction * (magnitude - moveColliderSize.magnitude);
        }
        else
        {
            direction = direction * sEExecuteDist;
        }

        colliderSize = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Height"]);
        colliderOffset = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Height"]);

        HeroGeneralManager.instance.SetAttackColliderActive();
        HeroGeneralManager.instance.atkCollider.size = colliderSize;
        HeroGeneralManager.instance.atkCollider.offset = colliderOffset;

        heroPos += direction;
        this.transform.position = heroPos;
    }

    void ISkillInterface.Skill_E_EffectOn()
    {
        throw new System.NotImplementedException();
    }

    void ISkillInterface.Skill_E_End()
    {
        HeroController.heroState = HeroController.HEROSTATE.IDLE;
        HeroController.heroAttackState = HeroController.HEROATTACKSTATE.NONE;

        _heroAnimator.SetFloat("actionX", _heroController.direction.x);
        _heroAnimator.SetFloat("actionY", _heroController.direction.y);

        // 어택하고 나서 남은 moveAxis 값을 0으로 되돌려줘야 다음 Move가 어색하지 않음.
        _heroController.moveAxis = Vector2.zero;

        HeroGeneralManager.instance.SetAttackColliderInactive();
    }
    #endregion

    #region Keyboard R Button Methods
    void ISkillInterface.Skill_R_Ready()
    {
        Vector2 heroPos = transform.position;
        heroPos += _heroController.direction * 0.09f;
        this.transform.position = heroPos;
    }

    void ISkillInterface.Skill_R_Execute()
    {
        Vector2 direction = _heroController.direction;

        if (direction == Vector2.up) i = (int)Skill_R.UP;
        else if (direction == Vector2.down) i = (int)Skill_R.DOWN;
        else if (direction == Vector2.left) i = (int)Skill_R.LEFT;
        else if (direction == Vector2.right) i = (int)Skill_R.RIGHT;

        Vector2 heroPos = transform.position;

        heroPos += direction * 0.18f;
        this.transform.position = heroPos;
    }

    void ISkillInterface.Skill_R_EffectOn()
    {
        Skill_R_EffectManager.isSkillR = true;

        print(i);

        colliderSize = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Height"]);
        colliderOffset = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Height"]);
        HeroGeneralManager.instance.SetAttackColliderActive();
        HeroGeneralManager.instance.atkCollider.size = colliderSize;
        HeroGeneralManager.instance.atkCollider.offset = colliderOffset;

        skillREffect.SetActive(true);
        skillREffect.transform.position = this.transform.position;
    }

    void ISkillInterface.Skill_R_End()
    {
        HeroController.heroState = HeroController.HEROSTATE.IDLE;
        HeroController.heroAttackState = HeroController.HEROATTACKSTATE.NONE;

        _heroAnimator.SetFloat("actionX", _heroController.direction.x);
        _heroAnimator.SetFloat("actionY", _heroController.direction.y);

        // 어택하고 나서 남은 moveAxis 값을 0으로 되돌려줘야 다음 Move가 어색하지 않음.
        _heroController.moveAxis = Vector2.zero;

        HeroGeneralManager.instance.SetAttackColliderInactive();
    }
    #endregion

    #endregion
}
