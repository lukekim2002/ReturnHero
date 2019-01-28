using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttackEffect : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.SetTrigger("isLichAttackEffect");
        transform.position = HeroGeneralManager.instance.heroObject.transform.position;
    }

    private void OnDisable()
    {
        animator.ResetTrigger("isLichAttackEffect");
    }
}
