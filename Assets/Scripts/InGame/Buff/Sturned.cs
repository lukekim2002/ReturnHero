using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sturned : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Sturned");
        }
    }
}
