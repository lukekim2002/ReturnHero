using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSetEvent : MonoBehaviour
{
    public Vector2 SpawnPos;
    public GameObject[] SpawnEffects;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        SpawnPos = new Vector2(0, 0);
    }


    public void TurnOnSpawnEffects()
    {
        float xPos, yPos;

        for (int i = 0; i < 3; i++)
        {
            xPos = Random.Range(-1.5f, 1.5f);
            yPos = Random.Range(-1.5f, 1.5f);

            SpawnPos.x = xPos;
            SpawnPos.y = yPos;
            SpawnEffects[i].SetActive(true);
            SpawnEffects[i].transform.localPosition = SpawnPos;
        }

        animator.ResetTrigger("isTrapped");
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.enabled = true;
            animator.SetTrigger("isTrapped");
        }
    }

}
