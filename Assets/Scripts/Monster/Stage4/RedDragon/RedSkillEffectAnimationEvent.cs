﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSkillEffectAnimationEvent : MonoBehaviour
{
    private Transform parentTransform;
    CircleCollider2D circleCollider;

    private void OnEnable()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        /*
        parentTransform = this.transform.parent;
        this.transform.parent = this.transform.parent.parent;
        */
    }

    public void ColliderOn()
    {
        circleCollider.enabled = true;
    }

    public void ColliderOff()
    {
        circleCollider.enabled = false;
    }

    public void On()
    {
        // get out from parent
        parentTransform = transform.parent;
        transform.parent = transform.parent.parent;

        transform.position = HeroGeneralManager.instance.heroObject.transform.position;

    }

    public void Off()
    {
        // go in to parent
        this.transform.parent = parentTransform;
    }
}