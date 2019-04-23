using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationEvent : MonoBehaviour, IMonsterAnimationEvent {

    public GameObject skeletonProjectile;

    private Vector2 _pos;
    private Vector2 _dir;
    private Vector2 _wallPoint;
    SkeletonClass _behaviour;


    public void AttackMelee_End()
    {
        //this.GetComponent<Animator>().SetInteger("actionNum", 0);
    }

    public void AttackMelee_Execute()
    {
        // 스켈레톤 투사체 SetActive
        GameObject projectile = Instantiate(skeletonProjectile, transform.position, Quaternion.identity);
        projectile.GetComponent<SkeletonProjectile>().myDir = transform.GetComponent<SkeletonClass>().myDirection;

        #region Projectile Collider Setting

        if (_dir == Vector2.up)
        {
            projectile.GetComponent<SkeletonProjectile>().myColliderSize = new Vector2((float)_behaviour.myColliderSet[4]["Size_x"], (float)_behaviour.myColliderSet[4]["Size_y"]);
            projectile.GetComponent<SkeletonProjectile>().myColliderOffset = new Vector2((float)_behaviour.myColliderSet[4]["Offset_x"], (float)_behaviour.myColliderSet[4]["Offset_y"]);
            projectile.GetComponent<Animator>().SetTrigger("isSkeletonAttackUp");
        }
        else if (_dir == Vector2.down)
        {
            projectile.GetComponent<SkeletonProjectile>().myColliderSize = new Vector2((float)_behaviour.myColliderSet[5]["Size_x"], (float)_behaviour.myColliderSet[5]["Size_y"]);
            projectile.GetComponent<SkeletonProjectile>().myColliderOffset = new Vector2((float)_behaviour.myColliderSet[5]["Offset_x"], (float)_behaviour.myColliderSet[5]["Offset_y"]);
            projectile.GetComponent<Animator>().SetTrigger("isSkeletonAttackDown");
        }
        else if (_dir == Vector2.left)
        {
            projectile.GetComponent<SkeletonProjectile>().myColliderSize = new Vector2((float)_behaviour.myColliderSet[6]["Size_x"], (float)_behaviour.myColliderSet[6]["Size_y"]);
            projectile.GetComponent<SkeletonProjectile>().myColliderOffset = new Vector2((float)_behaviour.myColliderSet[6]["Offset_x"], (float)_behaviour.myColliderSet[6]["Offset_y"]);
            projectile.GetComponent<Animator>().SetTrigger("isSkeletonAttackLeft");
        }
        else if (_dir == Vector2.right)
        {
            projectile.GetComponent<SkeletonProjectile>().myColliderSize = new Vector2((float)_behaviour.myColliderSet[7]["Size_x"], (float)_behaviour.myColliderSet[7]["Size_y"]);
            projectile.GetComponent<SkeletonProjectile>().myColliderOffset = new Vector2((float)_behaviour.myColliderSet[7]["Offset_x"], (float)_behaviour.myColliderSet[7]["Offset_y"]);
            projectile.GetComponent<Animator>().SetTrigger("isSkeletonAttackRight");
        }
        #endregion

        
    }

    public void AttackMelee_Ready()
    {
        _behaviour = GetComponent<SkeletonClass>();
        _pos = this.transform.position;
        _dir = _behaviour.myDirection;
        _pos += _dir * 0.1f;
        this.transform.position = _pos;
    }

    #region Not Used

    public void AttackSkill1_End()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_Execute()
    {
        throw new System.NotImplementedException();
    }

    public void AttackSkill1_Ready()
    {
        throw new System.NotImplementedException();
    }

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
