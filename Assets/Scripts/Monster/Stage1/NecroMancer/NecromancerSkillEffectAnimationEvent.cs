using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerSkillEffectAnimationEvent : MonoBehaviour
{
    private Transform parentTransform;

    private void OnEnable()
    {
        parentTransform = this.transform.parent;
        this.transform.parent = this.transform.parent.parent;
    }

    private void OnDisable()
    {
        this.transform.parent = parentTransform;
    }
}
