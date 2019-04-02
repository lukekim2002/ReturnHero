using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSetEffectEvent : MonoBehaviour
{
    public GameObject Skeleton;

    public void SpawnSkeltons()
    {
        Instantiate(Skeleton, transform.position, Quaternion.identity);
    }

    public void SetInactive()
    {
        if (transform.parent.gameObject.activeSelf == true)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
