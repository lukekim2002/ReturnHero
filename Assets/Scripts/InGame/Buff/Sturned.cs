using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sturned : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Buff buff in collision.GetComponent<BuffManager>().buffList)
            {
                if (buff.buffName == "Frosted")
                {
                    collision.GetComponent<BuffManager>().buffList.Remove(buff);
                    break;
                }
            }

            collision.GetComponent<BuffManager>().buffList.Add(BuffDatabase.frosted);
        }
    }
}
