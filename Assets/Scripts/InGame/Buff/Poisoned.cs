using UnityEngine;

public class Poisoned : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BuffDatabase buffDatabase = new BuffDatabase();

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

            collision.GetComponent<BuffManager>().buffList.Add(buffDatabase.poisoned);
            collision.GetComponent<BuffManager>().isPoisoned = true;
        }
    }
}
