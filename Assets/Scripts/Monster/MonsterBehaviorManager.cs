using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviorManager : MonoBehaviour {

    #region Private
    private const float RightTop = 45f;
    private const float LeftTop = 135f;
    private const float LeftBottom = 225f;
    private const float RightBottom = 315f;

    private const float delayForSettingDirection = 0.5f;

    #endregion

    public enum State { Dead, Alive }
    public enum Action { Idle, Move, Attack }
    public enum LookingDirection { Top, Down, Left, Right }

    public int monsterUniqueId = -1;                // Used for get which class object
    public IMonsterInterface myMonsterInfo;
    public List<Dictionary<string, object>> monsterPropertySet;
    public List<Dictionary<string, object>> monsterAttackColliderSet;
    public Pathfinding.AIPath aiMoveScript;
    public BoxCollider2D attackCollider;
    public Animator animator;

    public GameObject playerObject;
    //public bool isAttacking = false;
    public Vector2 direction = Vector2.zero;        // related with myLookingDirection

    public State myState;                           // Dead, Alive
    public Action myAction;                         // Idle, Move, Attack
    public LookingDirection myLookingDirection;     // Top, Down, Left, Right

    

    #region MonoBehavior CallBacks

    private void OnEnable()
    {
        // Initialize at here
        myState = State.Alive;
        myAction = Action.Idle;

        playerObject = HeroGeneralManager.instance.heroObject;
        aiMoveScript = GetComponent<Pathfinding.AIPath>();
        attackCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        //print(playerObject);
        print(animator);

        Initialize();

        aiMoveScript.enabled = false;
        
    }

    // Use this for initialization
    void Start ()
    {
        PlayerEnteredRoom();

    }
	
	// Update is called once per frame
	void Update ()
    {

        //myMonsterInfo.AttackMelee(direction, animator);

        if (myAction == Action.Idle)
        {
            animator.SetInteger("actionNum", 0);
        }
        else if (myAction == Action.Move)
        {
            //ChangeAnimationWhileMoving();
            animator.SetInteger("actionNum", 1);
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveX", direction.y);
        }

    }

    private void OnDisable()
    {
        myState = State.Dead;
        myAction = Action.Idle;

        //aiMoveScript.enabled = false;
    }
    #endregion

    #region Coroutines

    IEnumerator FindAngleInEvery1Sec()
    {
        while (true)
        {
            myLookingDirection = FindAngleBetweenHeroAndMe(this.transform.position, playerObject.transform.position);

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

            yield return new WaitForSeconds(delayForSettingDirection);
        }
    }

    #endregion



    #region Public Methods

    public LookingDirection FindAngleBetweenHeroAndMe(Vector2 myPos, Vector2 heroPos)
    {
        float angle = Mathf.Atan2(heroPos.y - myPos.y, heroPos.x - myPos.x) * 180 / Mathf.PI;
        if (angle < 0) angle += 360;

        //Debug.Log("Angle : " + angle);

        if (angle <= RightTop) return LookingDirection.Right;
        else if (angle <= LeftTop) return LookingDirection.Top;
        else if (angle <= LeftBottom) return LookingDirection.Left;
        else if (angle <= RightBottom) return LookingDirection.Down;
        else
        {
            return LookingDirection.Right;
        }

    }

    public void Initialize()
    {

        switch (monsterUniqueId)
        {
            case 0: // GateKeeper

                myMonsterInfo = gameObject.AddComponent<GateKeeperClass>() as IMonsterInterface;

                //print(myProperty);
                //print(mySkill);

                myMonsterInfo.Health = 3;
                myMonsterInfo.MeleeCoolDown = 3.0f;
                myMonsterInfo.Skill1CoolDown = 15.0f;
                myMonsterInfo.Skill2CoolDown = 15.0f;

                break;

            default: // Error
                //throw new System.ArgumentOutOfRangeException("monsterUniqueId", "Invalid Value");
                break;
        }

        myMonsterInfo.isMeleeAttackReady = true;
        myMonsterInfo.isSkill1AttackReady = true;
        myMonsterInfo.isSkill2AttackReady = true;
        myMonsterInfo.isSkill3AttackReady = true;
        myMonsterInfo.isSkill4AttackReady = true;
    }

    public void ChangeAnimationWhileMoving()
    {
        if (myAction == Action.Move)
        {
            switch (myLookingDirection)
            {
                case LookingDirection.Top:

                    animator.SetInteger("actionNum", 1);
                    animator.SetFloat("moveX", direction.x);
                    animator.SetFloat("moveX", direction.y);
                    break;

                case LookingDirection.Down:

                    animator.SetInteger("actionNum", 1);
                    break;

                case LookingDirection.Left:

                    animator.SetInteger("actionNum", 1);
                    break;

                case LookingDirection.Right:

                    animator.SetInteger("actionNum", 1);
                    break;
            }
        }
    }

    public void PlayerEnteredRoom() // called When OnTriggerExit() is called in Room
    {
        myAction = Action.Move;
        aiMoveScript.enabled = true;
        StartCoroutine(FindAngleInEvery1Sec());
    }

    public void getHit(int damage)
    {
        myMonsterInfo.HitByPlayer(damage);
    }
    #endregion
}
