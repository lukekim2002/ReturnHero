using UnityEngine;
using System.Collections;

public class Poisoned : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Buff buff in collision.GetComponent<BuffManager>().buffList)
            {
                if (buff.buffName == "Poisoned")
                {
                    collision.GetComponent<BuffManager>().buffList.Remove(buff);
                    break;
                }
            }

            collision.GetComponent<BuffManager>().buffList.Add(BuffDatabase.poisoned);
        }
    }
}
