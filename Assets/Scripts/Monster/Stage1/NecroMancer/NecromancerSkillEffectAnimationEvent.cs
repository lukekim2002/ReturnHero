using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerSkillEffectAnimationEvent : MonoBehaviour
{
    private Transform parentTransform;
    CircleCollider2D circleCollider;

    public GameObject lichObject;

    private void OnEnable()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        /*
        parentTransform = this.transform.parent;
        this.transform.parent = this.transform.parent.parent;
        */
    }

    private void OnDisable()
    {
        /*
        this.transform.parent = parentTransform;
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

    public void SpawnLich()
    {
        Instantiate(lichObject, transform.position, Quaternion.identity);
        transform.gameObject.SetActive(false);
    }

}
