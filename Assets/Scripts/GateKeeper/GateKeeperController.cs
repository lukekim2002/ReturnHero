using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class GateKeeperController : MonoBehaviour
{
    #region PRIVATE
    private Animator _gateKeeperAnimator;
    private Rigidbody2D _gateKeeperRigidBody2D;
    #endregion

    #region PUBLIC
    public Vector2 _gateKeeperMovePos = new Vector2(0f, 0f);
    #endregion

    void Start()
    {
        _gateKeeperAnimator = GetComponent<Animator>();
        _gateKeeperRigidBody2D = GetComponent<Rigidbody2D>();
        
        StartCoroutine("RandomDirection");
        StartCoroutine("OnMove");
    }

    #region IEnumerator RandomDirection Related Private Variables
    private Vector2[] _moveDirections = { Vector2.up, Vector2.left, Vector2.right, Vector2.down };
    private int _radnomDirectionNum;
    #endregion

    private IEnumerator RandomDirection()
    {
        while (true)
        {
            // 문지기가 어디 방향으로 움직일 지 결정하기 위한 난수 생성
            _radnomDirectionNum = Random.Range(0, 4);

            Debug.Log(_radnomDirectionNum); 

            // 1.5초 동안 대기하고 실행한다. 이로써 난수를 1.5초마다 생성할 수 있다.
            yield return new WaitForSeconds(1.5f);
        }
    }

    #region IEnumerator OnMove Related Private Variables
    private Vector2 _gateKeeperMoveVelocity;
    private const float gateKeeperMoveSpeed = 1.28f;
    private float _moveSpeed = gateKeeperMoveSpeed;
    #endregion

    private IEnumerator OnMove()
    {
        // 문지기 애니메이터에 Idle -> Move로 넘어가기 위한 조건인 actionNum == 1을 설정한다.
        //_gateKeeperAnimator.SetInteger("actionNum", 1);

        while (true)
        {
            // 문지기의 속력을 계산한다.
            _gateKeeperMoveVelocity = _moveDirections[_radnomDirectionNum].normalized * _moveSpeed;
            // 문지기를 속력 * Time.deltaTime에 맞춰 움직인다.
            _gateKeeperRigidBody2D.MovePosition(_gateKeeperRigidBody2D.position + _gateKeeperMoveVelocity * Time.deltaTime);
            
            // 문지기가 움직이는 방향에 따라 Move 블렌드 트리의 파라미터 값을 바꿔줌
            //_gateKeeperAnimator.SetFloat("moveX", _moveDirections[_radnomDirectionNum].x);
            //_gateKeeperAnimator.SetFloat("moveY", _moveDirections[_radnomDirectionNum].y);

            yield return null;
        }
    }
}
