using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterInterface {

    #region Property
    int Health { get; set; }
    float MovingSpeed { get; set; }

    bool isMeleeAttackReady { get; set; }
    int MeleeDamage { get; set; }
    float MeleeCoolDown { get; set; }
    Vector2 MeleeBoxSizeUp { get; set; }
    Vector2 MeleeBoxSizeDown { get; set; }
    Vector2 MeleeBoxSizeLeft { get; set; }
    Vector2 MeleeBoxSizeRight { get; set; }

    bool isSkill1AttackReady { get; set; }
    int Skill1Damage { get; set; }
    float Skill1CoolDown { get; set; }
    Vector2 Skill1BoxSizeUp { get; set; }
    Vector2 Skill1BoxSizeDown { get; set; }
    Vector2 Skill1BoxSizeLeft { get; set; }
    Vector2 Skill1BoxSizeRight { get; set; }

    bool isSkill2AttackReady { get; set; }
    int Skill2Damage { get; set; }
    float Skill2CoolDown { get; set; }
    Vector2 Skill2BoxSizeUp { get; set; }
    Vector2 Skill2BoxSizeDown { get; set; }
    Vector2 Skill2BoxSizeLeft { get; set; }
    Vector2 Skill2BoxSizeRight { get; set; }

    bool isSkill3AttackReady { get; set; }
    int Skill3Damage { get; set; }
    float Skill3CoolDown { get; set; }
    Vector2 Skill3BoxSizeUp { get; set; }
    Vector2 Skill3BoxSizeDown { get; set; }
    Vector2 Skill3BoxSizeLeft { get; set; }
    Vector2 Skill3BoxSizeRight { get; set; }

    bool isSkill4AttackReady { get; set; }
    int Skill4Damage { get; set; }
    float Skill4CoolDown { get; set; }
    Vector2 Skill4BoxSizeUp { get; set; }
    Vector2 Skill4BoxSizeDown { get; set; }
    Vector2 Skill4BoxSizeLeft { get; set; }
    Vector2 Skill4BoxSizeRight { get; set; }
    #endregion

    #region Methods
    void Initialize();

    void AttackMelee(Vector2 dir, Animator anim);
    void AttackSkill1(Vector2 dir, Animator anim);
    void AttackSkill2(Vector2 dir, Animator anim);
    void AttackSkill3(Vector2 dir, Animator anim);
    void AttackSkill4(Vector2 dir, Animator anim);

    IEnumerator CoolDownMelee();
    IEnumerator CoolDownSkill1();
    IEnumerator CoolDownSkill2();
    IEnumerator CoolDownSkill3();
    IEnumerator CoolDownSkill4();
    IEnumerator WaitAnimationFinish(Animator anim);

    void DyingEvent();
    void HitByPlayer(int damage);
    #endregion
}
