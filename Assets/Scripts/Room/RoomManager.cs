using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    #region PRIVATE
    private BoxCollider2D _wallBoxcollider2D;
    private int _monsterNum;
    #endregion

    #region PUBLIC
    // 방 안에 있는 몬스터들
    public GameObject[] monsters;
    public GameObject walls;
    public GameObject portal;
    #endregion

    private void Start()
    {
        _monsterNum = monsters.Length;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            walls.GetComponent<TilemapCollider2D>().enabled = true;
            walls.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 1f);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void CountMonsterInRoom()
    {
        _monsterNum--;

        if (_monsterNum == 0)
        {
            walls.GetComponent<TilemapCollider2D>().enabled = false;
            walls.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 0.5f);
            OpenPortal();
        }
    }

    public void OpenPortal()
    {
        portal.SetActive(true);
    }
}
