using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAnimationEvent : MonoBehaviour, IMonsterAnimationEvent {

    public GameObject lichAttackEffect;
    public GameObject lichSkillEffect;

    public void AttackMelee_End()
    {
        // Lich attackEffect SetInactive
        lichAttackEffect.SetActive(false);
    }

    public void AttackMelee_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackMelee_Ready()
    {
        // Lich attackEffect SetActive
        lichAttackEffect.SetActive(true);
        //LichAttackEffect.transform.position = HeroGeneralManager.instance.heroObject.transform.position;
    }

    public void AttackSkill1_End()
    {
        lichSkillEffect.SetActive(false);
    }

    public void AttackSkill1_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_Ready()
    {
        lichSkillEffect.SetActive(true);
    }

    public void AttackSkill2_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill2_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill2_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill3_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill3_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill3_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4_Ready()
    {
        throw new System.NotImplementedException();
    }

}
