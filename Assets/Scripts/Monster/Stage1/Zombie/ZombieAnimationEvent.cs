using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationEvent : MonoBehaviour, IMonsterAnimationEvent
{
    private Vector2 _pos;
    private Vector2 _dir;
    private Vector2 _wallPoint;
    ZombieClass _behaviour;

    public void AttackMelee_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackMelee_Execute()
    {
        _behaviour = GetComponent<ZombieClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.2f; // 0.2적ㅇ면 되냐? 아니면 구해야하냐/
        this.transform.position = _pos;
    }

    public void AttackMelee_End()
    {
        throw new System.NotImplementedException();
    }

    #region NOT USED

    public void AttackSkill1_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_End()
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
