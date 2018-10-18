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
    public bool isSkillCoolTimeEnable = true;
    #endregion

    private void Start()
    {
        ResetCoolTime();
    }

    private void Update()
    {
        if (isSkillCoolTimeEnable)
        {
            StartCoroutine("CalcuateCoolTime");
            isSkillCoolTimeEnable = false;
        }
    }

    IEnumerator CalcuateCoolTime()
    {
        while (translucencyCoolTimeImage.fillAmount > 0.0f)
        {            
            translucencyCoolTimeImage.fillAmount -= 1 / (skillCoolTime * 10);

            print(translucencyCoolTimeImage.name + " : " + translucencyCoolTimeImage.fillAmount);
            yield return new WaitForSeconds(0.1f);
        }

        ResetCoolTime();
        yield break;
    }

    // 쿨타임 초기화
    private void ResetCoolTime()
    {
        isSkillCoolTimeEnable = false;
        translucencyCoolTimeImage.fillAmount = 1;
        translucencyCoolTimeImage.gameObject.SetActive(false);
    }
}
