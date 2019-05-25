using UnityEngine;

public class Burned : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BuffDatabase buffDatabase = new BuffDatabase();

        if (collision.CompareTag("Player"))
        {
            foreach (Buff buff in collision.GetComponent<BuffManager>().buffList)
            {
                if (buff.buffName == "Burned")
                {
                    collision.GetComponent<BuffManager>().buffList.Remove(buff);
                    break;
                }
            }

            collision.GetComponent<BuffManager>().buffList.Add(buffDatabase.burned);
            collision.GetComponent<BuffManager>().isBurned = true;
        }
    }
}
