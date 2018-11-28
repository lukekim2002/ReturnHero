using UnityEngine;
using System.Collections;

public class Skill_Space_Manager : MonoBehaviour
{
    #region PRIVATE
    private HeroController _heroController;
    private Animator _heroAnimator;

    private RaycastHit2D _hitWall;
    private const float _dashDist = 1.6f;
    private const float _dashMoveDist = 0.09f;
    private Vector2 _colliderSize;
    private float _colliderDiagonalLength;
    #endregion

    #region PUBLIC
    #endregion

    // Use this for initialization
    void Start()
    {
        _heroController = GetComponent<HeroController>();
        _heroAnimator = GetComponent<Animator>();

        _colliderSize = HeroGeneralManager.instance.colliderSize;
        _colliderDiagonalLength = HeroGeneralManager.instance.colliderDiagonalLength;
    }


    private void DashMove()
    {
        Vector2 direction = Vector2.zero;
        Vector2 heroPos = transform.position;
        Vector2 hitPoint;

        direction = _heroController.direction;

        hitPoint = GameGeneralManager.instance.IsWallInFrontOfCharacter(heroPos, direction, _dashDist);
        //print(hitPoint);

        if (hitPoint != Vector2.zero)
        {
            float magnitude = (hitPoint - heroPos).magnitude;

            if (magnitude <= _colliderDiagonalLength)
                direction = Vector2.zero;

            direction = direction * (magnitude - _colliderSize.magnitude);
        }
        else
        {
            direction = direction * _dashDist;
        }
        
        heroPos += direction;
        this.transform.position = heroPos;
    }

    private void Dash_Excute()
    {
        Vector2 direction = Vector2.zero;
        Vector2 heroPos = transform.position;
        Vector2 hitPoint;

        direction = _heroController.direction;

        hitPoint = GameGeneralManager.instance.IsWallInFrontOfCharacter(heroPos, direction, _dashMoveDist);
        //print(hitPoint);

        if (hitPoint != Vector2.zero)
        {
            float magnitude = (hitPoint - heroPos).magnitude;

            if (magnitude <= _colliderDiagonalLength)
                direction = Vector2.zero;

            direction = direction * (magnitude - _colliderSize.magnitude);
        }
        else
        {
            direction = direction * _dashMoveDist;
        }

        heroPos += direction;
        this.transform.position = heroPos;

    }

    private void DashEnd()
    {
        HeroController.heroState = HeroController.HEROSTATE.IDLE;

        _heroAnimator.SetInteger("actionNum", 0);

        _heroAnimator.SetFloat("actionX", _heroController.direction.x);
        _heroAnimator.SetFloat("actionY", _heroController.direction.y);

        // 대쉬하고 나서 남은 moveAxis 값을 0으로 되돌려줘야 다음 Move가 어색하지 않음.
        _heroController.moveAxis = Vector2.zero;
    }
}
