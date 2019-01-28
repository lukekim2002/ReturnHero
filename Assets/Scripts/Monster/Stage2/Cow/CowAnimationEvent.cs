using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowAnimationEvent : MonoBehaviour, IMonsterAnimationEvent
{
    public GameObject[] Skill1AttackEffect;

    private Vector2 _pos;
    private Vector2 _dir;
    private Vector2 _wallPoint;
    CowClass _behaviour;

    public void AttackMelee_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackMelee_Execute()
    {
        _behaviour = GetComponent<CowClass>();
        _pos = this.transform.position;
        _dir = _behaviour.direction;
        _pos += _dir * 0.16f;
        this.transform.position = _pos;
    }

    public void AttackMelee_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_Execute()
    {
        StartCoroutine(Skill1AttackEffectOn());
    }

    public void AttackSkill1_Ready()
    {
        throw new System.NotImplementedException();
    }


    IEnumerator Skill1AttackEffectOn()
    {
        int i = 0;
        while (i < 3)
        {
            Skill1AttackEffect[i].transform.position = HeroGeneralManager.instance.heroObject.transform.position;
            Skill1AttackEffect[i].SetActive(true);
            i++;

            yield return new WaitForSeconds(1.0f);
        }
    }

    #region NOT USED

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

    #endregion
}
