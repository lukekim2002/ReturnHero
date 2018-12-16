using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_R_EffectManager : MonoBehaviour {

    #region PRIVATE
    private Animator _skill_R_Animator;
    private Transform _heroObject;
    private SpriteRenderer _Skill_R_Sprite;
    #endregion

    #region PUBLIC
    public enum ATTACKDIRECTION { NONE, UP, LEFT, RIGHT, DOWN };
    static public ATTACKDIRECTION attackDirection = ATTACKDIRECTION.NONE;
    static public bool isSkillR = false;
    #endregion

    private void OnEnable()
    {
        _skill_R_Animator = GetComponent<Animator>();
        _Skill_R_Sprite = GetComponent<SpriteRenderer>();

        // MR Effect의 부모 오브젝트를 저장
        _heroObject = this.transform.parent;
        // 부모 오브젝트 좌표를 MR Effect에 설정
        this.transform.position = _heroObject.position;
        // MR Effect의 부모 오브젝트를 MR Effect의 부모오브젝트의 부모오브젝트로 바꾼다. (Skeleton에서 해제한다.)
        this.transform.parent = this.transform.parent.parent;

        if (isSkillR)
        {

            if (attackDirection == ATTACKDIRECTION.UP)
            {
                _skill_R_Animator.SetTrigger("SkillRUp");
            }
            else if (attackDirection == ATTACKDIRECTION.LEFT)
            {
                _skill_R_Animator.SetTrigger("SkillRLeft");
            }
            else if (attackDirection == ATTACKDIRECTION.RIGHT)
            {
                _skill_R_Animator.SetTrigger("SkillRRight");
            }
            else if (attackDirection == ATTACKDIRECTION.DOWN)
            {
                _skill_R_Animator.SetTrigger("SkillRDown");
            }
        }
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void EndSkillREffect()
    {
        attackDirection = ATTACKDIRECTION.NONE;
        isSkillR = false;
        transform.parent = _heroObject;

        _Skill_R_Sprite.enabled = false;
        _Skill_R_Sprite.sprite = null;

        this.gameObject.SetActive(false);
    }
}
