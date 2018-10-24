using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieClass : MonoBehaviour, IMonsterInterface
{
    #region PRIVATE VALUES

    private int _health;
    private float _movingSpeed;

    private bool _isMeleeAttackReady;
    private int _meleeDamage;
    public float _meleeCoolDown;
    private Vector2 _meleeBoxSizeUp;
    private Vector2 _meleeBoxSizedown;
    private Vector2 _meleeBoxSizeLeft;
    private Vector2 _meleeBoxSizeRight;

    #endregion

    #region PROPERTY IMPLEMENTATION

    int IMonsterInterface.Health
    {
        get { return this._health; }
        set { this._health = value; }
    }

    float IMonsterInterface.MovingSpeed
    {
        get { return this._movingSpeed; }
        set { this._movingSpeed = value; }
    }

    #region MeleeAttack

    bool IMonsterInterface.isMeleeAttackReady
    {
        get { return this._isMeleeAttackReady; }
        set { this._isMeleeAttackReady = value; }
    }

    int IMonsterInterface.MeleeDamage
    {
        get { return this._meleeDamage; }
        set { this._meleeDamage = value; }
    }

    float IMonsterInterface.MeleeCoolDown
    {
        get { return this._meleeCoolDown; }
        set { this._meleeCoolDown = value; }
    }

    Vector2 IMonsterInterface.MeleeBoxSizeUp
    {
        get { return this._meleeBoxSizeUp; }
        set { this._meleeBoxSizeUp = value; }
    }

    Vector2 IMonsterInterface.MeleeBoxSizeDown
    {
        get { return this._meleeBoxSizedown; }
        set { this._meleeBoxSizedown = value; }
    }

    Vector2 IMonsterInterface.MeleeBoxSizeLeft
    {
        get { return this._meleeBoxSizeLeft; }
        set { this._meleeBoxSizeLeft = value; }
    }

    Vector2 IMonsterInterface.MeleeBoxSizeRight
    {
        get { return this._meleeBoxSizeRight; }
        set { this._meleeBoxSizeRight = value; }
    }

    #endregion

    #region NOT USED

    bool IMonsterInterface.isSkill1AttackReady
    {
        get { return false; }
        set { }
    }

    int IMonsterInterface.Skill1Damage
    {
        get { return -1; }
        set { }
    }

    float IMonsterInterface.Skill1CoolDown
    {
        get { return -1; }
        set { }
    }

    Vector2 IMonsterInterface.Skill1BoxSizeUp
    {
        get { return Vector2.zero; ; }
        set { }
    }

    Vector2 IMonsterInterface.Skill1BoxSizeDown
    {
        get { return Vector2.zero; }
        set { }
    }

    Vector2 IMonsterInterface.Skill1BoxSizeLeft
    {
        get { return Vector2.zero; }
        set { }
    }

    Vector2 IMonsterInterface.Skill1BoxSizeRight
    {
        get { return Vector2.zero; }
        set { }
    }

    bool IMonsterInterface.isSkill2AttackReady
    {
        get { return false; }
        set {  }
    }

    int IMonsterInterface.Skill2Damage
    {
        get { return -1; }
        set { ; }
    }

    float IMonsterInterface.Skill2CoolDown
    {
        get { return -1; }
        set {  }
    }

    Vector2 IMonsterInterface.Skill2BoxSizeUp
    {
        get { return Vector2.zero; }
        set {  }
    }

    Vector2 IMonsterInterface.Skill2BoxSizeDown
    {
        get { return Vector2.zero; }
        set {  }
    }

    Vector2 IMonsterInterface.Skill2BoxSizeLeft
    {
        get { return Vector2.zero; }
        set {  }
    }

    Vector2 IMonsterInterface.Skill2BoxSizeRight
    {
        get { return Vector2.zero; }
        set {  }
    }



    bool IMonsterInterface.isSkill3AttackReady
    {
        get { return false; }
        set { }
    }

    int IMonsterInterface.Skill3Damage
    {
        get { return -1; }
        set { }
    }

    float IMonsterInterface.Skill3CoolDown
    {
        get { return -1; }
        set { }
    }

    Vector2 IMonsterInterface.Skill3BoxSizeUp
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill3BoxSizeDown
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill3BoxSizeLeft
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill3BoxSizeRight
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    bool IMonsterInterface.isSkill4AttackReady
    {
        get { return false; }
        set { }
    }

    int IMonsterInterface.Skill4Damage
    {
        get { return -1; }
        set { }
    }

    float IMonsterInterface.Skill4CoolDown
    {
        get { return -1; }
        set { }
    }

    Vector2 IMonsterInterface.Skill4BoxSizeUp
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill4BoxSizeDown
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill4BoxSizeLeft
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    Vector2 IMonsterInterface.Skill4BoxSizeRight
    {
        get { return new Vector2(0, 0); }
        set { }
    }

    #endregion

    #endregion


    #region METHOD IMPLEMENTATION

    public void AttackMelee(Vector2 dir, Animator anim)
    {
        if (_isMeleeAttackReady == false) return;
        _isMeleeAttackReady = false;

        anim.SetInteger("actionNum", 2);
        //anim.SetTrigger("isMelee");
        anim.SetFloat("actionX", dir.x);
        anim.SetFloat("actionY", dir.y);


        StartCoroutine(WaitAnimationFinish(anim));
        StartCoroutine(CoolDownMelee());

    }

    public IEnumerator CoolDownMelee()
    {
        //Debug.Log("\"CoolDownMelee\" called in GateKeeperClass");
        yield return new WaitForSeconds(this._meleeCoolDown);
        _isMeleeAttackReady = true;
    }

    public bool CheckAnimatorStateName(AnimatorStateInfo stateInfo)
    {
        return (stateInfo.IsName("Melee"));
    }

    public IEnumerator WaitAnimationFinish(Animator anim)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // Wait Attack State
        while (!CheckAnimatorStateName(stateInfo))
        {
            stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }


        // Wait Animation Ends
        while (stateInfo.normalizedTime <= 0.95f)
        {
            stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            //print(stateInfo.normalizedTime);
            yield return null;
        }

        // endAttackMelee
        if (stateInfo.IsName("Melee"))
            GetComponent<MonsterBehaviorManager>().EndAttackMelee();

    }
        #region NOT USED

        public void AttackSkill1(Vector2 dir, Animator anim)
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill2(Vector2 dir, Animator anim)
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill3(Vector2 dir, Animator anim)
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4(Vector2 dir, Animator anim)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator CoolDownSkill1()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator CoolDownSkill2()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator CoolDownSkill3()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator CoolDownSkill4()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    public void DyingEvent()
    {
        throw new System.NotImplementedException();
    }

    public void HitByPlayer(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void Initialize()
    {
        throw new System.NotImplementedException();
    }

    

    #endregion
}
