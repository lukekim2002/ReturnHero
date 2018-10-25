using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonProjectile : MonoBehaviour {

    MonsterBehaviorManager rootBehaviour;

    Vector2 myPos;
    Vector2 myDir;

    float moveSpeed = 0.1f;
    float moveDelayTime = (1 / 60);
    int delayFrame = 2;

    /*
    private void OnEnable()
    {
        rootBehaviour = transform.root.GetComponent<MonsterBehaviorManager>();
        myPos = transform.position;
    }

    IEnumerator WaitDelay()
    {
        
        yield return new WaitForSeconds(delayFrame * moveDelayTime);
    }
    */
}
