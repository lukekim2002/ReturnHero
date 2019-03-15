using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    #region PRIVATE
    private BoxCollider2D _wallBoxcollider2D;
    private SpriteRenderer _wallSprite;
    private bool _isRoomClear = false;
    #endregion

    #region PUBLIC
    // 방 안에 있는 몬스터들
    public GameObject[] monsters;
    // 방 안에 있는 몬스터 개수
    public int monsterNum;
    #endregion

    private void Start()
    {
        _wallBoxcollider2D = GetComponent<BoxCollider2D>();
        _wallSprite = GetComponent<SpriteRenderer>();
        monsterNum = monsters.Length;
    }

    //public void SetChildrenIsTriggerOn()
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        BoxCollider2D boxCollider2D = transform.GetChild(i).GetComponent<BoxCollider2D>();
    //        boxCollider2D.isTrigger = true;
    //    }
    //}

    //public void SetChildrenIsTriggerOff()
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        BoxCollider2D boxCollider2D = transform.GetChild(i).GetComponent<BoxCollider2D>();
    //        boxCollider2D.isTrigger = false;

    //        //gateKeeper.SetActive(true);
    //    }
    //}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isRoomClear == false)
        {
            if (collision.CompareTag("Player"))
            {
                _wallSprite.color = new Color(255, 255, 255, 255);
                _wallBoxcollider2D.isTrigger = false;
            }
        }
    }

    public void CountMonsterInRoom()
    {
        monsterNum--;

        if (monsterNum == 0)
        {
            _wallSprite.color = new Color(255, 255, 255, 128);
            _wallBoxcollider2D.isTrigger = true;
            _isRoomClear = true;
        }
    }
}
