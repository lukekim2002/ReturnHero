using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    #region PRIVATE
    public ISkill_WeaponB skill_WeaponB;
    public ISkill_WeaponC skill_WeaponC;
    #endregion

    #region PUBLIC
    public static SkillManager instance;
    public ISkillInterface currentWeaponSkill;
    public ISkillInterface[] ISkillWeapon = new ISkillInterface[] { new ISkill_Sword(), new ISkill_WeaponB(), new ISkill_WeaponC() };
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        currentWeaponSkill = ISkillWeapon[2];
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
