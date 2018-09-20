using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    #region PRIVATE

    private Attack_ML_Manager _attack_ML_Manager;
    private Animator _heroAnimator;
    private Rigidbody2D _heroRigidbody;
    private float moveSpeed = 1.28f;
    
    [SerializeField]
    private bool _isAttack = false;
    #endregion

    #region PUBLIC
    public enum HEROSTATE { IDLE, MOVE, ATTACK, DEFENSE, DASH };
    static public HEROSTATE heroState = HEROSTATE.IDLE;


    public Vector2 moveAxis;
    public Vector2 direction;

    #endregion

    private void Awake()
    {
        _heroAnimator = GetComponent<Animator>();
        _attack_ML_Manager = GetComponent<Attack_ML_Manager>();
        _heroRigidbody = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        moveAxis = Vector2.zero;
        direction = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameGeneralManager.isPause)
        {
            // 공격 상태가 아니라면
            if (heroState == HEROSTATE.IDLE || heroState == HEROSTATE.MOVE || heroState == HEROSTATE.DEFENSE)
            {
                // 키 입력
                InputKey();
            }

            // Idle 상태
            if (heroState == HEROSTATE.IDLE)
            {
                _heroAnimator.SetInteger("actionNum", 0);

                // Idle 애니메이션으로 넘어갈 때 Idle할 방향 지정
                _heroAnimator.SetFloat("actionX", direction.x);
                _heroAnimator.SetFloat("actionY", direction.y);
            }
            // Move 상태
            else if (heroState == HEROSTATE.MOVE)
            {
                _heroAnimator.SetInteger("actionNum", 1);

                Vector2 heroPos = moveAxis;
                Vector2 moveVelocity = (heroPos.normalized) * moveSpeed;

                // velocity, transform.translate, addforce, MovePosition중에서 MovePosition이 벽과의 충돌에서 자연스러운 모습을 보임
                // 캐릭터 움직임
                _heroRigidbody.MovePosition(_heroRigidbody.position + moveVelocity * Time.deltaTime);

                // 캐릭터가 움직일 방향 애니 지정
                _heroAnimator.SetFloat("moveX", moveAxis.x);
                _heroAnimator.SetFloat("moveY", moveAxis.y);

                // 마지막으로 캐릭터가 움직인 방향을 저장
                direction = moveAxis;
            }

            // Attack 상태 및 기본공격을 눌렀다면 실행하고 누르지 않았다면 실행 불가능
            else if (heroState == HEROSTATE.ATTACK && _attack_ML_Manager.isMeeleAttack == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _attack_ML_Manager.Attack();
                }
            }
            else if (heroState == HEROSTATE.DEFENSE)
            {
                _heroAnimator.SetInteger("actionNum", 3);

                _heroAnimator.SetFloat("actionX", direction.x);
                _heroAnimator.SetFloat("actionY", direction.y);
            }
            else if (heroState == HEROSTATE.DASH)
            {
                _heroAnimator.SetInteger("actionNum", 4);

                _heroAnimator.SetFloat("actionX", direction.x);
                _heroAnimator.SetFloat("actionY", direction.y);
            }
        }
    }

    // WASD 입력 처리
    private void InputKey()
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region 이동 키 입력 처리
        // S키
        if (Input.GetKey(KeyCode.S))
        {
            // 아래 방향
            moveAxis.y = -1;
            heroState = HEROSTATE.MOVE;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            moveAxis.y = 0;
        }
        // W키
        if (Input.GetKey(KeyCode.W))
        {
            // 위쪽 방향
            moveAxis.y = 1;
            heroState = HEROSTATE.MOVE;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            moveAxis.y = 0;
        }
        // D키
        if (Input.GetKey(KeyCode.D))
        {
            // 오른쪽 방향
            moveAxis.x = 1;
            heroState = HEROSTATE.MOVE;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            moveAxis.x = 0;
        }
        // A키
        if (Input.GetKey(KeyCode.A))
        {
            // 왼쪽 방향
            moveAxis.x = -1;
            heroState = HEROSTATE.MOVE;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            moveAxis.x = 0;
        }

        // 캐릭터가 움직이지 않는 상태라면
        if (moveAxis == Vector2.zero)
        {
            // IDLE 상태로 전환
            heroState = HEROSTATE.IDLE;
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region 방어 키 입력 처리
        if (Input.GetKey(KeyCode.Q))
        {
            heroState = HEROSTATE.DEFENSE;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            heroState = HEROSTATE.IDLE;
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region E키 입력 처리
        if (Input.GetKeyDown(KeyCode.E))
        {
            heroState = HEROSTATE.ATTACK;
            _isAttack = true;

            _heroAnimator.SetInteger("actionNum", 2);
            _heroAnimator.SetTrigger("isSkillE");

            ClickAttackDirection();

            _heroAnimator.SetFloat("actionX", direction.x);
            _heroAnimator.SetFloat("actionY", direction.y);

        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region R키 입력 처리
        if (Input.GetKeyDown(KeyCode.R) && Skill_R_EffectManager.isSkillR == false)
        {
            heroState = HEROSTATE.ATTACK;
            _isAttack = true;

            _heroAnimator.SetInteger("actionNum", 2);
            _heroAnimator.SetTrigger("isSkillR");

            ClickAttackDirection();

            _heroAnimator.SetFloat("actionX", direction.x);
            _heroAnimator.SetFloat("actionY", direction.y);
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region 대쉬 키 입력 처리
        if (Input.GetKeyDown(KeyCode.Space))
        {
            heroState = HEROSTATE.DASH;
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region 기본 3단 공격 입력 처리

        if (Input.GetMouseButtonDown(0) && _attack_ML_Manager.isMeeleAttack == false)
        {
            heroState = HEROSTATE.ATTACK;

            _heroAnimator.SetInteger("actionNum", 2);
            _isAttack = true;
            _attack_ML_Manager.isMeeleAttack = true;
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region 기본 스킬 공격 입력 처리
        // 스킬 이펙트가 끝난 후에야 다시 누를 수 있다.
        if (Input.GetMouseButtonDown(1) && Skill_MR_EffectManager.isSkillMr == false)
        {
            heroState = HEROSTATE.ATTACK;

            _heroAnimator.SetInteger("actionNum", 2);
            _heroAnimator.SetTrigger("isSkillMr");

            _isAttack = true;

            ClickAttackDirection();

            _heroAnimator.SetFloat("actionX", direction.x);
            _heroAnimator.SetFloat("actionY", direction.y);
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    public void ClickAttackDirection()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray2D ray2D = new Ray2D(cursorPos, Vector2.zero);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(ray2D.origin, ray2D.direction);

        // 클릭한 곳을 바라보면서 공격하는 코드
        if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.name.Equals("Top"))
        {
            if (_isAttack)
            {
                direction = Vector2.up;
            }

            if (!Skill_MR_EffectManager.isSkillMr)
            {
                Skill_MR_EffectManager.attackDirection = Skill_MR_EffectManager.ATTACKDIRECTION.UP;
            }

            if (!Skill_R_EffectManager.isSkillR)
            {
                Skill_R_EffectManager.attackDirection = Skill_R_EffectManager.ATTACKDIRECTION.UP;
            }
        }
        else if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.name.Equals("Bottom"))
        {
            if (_isAttack)
            {
                direction = Vector2.down;
            }

            if (!Skill_MR_EffectManager.isSkillMr)
            {
                Skill_MR_EffectManager.attackDirection = Skill_MR_EffectManager.ATTACKDIRECTION.DOWN;
            }

            if (!Skill_R_EffectManager.isSkillR)
            {
                Skill_R_EffectManager.attackDirection = Skill_R_EffectManager.ATTACKDIRECTION.DOWN;
            }
        }
        else if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.name.Equals("Right"))
        {
            if (_isAttack)
            {
                direction = Vector2.right;
            }

            if (!Skill_MR_EffectManager.isSkillMr)
            {
                Skill_MR_EffectManager.attackDirection = Skill_MR_EffectManager.ATTACKDIRECTION.RIGHT;
            }

            if (!Skill_R_EffectManager.isSkillR)
            {
                Skill_R_EffectManager.attackDirection = Skill_R_EffectManager.ATTACKDIRECTION.RIGHT;
            }
        }
        else if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.name.Equals("Left"))
        {
            if (_isAttack)
            {
                direction = Vector2.left;
            }

            if (!Skill_MR_EffectManager.isSkillMr)
            {
                Skill_MR_EffectManager.attackDirection = Skill_MR_EffectManager.ATTACKDIRECTION.LEFT;
            }

            if (!Skill_R_EffectManager.isSkillR)
            {
                Skill_R_EffectManager.attackDirection = Skill_R_EffectManager.ATTACKDIRECTION.LEFT;
            }
        }
    }



    

}