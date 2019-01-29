﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaAnimationEvent : MonoBehaviour, IMonsterAnimationEvent
{
    private Vector2 _pos;
    private Vector2 _dir;
    private Vector2 _wallPoint;
    GorillaClass _behaviour;

    public void AttackMelee_Ready()
    {
        _behaviour = GetComponent<GorillaClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.14f;
        this.transform.position = _pos;
    }

    public void AttackMelee_Execute()
    {
        _behaviour = GetComponent<GorillaClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;

        _wallPoint = GameGeneralManager.instance.IsWallInFrontOfCharacter(_pos, _dir, 1.4f);

        if (_wallPoint != Vector2.zero)
        {
            float magnitude = (_wallPoint - _pos).magnitude;

            _pos += _dir * magnitude;
        }
        else
        {
            _pos += _dir * 1.4f;
        }

        this.transform.position = _pos;

    }

    public void AttackMelee_End()
    {
        _behaviour = GetComponent<GorillaClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.28f;
        this.transform.position = _pos;
    }

    #region NOT USED

    public void AttackSkill1_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_Ready()
    {
        throw new System.NotImplementedException();
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

    #endregion
}
