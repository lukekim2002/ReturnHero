using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour {

    #region PRIVATE

    private const float RIGHT_TOP = 45f;
    private const float LEFT_TOP = 135f;
    private const float LEFT_BOTTOM = 225f;
    private const float RIGHT_BOTTOM = 315f;

    private const float findingDirectionDelayTime = 0.3f;

    #endregion

    #region PUBLIC

    //public enum State { Dead, Alive }
    public enum Action { Idle, Move, Attack }
    public enum LookingDirection { Top, Down, Left, Right }

    //public State myState;                           // Dead, Alive
    //public Action myAction;                         // Idle, Move, Attack
    public LookingDirection myLookingDirection;     // Top, Down, Left, Right

    public Vector2 direction = Vector2.zero;
    public GameObject playerObject;

    #endregion

    #region PUBLIC FUNCTION

    private void OnEnable()
    {
        playerObject = HeroGeneralManager.instance.heroObject;
    }

    public IEnumerator FindLookingDirection()
    {
        if (playerObject == null)
        {
            playerObject = HeroGeneralManager.instance.heroObject;
        }
        else
        {
            while (true)
            {
                //if (playerObject == null) continue;
                myLookingDirection = FindAngleBetweenHeroAndMe(transform.position, playerObject.transform.position);

                //Debug.Log(myLookingDirection);

                switch (myLookingDirection)
                {
                    case LookingDirection.Top:
                        direction = Vector2.up;
                        break;
                    case LookingDirection.Down:
                        direction = Vector2.down;
                        break;
                    case LookingDirection.Left:
                        direction = Vector2.left;
                        break;
                    case LookingDirection.Right:
                        direction = Vector2.right;
                        break;
                    default:
                        direction = Vector2.zero;
                        break;

                }

                yield return new WaitForSeconds(findingDirectionDelayTime);
            }
        }
    }

    public LookingDirection FindAngleBetweenHeroAndMe(Vector2 myPos, Vector2 heroPos)
    {
        float angle = Mathf.Atan2(heroPos.y - myPos.y, heroPos.x - myPos.x) * 180 / Mathf.PI;
        if (angle < 0) angle += 360;

        //Debug.Log("Angle : " + angle);

        if (angle <= RIGHT_TOP) return LookingDirection.Right;
        else if (angle <= LEFT_TOP) return LookingDirection.Top;
        else if (angle <= LEFT_BOTTOM) return LookingDirection.Left;
        else if (angle <= RIGHT_BOTTOM) return LookingDirection.Down;
        else
        {
            return LookingDirection.Right;
        }

    }

    #endregion

    #region ABSTRACT

    public abstract void Initialize();

    public abstract void AttackMelee();
    public abstract void AttackSkill1();
    public abstract void AttackSkill2();
    public abstract void AttackSkill3();
    public abstract void AttackSkill4();

    public abstract void EndAttackMelee();
    public abstract void EndAttackSkill1();
    public abstract void EndAttackSkill2();
    public abstract void EndAttackSkill3();
    public abstract void EndAttackSkill4();

    public abstract IEnumerator CoolDownMelee();
    public abstract IEnumerator CoolDownSkill1();
    public abstract IEnumerator CoolDownSkill2();
    public abstract IEnumerator CoolDownSkill3();
    public abstract IEnumerator CoolDownSkill4();

    public abstract bool CheckAnimatorStateName(AnimatorStateInfo stateInfo);
    public abstract IEnumerator WaitAnimationFinish();

    public abstract void DyingMotion();
    public abstract void HitByPlayer(int damage);
    public abstract void EndGetHit();

    #endregion
}
