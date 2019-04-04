using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpiritProjectile : MonoBehaviour
{
    public int damage = 10;

    private Vector2 _pos;
    public Vector2 _dir;

    public void Move1() // move 4px
    {
        _pos = this.transform.position;
        _pos += _dir * 0.04f;
        this.transform.position = _pos;
    }

    public void Move2() // move 50px
    {
        _pos = this.transform.position;
        _pos += _dir * 0.5f;
        this.transform.position = _pos;
    }

    public void Move3() // move 8x
    {
        _pos = this.transform.position;
        _pos += _dir * 0.08f;
        this.transform.position = _pos;
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
