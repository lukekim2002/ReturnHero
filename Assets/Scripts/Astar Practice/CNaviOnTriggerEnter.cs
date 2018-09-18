using UnityEngine;
using System.Collections;
using Pathfinding;

public class CNaviOnTriggerEnter : MonoBehaviour
{
    private IAstarAI astarAI;

    private void Start()
    {
        astarAI = this.transform.parent.GetComponent<AIPath>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            astarAI.canMove = true;
    }
}
