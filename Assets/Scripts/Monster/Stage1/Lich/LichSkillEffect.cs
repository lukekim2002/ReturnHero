using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichSkillEffect : MonoBehaviour
{
    public GameObject SkeletonObject;

    Animator myanimator;

    private void Awake()
    {
        myanimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        myanimator.SetTrigger("isLichSkillEffect");
    }

    private void OnDisable()
    {
        myanimator.ResetTrigger("isLichSkillEffect");
    }

    public void SpawnSkeleton()
    {
        Instantiate(SkeletonObject, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
