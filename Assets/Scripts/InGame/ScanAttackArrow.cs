using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanAttackArrow : MonoBehaviour
{
    #region PRIVATE
    private SpriteRenderer _arrowSpriteRenderer;
    #endregion

    #region PUBLIC
    public Sprite[] arrowSprite;
    #endregion

    // 마우스 포인터가 놓인 위치에 따라서 캐릭터 밑의 화살표가 바뀌는 코드
    
    void Start()
    {
        _arrowSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ScanDirection();
    }

    private void ScanDirection()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray2D ray2D = new Ray2D(cursorPos, Vector2.zero);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(ray2D.origin, ray2D.direction);

        if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.name.Equals("Top"))
        {
            _arrowSpriteRenderer.sprite = arrowSprite[3];
        }
        else if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.name.Equals("Bottom"))
        {
            _arrowSpriteRenderer.sprite = arrowSprite[0];
        }
        else if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.name.Equals("Left"))
        {
            _arrowSpriteRenderer.sprite = arrowSprite[1];
        }
        else if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.name.Equals("Right"))
        {
            _arrowSpriteRenderer.sprite = arrowSprite[2];
        }
    }
}
