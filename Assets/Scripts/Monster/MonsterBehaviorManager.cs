using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviorManager : MonoBehaviour {

    #region Private
    private const float RightTop = 45f;
    private const float LeftTop = 135f;
    private const float LeftBottom = 225f;
    private const float RightBottom = 315f;


    #endregion

    public enum State { Dead, Alive }
    public enum Action { Idle, Move, Attack }
    public enum LookingDirection { Top, Down, Left, Right }

    public GameObject playerObject;
    public bool isAttacking = false;
    public int attackPriority;
    public Vector2 direction = Vector2.zero; // related with myLookingDirection

    public int monsterUniqueId = -1;
    public State myState;
    public Action myAction;
    public LookingDirection myLookingDirection;
    public IMonsterPropertySet myProperty;
    public IMonsterMethodSet mySkill;


    #region MonoBehavior CallBacks

    private void OnEnable()
    {
        // Initialize at here
        myState = State.Alive;
        myAction = Action.Idle;
        playerObject = HeroGeneralManager.instance.heroObject;

        switch (monsterUniqueId)
        {
            case 0: // GateKeeper
                myProperty = new GateKeeperClass();
                mySkill = new GateKeeperClass();
                break;
            default:
                break;
        }
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(FindAngleInEvery1Sec());

        mySkill.AttackMelee(direction);
        mySkill.AttackSkill1(direction);
        mySkill.AttackSkill2(direction);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        myState = State.Dead;
        myAction = Action.Idle;
    }

    IEnumerator FindAngleInEvery1Sec()
    {
        while (true)
        {
            myLookingDirection = FindAngleBetweenHeroAndMe(this.transform.position, playerObject.transform.position);

            Debug.Log(myLookingDirection);

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


            yield return new WaitForSeconds(0.5f);
        }
    }
    #endregion

    #region Public Methods

    public LookingDirection FindAngleBetweenHeroAndMe(Vector2 myPos, Vector2 heroPos)
    {
        float angle = Mathf.Atan2(heroPos.y - myPos.y, heroPos.x - myPos.x) * 180 / Mathf.PI;
        if (angle < 0) angle += 360;

        Debug.Log("Angle : " + angle);

        if (angle <= RightTop) return LookingDirection.Right;
        else if (angle <= LeftTop) return LookingDirection.Top;
        else if (angle <= LeftBottom) return LookingDirection.Left;
        else if (angle <= RightBottom) return LookingDirection.Down;
        else
        {
            return LookingDirection.Right;
        }

    }


    public void getHit(int damage)
    {
        mySkill.HitByPlayer(damage);
    }
    #endregion
}
