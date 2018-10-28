using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeClass : MonoBehaviour
{

    #region PRIVATE
    #endregion

    #region PUBLIC
    public Image translucencyCoolTimeImage;
    public float skillCoolTime;
    public bool isSkillCoolTimeEnable = false;
    public bool isInputKey = false;
    #endregion

    private void Start()
    {
        ResetCoolTime();
    }

    private void Update()
    {
        if (isSkillCoolTimeEnable)
        {
            translucencyCoolTimeImage.gameObject.SetActive(true);
            StartCoroutine(CalcuateCoolTime());
            isSkillCoolTimeEnable = false;
        }
    }

    IEnumerator CalcuateCoolTime()
    {
        var timer = 0f;
        while (timer < skillCoolTime)
        {
            timer += Time.deltaTime;

            translucencyCoolTimeImage.fillAmount = 1f - (timer / skillCoolTime);

            // print(translucencyCoolTimeImage.name + " : " + translucencyCoolTimeImage.fillAmount);
            yield return null; // per frame
        }

        ResetCoolTime();
        yield break;
    }

    // 쿨타임 초기화
    private void ResetCoolTime()
    {
        isInputKey = false;
        translucencyCoolTimeImage.fillAmount = 1;
        translucencyCoolTimeImage.gameObject.SetActive(false);
    }
}
