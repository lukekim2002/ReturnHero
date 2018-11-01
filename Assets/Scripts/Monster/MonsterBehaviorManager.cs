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
    public bool isAttacking = false;
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

        if (myAction == Action.Idle && isAttacking == false)
        {
            animator.SetInteger("actionNum", 0);
        }
        else if (myAction == Action.Move && isAttacking == false)
        {
            animator.SetInteger("actionNum", 1);
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
        }
        /*
        if (myAction != Action.Attack && myMonsterInfo.isSkill2AttackReady == true)
        {
            AttackSkill2Facade();
        }
        */
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
            case 1: // GateKeeper

                myMonsterInfo = gameObject.AddComponent<GateKeeperClass>() as IMonsterInterface;

                myMonsterInfo.Health = 3;
                myMonsterInfo.MeleeCoolDown = 3.0f;
                myMonsterInfo.Skill1CoolDown = 3.0f;
                myMonsterInfo.Skill2CoolDown = 3.0f;

                break;

            case 2: // Zombie

                myMonsterInfo = gameObject.AddComponent<ZombieClass>() as IMonsterInterface;

                myMonsterInfo.Health = 3;
                myMonsterInfo.MeleeCoolDown = 3.0f;

                break;

            case 3: // Skeleton

                myMonsterInfo = gameObject.AddComponent<SkeletonClass>() as IMonsterInterface;

                myMonsterInfo.Health = 3;
                myMonsterInfo.MeleeCoolDown = 3.0f;

                break;

            case 4: // Ghoul

                myMonsterInfo = gameObject.AddComponent<GhoulClass>() as IMonsterInterface;

                myMonsterInfo.Health = 100;
                myMonsterInfo.MeleeCoolDown = 3.0f;

                break;

            case 5: // Ghoul

                myMonsterInfo = gameObject.AddComponent<LichClass>() as IMonsterInterface;

                myMonsterInfo.Health = 3;
                myMonsterInfo.MeleeCoolDown = 3.0f;
                myMonsterInfo.Skill1CoolDown = 3.0f;

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


    public void AttackMeleeFacade()
    {
        myAction = Action.Attack;
        isAttacking = true;
        aiMoveScript.enabled = false;

        //Debug.Log(animator);
        myMonsterInfo.AttackMelee(direction, animator);

        //StartCoroutine(WaitUntilAnimationEnds());
        
    }

    public void EndAttackMelee()
    {
        Debug.Log("\"EndAttackMelee\" is called.");
        myAction = Action.Move;
        animator.SetInteger("actionNum", 1);
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
        animator.ResetTrigger("isMelee");
        isAttacking = false;
        aiMoveScript.enabled = true;
        
    }

    public void AttackSkill1Facade()
    {
        myAction = Action.Attack;
        isAttacking = true;
        aiMoveScript.enabled = false;

        myMonsterInfo.AttackSkill1(direction, animator);
    }

    public void EndAttackSkill1()
    {
        Debug.Log("\"EndAttackSkill1\" is called.");
        myAction = Action.Move;
        animator.SetInteger("actionNum", 1);
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
        animator.ResetTrigger("isSkill1");
        isAttacking = false;
        aiMoveScript.enabled = true;

    }

    public void AttackSkill2Facade()
    {
        myAction = Action.Attack;
        isAttacking = true;
        aiMoveScript.enabled = false;

        myMonsterInfo.AttackSkill2(direction, animator);
    }

    public void EndAttackSkill2()
    {
        Debug.Log("\"EndAttackSkill2\" is called.");
        myAction = Action.Move;
        animator.SetInteger("actionNum", 1);
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
        animator.ResetTrigger("isSkill2");
        isAttacking = false;
        aiMoveScript.enabled = true;
    }

    public void AttackSkill3Facade()
    {
    }

    public void AttackSkill4Facade()
    {
    }

    public void PlayerEnteredRoom() // called When OnTriggerExit() is called in Room
    {
        myAction = Action.Move;
        aiMoveScript.enabled = true;
        StartCoroutine(FindAngleInEvery1Sec());
    }

    public void GetHit(int damage)
    {
        Debug.Log(this.name + " \"GetHit\" is called.");
        myAction = Action.Idle;
        aiMoveScript.enabled = false;
        myMonsterInfo.HitByPlayer(direction, animator, damage);
    }

    public void EndGetHit()
    {
        Debug.Log(this.name + " \"EndGetHit\" is called.");
        myAction = Action.Move;
        animator.SetInteger("actionNum", 1);
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
        aiMoveScript.enabled = true;
    }

    #endregion
}
