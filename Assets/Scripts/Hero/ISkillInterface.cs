using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillInterface
{
    void Skill_E_Ready();
    void Skill_E_Execute();
    void Skill_E_EffectOn();
    void Skill_E_End();

    void Skill_MR_Ready();
    void Skill_MR_Execute();
    void Skill_MR_EffectOn();
    void Skill_MR_End();

    void Skill_R_Ready();
    void Skill_R_Execute();
    void Skill_R_EffectOn();
    void Skill_R_End();
}
