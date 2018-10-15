using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeClass : MonoBehaviour
{

    #region PRIVATE
    private float leftCoolTime;
    #endregion

    #region PUBLIC
    public Image translucencyCoolTimeImage;
    public float skillCoolTime;
    public bool isSkillCoolTimeEnable = true;
    #endregion

    private void Start()
    {
        leftCoolTime = skillCoolTime;
        // ResetCoolTime();
    }

    private void Update()
    {
        if (isSkillCoolTimeEnable)
        {
            StartCoroutine(CalcuateCoolTime(HeroGeneralManager.SKillCoolTime.SKILL_E));

            isSkillCoolTimeEnable = false;
        }
    }

    IEnumerator CalcuateCoolTime(HeroGeneralManager.SKillCoolTime skillNum)
    {
        while (translucencyCoolTimeImage.fillAmount > 0.0f)
        {
            leftCoolTime -= 1 / skillCoolTime;

            print(leftCoolTime);

            if (leftCoolTime <= 0)
            {
                leftCoolTime = 0;
                ResetCoolTime();

                yield break;
            }
            translucencyCoolTimeImage.fillAmount = (leftCoolTime / skillCoolTime);

            yield return new WaitForSeconds(0.1f);
        }
    }

    // 쿨타임 초기화
    private void ResetCoolTime()
    {
        translucencyCoolTimeImage.fillAmount = 1;
        leftCoolTime = skillCoolTime;
        translucencyCoolTimeImage.gameObject.SetActive(false);
    }
}
