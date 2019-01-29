using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpiritSkillTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.SendMessage("GetHealed", 5, SendMessageOptions.DontRequireReceiver);
        }
    }
}
