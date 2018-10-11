using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAbstractClass : MonoBehaviour {

    #region Private
    private const float RightTop = 45f;
    private const float LeftTop = 135f;
    private const float LeftBottom = 225f;
    private const float RightBottom = 315f;

    #endregion

    #region Public
    public enum State { Dead, Alive }
    public enum Action { Idle, Move, Attack }
    public enum LookingDirection { Top, Down, Left, Right }

    /*
    public bool ISAttacking { get; set; }
    public int Health { get; set; }
    public float MovingSpeed { get; set; }

    public int DamageMelee { get; set; }
    public int DamageSkill1 { get; set; }
    public int DamageSkill2 { get; set; }

    public float CoolDownMelee { get; set; }
    public float CoolDownSkill1 { get; set; }
    public float CoolDownSkill2 { get; set; }

    public Vector2 RangeMelee { get; set; }
    public Vector2 RangeSkill1 { get; set; }
    public Vector2 RangeSkill2 { get; set; }

    public Vector2 BoxSizeMelee { get; set; }
    public Vector2 BoxSizeSkill1 { get; set; }
    public Vector2 BoxSizeSkill2 { get; set; }
    */
    #endregion



    /// <summary>
    /// Find Angle Between me and hero.
    /// </summary>
    /// <param name="myPos"></param>
    /// <param name="heroPos"></param>
    /// <returns></returns>
    public LookingDirection FindAngleBetweenHeroAndMe(Vector2 myPos, Vector2 heroPos)
    {
        float angle = Mathf.Atan2(heroPos.y - myPos.y, heroPos.x - myPos.x) * 180 / Mathf.PI;
        if (angle < 0) angle += 360;

        Debug.Log("Angle : " + angle);

        if (angle <= RightTop)              return LookingDirection.Right;
        else if (angle <= LeftTop)          return LookingDirection.Top;
        else if (angle <= LeftBottom)       return LookingDirection.Left;
        else if (angle <= RightBottom)      return LookingDirection.Down;
        else
        {
            return LookingDirection.Right;
        }
        
    }

    public abstract void Initalization();

    public abstract void Attack1();
    public abstract void Attack2();
    public abstract void Attack3();

    public abstract void Skill1();
    public abstract void Skill2();
}
