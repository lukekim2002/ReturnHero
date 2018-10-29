using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionClickEvent : MonoBehaviour
{
    #region PRIVATE
    private Vector2 _startPos;
    private Vector2 _endPos;
    private bool _isProductionImageOpened = false;
    #endregion

    #region PUBLIC
    public Image productionRightImage;
    public float duration = 0.5f;
    public float closeProductionX = 323f;
    public float openProductionX = 0f;
    #endregion

    // Production Left Image를 클릭할 때 Production Right Image가 드러남
    public void OnClickProductionOpenCloseButton()
    {
        if (!_isProductionImageOpened)
        {
            // 시작 좌표, 끝 좌표 설정
            _startPos = new Vector2(closeProductionX, 0);
            _endPos = new Vector2(openProductionX, 0);

            // 열리는 애니메이션 실행.
            StartCoroutine(MoveProductionCanvas());
        }
        else
        {
            // 시작 좌표, 끝 좌표 설정
            _startPos = new Vector2(openProductionX, 0);
            _endPos = new Vector2(closeProductionX, 0);

            // 닫히는 애니메이션 실행.
            StartCoroutine(MoveProductionCanvas());
        }
    }

    IEnumerator MoveProductionCanvas()
    {
        var timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            UIGeneralManager.instance.productionCanvas.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(_startPos, _endPos, timer / duration);
            
            if (UIGeneralManager.instance.productionCanvas.GetComponent<RectTransform>().anchoredPosition.x < 190)
            {
                productionRightImage.gameObject.SetActive(true);
            }
            else
            {
                productionRightImage.gameObject.SetActive(false);
            }
            yield return null;
        }

        _isProductionImageOpened = !_isProductionImageOpened;

        yield break;
    }
}
