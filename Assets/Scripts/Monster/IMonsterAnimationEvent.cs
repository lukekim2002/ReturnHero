using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterAnimationEvent {

    void AttackMelee_Ready();
    void AttackMelee_Execute();
    void AttackMelee_End();

    void AttackSkill1_Ready();
    void AttackSkill1_Execute();
    void AttackSkill1_End();

    void AttackSkill2_Ready();
    void AttackSkill2_Execute();
    void AttackSkill2_End();

    void AttackSkill3_Ready();
    void AttackSkill3_Execute();
    void AttackSkill3_End();

    void AttackSkill4_Ready();
    void AttackSkill4_Execute();
    void AttackSkill4_End();
}
