using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    #region PRIVATE
    private BoxCollider2D _wallBoxcollider2D;
    private SpriteRenderer _wallSprite;
    private bool _isRoomClear = false;
    private int _monsterNum;
    #endregion

    #region PUBLIC
    // 방 안에 있는 몬스터들
    public GameObject[] monsters;
    public GameObject[] walls;
    #endregion

    private void Start()
    {
        _wallSprite = GetComponent<SpriteRenderer>();
        _monsterNum = monsters.Length;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isRoomClear == false)
        {
            if (collision.CompareTag("Player"))
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    var wallBoxCollider = walls[i].GetComponent<BoxCollider2D>();
                    var wallSpriteRenderer = walls[i].GetComponent<SpriteRenderer>();

                }
            }
        }
    }

    public void CountMonsterInRoom()
    {
        _monsterNum--;

        if (_monsterNum == 0)
        {
            for (int i = 0; i < walls.Length; i++)
            {
                var wallBoxCollider = walls[i].GetComponent<BoxCollider2D>();
                var wallSpriteRenderer = walls[i].GetComponent<SpriteRenderer>();
                wallBoxCollider.enabled = false;
                wallSpriteRenderer.color = new Color(255, 255, 255, 128);
            }
            _isRoomClear = true;
        }
    }
}
