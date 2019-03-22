using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeperAnimationEvent : MonoBehaviour, IMonsterAnimationEvent
{

    private Vector2 _pos;
    private Vector2 _dir;
    private Vector2 _wallPoint;
    GateKeeperClass _behaviour;

    public GameObject[] Skill2AttackEffect;

    int tempTime;

    public void AttackMelee_Ready()
    {
        //throw new System.NotImplementedException();
    }

    public void AttackMelee_Execute()
    {
        _behaviour = GetComponent<GateKeeperClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.16f;
        this.transform.position = _pos;
        //throw new System.NotImplementedException();
    }

    public void AttackMelee_End()
    {
        //throw new System.NotImplementedException();
    }

    public void AttackSkill1_Ready()
    {
        _behaviour = GetComponent<GateKeeperClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.32f;
        this.transform.position = _pos;

        //throw new System.NotImplementedException();
    }

    public void AttackSkill1_Execute()
    {
        _behaviour = GetComponent<GateKeeperClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;

        _wallPoint = GameGeneralManager.instance.IsWallInFrontOfCharacter(_pos, _dir, 4.18f);

        if (_wallPoint != Vector2.zero)
        {
            float magnitude = (_wallPoint - _pos).magnitude;

            _pos += _dir * magnitude;
        }
        else
        {
            _pos += _dir * 4.18f;
        }
        
        this.transform.position = _pos;

        //throw new System.NotImplementedException();
    }

    public void AttackSkill1_End()
    {
        //throw new System.NotImplementedException();
    }

    public void AttackSkill2_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill2_Execute()
    {
        StartCoroutine(Skill2AttackEffectOn());

        //throw new System.NotImplementedException();
    }

    public void AttackSkill2_End()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator Skill2AttackEffectOn()
    {
        int i = 0;
        while (i < 3)
        {
            Skill2AttackEffect[i].transform.position = HeroGeneralManager.instance.heroObject.transform.position;
            Skill2AttackEffect[i].SetActive(true);
            i++;

            yield return new WaitForSeconds(1.0f);
        }
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
