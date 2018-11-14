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
    #endregion

    private void OnEnable()
    {
        ResetCoolTime();
        translucencyCoolTimeImage.gameObject.SetActive(true);
        StartCoroutine(CalcuateCoolTime());
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

        this.enabled = false;

        yield break;
    }

    // 쿨타임 초기화
    private void ResetCoolTime()
    {
        translucencyCoolTimeImage.fillAmount = 1;
        translucencyCoolTimeImage.gameObject.SetActive(false);
    }
}