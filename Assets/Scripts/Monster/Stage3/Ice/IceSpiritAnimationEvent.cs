using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpiritAnimationEvent : MonoBehaviour
{
    private Vector2 _pos;
    private Vector2 _dir;
    private Vector2 _wallPoint;
    IceSpiritClass _behaviour;

    public GameObject myAttackSpawnSet;     // 4방향 생성포인트를 저장한 게임오브젝트셋
    public GameObject[] myArrowSet;         // 4방향 화살 오브젝트

    public void AttackMelee_Ready()
    {
        _behaviour = GetComponent<IceSpiritClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.14f;
        this.transform.position = _pos;

    }

    public void AttackMelee_Execute()
    {
        int index = 0;
        _behaviour = GetComponent<IceSpiritClass>();

        // 방향에 따른 인덱스 초기화. -> myArrowSet의 child 순서가 됨.
        if (_behaviour.myDirection == Vector2.up) index = 0;
        else if (_behaviour.myDirection == Vector2.down) index = 1;
        else if (_behaviour.myDirection == Vector2.left) index = 2;
        else if (_behaviour.myDirection == Vector2.right) index = 3;

        // 스폰 오브젝트셋을 실행 시점의 히어로 좌표로 보냄.
        myAttackSpawnSet.transform.position = HeroGeneralManager.instance.heroObject.transform.position;
        // 4방향 화살 중 하나를 생성하고 obj라는 임시 오브젝트에 저장하여 참조 가능하게 함.
        GameObject obj = (GameObject)Instantiate(myArrowSet[index], myAttackSpawnSet.transform.GetChild(index).transform.position, Quaternion.identity);

        // 데미지와 날라갈 방향을 결정해준다.
        obj.GetComponent<IceSpiritProjectile>().damage = _behaviour._meleeDamage;
        obj.GetComponent<IceSpiritProjectile>()._dir = _behaviour.myDirection;
    }

    public void AttackMelee_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_Ready()
    {
        _behaviour = GetComponent<IceSpiritClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.14f;
        this.transform.position = _pos;
    }

    

    public void AttackSkill1_Execute()
    {
        int index = 0;
        _behaviour = GetComponent<IceSpiritClass>();
        myAttackSpawnSet.transform.position = HeroGeneralManager.instance.heroObject.transform.position;

        for (; index < 4; index++)
        {
            GameObject obj = (GameObject)Instantiate(myArrowSet[index], myAttackSpawnSet.transform.GetChild(index).transform.position, Quaternion.identity);
            obj.GetComponent<IceSpiritProjectile>().damage = _behaviour._skill1Damage;

            // 4개가 동시에 나가야하지만 방향은 각기 달라야함.
            // 분기 처리밖에 생각 안났는데 더 좋은 방법이 있지 않을까 싶다
            Vector2 arrowDir = new Vector2(0,0);
            switch (index)
            {
                case 0:
                    arrowDir = Vector2.up;
                    break;
                case 1:
                    arrowDir = Vector2.down;
                    break;
                case 2:
                    arrowDir = Vector2.left;
                    break;
                case 3:
                    arrowDir = Vector2.right;
                    break;
            }

            obj.GetComponent<IceSpiritProjectile>()._dir = arrowDir;
        }
    }

    public void AttackSkill1_End()
    {
        throw new System.NotImplementedException();
    }

    #region NOT USED



    public void AttackSkill2_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill2_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill2_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill3_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill3_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill3_Ready()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill4_Ready()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
