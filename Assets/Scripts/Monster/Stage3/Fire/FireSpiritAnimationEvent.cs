using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritAnimationEvent : MonoBehaviour
{
    private Vector2 _pos;
    private Vector2 _dir;
    private Vector2 _wallPoint;
    FireSpiritClass _behaviour;

    public GameObject skillEffect;
    public GameObject spawnPos;

    public void AttackMelee_Execute()
    {
        _behaviour = GetComponent<FireSpiritClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.14f;
        this.transform.position = _pos;

    }

    public void AttackSkill1_Ready()
    {
        _behaviour = GetComponent<FireSpiritClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.14f;
        this.transform.position = _pos;
    }

    public void AttackSkill1_Execute()
    {
        int index = 0;
        _behaviour = GetComponent<FireSpiritClass>();

        if (_behaviour.myDirection == Vector2.up) index = 0;
        else if (_behaviour.myDirection == Vector2.down) index = 1;
        else if (_behaviour.myDirection == Vector2.left) index = 2;
        else if (_behaviour.myDirection == Vector2.right) index = 3;

        GameObject dirSet = spawnPos.transform.GetChild(index).gameObject;
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(i + " repeatition");
            Instantiate(skillEffect, dirSet.transform.GetChild(i).transform.position, Quaternion.identity);
        }
    }
}
