using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManticoreAttackEffect : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo stateInfo;

    public GameObject[] myColliderSet; // Up = 0, Down = 1, Left = 2, Right = 3

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ColliderOn()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Manticore_AttackEffect_Up"))
            myColliderSet[0].SetActive(true);
        else if (stateInfo.IsName("Manticore_AttackEffect_Down"))
            myColliderSet[1].SetActive(true);
        else if (stateInfo.IsName("Manticore_AttackEffect_Left"))
            myColliderSet[2].SetActive(true);
        else if (stateInfo.IsName("Manticore_AttackEffect_Right"))
            myColliderSet[3].SetActive(true);
    }

    public void ColliderOff()
    {
        foreach (GameObject collider in myColliderSet)
        {
            if(collider.activeSelf != false)
                collider.SetActive(false);
        }
    }
}
