using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    BuffTrigger bleeded = new BuffTrigger();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print(bleeded.isBleeded);   
            if (bleeded.isBleeded == false)
            {
                bleeded.OnBleeding();
                bleeded.isBleeded = true;
            }
        }
    }
}
