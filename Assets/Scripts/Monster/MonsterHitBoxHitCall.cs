using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitBoxHitCall : MonoBehaviour
{
    

    public void GetHit(int damage)
    {
        transform.root.gameObject.SendMessage("HitByPlayer", damage, SendMessageOptions.DontRequireReceiver);
    }
}
