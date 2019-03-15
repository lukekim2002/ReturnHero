using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomClose : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.GetComponentInParent<RoomManager>().CountMonsterInRoom();
            this.gameObject.SetActive(false);
        }
    }
}
