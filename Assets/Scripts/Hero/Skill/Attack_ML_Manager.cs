using UnityEngine;
using System.Collections;

public class Attack_ML_Manager : MonoBehaviour
{
    #region PRIVATE
    private HeroController _heroController;
    private Animator _heroAnimator;
    private int _heroAttackCount = 0;
    private int _heroAttackState = 1;

    private Vector2 colliderSize;
    private Vector2 colliderOffset;
    int i = 0;
    #endregion

    #region PUBLIC
    public GameObject atkCollider;

    // Used for finding child in under order.
    public enum Dir { UP = 0, DOWN = 3, LEFT = 6, RIGHT = 9 };
    public bool isMeeleAttack = false;
    #endregion

    private void Awake()
    {
        _heroController = GetComponent<HeroController>();
        _heroAnimator = GetComponent<Animator>();
    }



    // HeroController에서 마우스 왼쪽 버튼을 누를 때마다 호출
    public void Attack()
    {
        if (_heroAttackCount == 0)
        {
            _heroController.ClickAttackDirection();

            _heroAnimator.SetInteger("attackCount", 1);

            _heroAnimator.SetFloat("actionX", _heroController.direction.x);
            _heroAnimator.SetFloat("actionY", _heroController.direction.y);

            _heroAttackCount++;
        }
        else if (_heroAttackCount == _heroAttackState) // && isAttack == true)
        {
            _heroAttackCount++;
        }
    }

    #region Animation Event Function
    // 공격하면서 움직임
    private void AttackMove()
    {
        Vector2 heroPos = transform.position;
        i = _heroAttackState - 1;

        if (_heroController.direction == Vector2.right)
        {
            i += (int)Dir.RIGHT;

            heroPos.x = heroPos.x + 0.09f;
            this.transform.position = heroPos;
        }
        else if (_heroController.direction == Vector2.left)
        {

            i += (int)Dir.LEFT;

            heroPos.x = heroPos.x - 0.09f;
            this.transform.position = heroPos;
        }
        else if (_heroController.direction == Vector2.up)
        {
            i += (int)Dir.UP;

            heroPos.y = heroPos.y + 0.09f;
            this.transform.position = heroPos;
        }
        else if (_heroController.direction == Vector2.down)
        {
            i += (int)Dir.DOWN;

            heroPos.y = heroPos.y - 0.09f;
            this.transform.position = heroPos;
        }

        colliderSize = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Height"]);
        colliderOffset = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Height"]);
        HeroGeneralManager.instance.SetAttackColliderActive();
        HeroGeneralManager.instance.atkCollider.size = colliderSize;
        HeroGeneralManager.instance.atkCollider.offset = colliderOffset;
    }

    // 다음 연타 모션을 취할 지 판단.
    private void AttackTiming()
    {
        if (_heroAttackState + 1 == _heroAttackCount)
        {
            _heroAttackState++;
            _heroAnimator.SetInteger("attackCount", _heroAttackState);
        }
    }
    
    // 공격 애니메이션이 끝나면 실행
    private void AttackEnd()
    {
        HeroController.heroState = HeroController.HEROSTATE.IDLE;

        _heroAttackState = 1;
        _heroAttackCount = 0;
        _heroAnimator.SetInteger("attackCount", 0);
        _heroAnimator.SetInteger("actionNum", 0);

        _heroAnimator.SetFloat("actionX", _heroController.direction.x);
        _heroAnimator.SetFloat("actionY", _heroController.direction.y);

        HeroGeneralManager.instance.SetAttackColliderInactive();

        // 어택하고 나서 남은 moveAxis 값을 0으로 되돌려줘야 다음 Move가 어색하지 않음.
        _heroController.moveAxis = Vector2.zero;

        isMeeleAttack = false;

    }
    #endregion
}
