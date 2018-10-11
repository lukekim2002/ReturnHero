using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterMethodSet {

    void Initialize();

    void AttackMelee(Vector2 dir);
    void AttackSkill1(Vector2 dir);
    void AttackSkill2(Vector2 dir);
    void AttackSkill3(Vector2 dir);
    void AttackSkill4(Vector2 dir);

    void DyingEvent();
    void HitByPlayer(int damage);
}
