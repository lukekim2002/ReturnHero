using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAnimationEvent : MonoBehaviour, IMonsterAnimationEvent
{
    private Vector2 _pos;
    private Vector2 _curPos;
    private Vector2 _finalPos;
    private Vector2 _dir;
    private Vector2 _wallPoint;
    EagleClass _behaviour;

    public void AttackMelee_Ready()
    {
        //throw new System.NotImplementedException();
    }

    public void AttackMelee_Execute()
    {
        _behaviour = GetComponent<EagleClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.1f;
        this.transform.position = _pos;
        //throw new System.NotImplementedException();
    }

    public void AttackMelee_End()
    {
        //throw new System.NotImplementedException();
    }

    public void AttackSkill1_Ready()
    {
        
    }

    public void AttackSkill1_Execute()
    {
        _behaviour = GetComponent<EagleClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.1f;
        this.transform.position = _pos;
    }


    public void AttackSkill1_End()
    {
        //throw new System.NotImplementedException();
    }

    public void AttackSkill2_Ready()
    {
        _behaviour = GetComponent<EagleClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.1f;
        this.transform.position = _pos;
    }

    public void AttackSkill2_Execute()
    {
        _behaviour = GetComponent<EagleClass>();
        _pos = this.transform.position;
        _curPos = _finalPos = _pos;
        _dir = _behaviour.myDirection;

        _wallPoint = GameGeneralManager.instance.IsWallInFrontOfCharacter(_pos, _dir, 2.02f);

        Debug.Log(_wallPoint);

        if (_wallPoint != Vector2.zero)
        {
            float magnitude = (_wallPoint - _pos).magnitude;

            _pos += _dir * magnitude;
        }
        else
        {
            _pos += _dir * 2.02f;
        }

        this.transform.position = _pos;
    }

    /*
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _finalPos = transform.position;
        }
    }
    */

    public void AttackSkill2_End()
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
