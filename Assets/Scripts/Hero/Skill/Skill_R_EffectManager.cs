using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_R_EffectManager : MonoBehaviour {

    #region PRIVATE
    private Animator _skill_R_Animator;
    #endregion

    #region PUBLIC
    public enum ATTACKDIRECTION { NONE, UP, LEFT, RIGHT, DOWN };
    static public ATTACKDIRECTION attackDirection = ATTACKDIRECTION.NONE;
    static public bool isSkillR = false;
    #endregion

    private void Start()
    {
        _skill_R_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSkillR)
        {
            this.GetComponent<Renderer>().enabled = true;

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
    }

    private void EndSkillMrEffect()
    {
        attackDirection = ATTACKDIRECTION.NONE;
        isSkillR = false;
        this.GetComponent<Renderer>().enabled = false;
    }
}
