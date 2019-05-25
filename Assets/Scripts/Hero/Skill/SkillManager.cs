using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    #region PRIVATE
    #endregion

    #region PUBLIC
    public ISkillInterface[] ISkillWeapon = new ISkillInterface[] { new ISkill_WeaponA()
        , new ISkill_WeaponB(), new ISkill_WeaponC(), new ISkill_WeaponD(), new ISkill_WeaponE() };
    public ISkillInterface currentWeaponSkill;
    #endregion

    private void Start()
    {
        currentWeaponSkill = ISkillWeapon[0];
    }

    public void ISkillInterface_Skill_MR_Ready()
    {
        currentWeaponSkill.Skill_MR_Ready();
    }

    public void ISkillInterface_Skill_MR_Execute()
    {
        currentWeaponSkill.Skill_MR_Execute();
    }

    public void ISkillInterface_SKill_MR_EffectOn()
    {
        currentWeaponSkill.Skill_MR_EffectOn();
    }

    public void ISkillInterface_Skill_MR_End()
    {
        currentWeaponSkill.Skill_MR_End();
    }

    public void ISkillInterface_Skill_E_Ready()
    {
        currentWeaponSkill.Skill_E_Ready();
    }

    public void ISkillInterface_Skill_E_Execute()
    {
        currentWeaponSkill.Skill_E_Execute();
    }

    public void ISkillInterface_SKill_E_EffectOn()
    {
        currentWeaponSkill.Skill_E_EffectOn();
    }

    public void ISkillInterface_Skill_E_End()
    {
        currentWeaponSkill.Skill_E_End();
    }

    public void ISkillInterface_Skill_R_Ready()
    {
        currentWeaponSkill.Skill_R_Ready();
    }

    public void ISkillInterface_Skill_R_Execute()
    {
        currentWeaponSkill.Skill_R_Execute();
    }

    public void ISkillInterface_SKill_R_EffectOn()
    {
        currentWeaponSkill.Skill_R_EffectOn();
    }

    public void ISkillInterface_Skill_R_End()
    {
        currentWeaponSkill.Skill_R_End();
    }
}
