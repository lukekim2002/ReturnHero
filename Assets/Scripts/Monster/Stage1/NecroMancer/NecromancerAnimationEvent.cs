using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerAnimationEvent : MonoBehaviour, IMonsterAnimationEvent
{

    private Vector2 _pos;
    private Vector2 _dir;
    private Vector2 _wallPoint;
    NecromancerClass _behaviour;

    public GameObject[] Skill1AttackEffects;
    int randomSeed;

    public void AttackMelee_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackMelee_Execute()
    {
        _behaviour = GetComponent<NecromancerClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.18f;
        this.transform.position = _pos;
    }

    public void AttackMelee_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_End()
    {
        Skill1AttackEffects[randomSeed].transform.parent = transform;

        Skill1AttackEffects[randomSeed].SetActive(false);
    }

    public void AttackSkill1_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_Ready()
    {
        randomSeed = Random.Range(0, 2);

        Skill1AttackEffects[randomSeed].transform.position = HeroGeneralManager.instance.heroObject.transform.position;
        Skill1AttackEffects[randomSeed].SetActive(true);

        Skill1AttackEffects[randomSeed].transform.parent = Skill1AttackEffects[randomSeed].transform.parent.parent;

        /*
        if (randomSeed == 0)
        {
            Skill1AttackEffects[randomSeed].transform.position = HeroGeneralManager.instance.heroObject.transform.position;
            Skill1AttackEffects[randomSeed].SetActive(true);
        }
        else
        {

        }
        */
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

    #region NOT USED

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

    #endregion
}
