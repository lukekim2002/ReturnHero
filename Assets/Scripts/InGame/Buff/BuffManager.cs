using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    #region PRIVATE
    private float buffAnimationTime = 0.1f;
    private float buffAlpha = 0.0f;
    private float buffAlphaCalcuateTime = 0.1f;
    #endregion

    #region PUBLIC
    public List<Buff> buffList = new List<Buff>();
    public SpriteRenderer buffSpriteRenderer;
    public bool isBleeded = false;
    #endregion

    public void Update()
    {
        if (isBleeded)
        {
            buffSpriteRenderer.color = new Color(0.7411765f, 0.1921569f, 0.0627451f, buffAlpha);
            buffAlpha = 0.0f;
            buffAlphaCalcuateTime = 0.1f;
            buffAnimationTime = 0.1f;
            isBleeded = !isBleeded;
        }

        for (int i = buffList.Count - 1; i >= 0; i--)
        {
            if (buffList[i].buffName == "Bleeded")
            {
            }

            if (buffList[i].buffName == "Blinded")
            {
            }

            if (buffList[i].buffName == "Burned")
            {
            }

            if (buffList[i].buffName == "Frosted")
            {
            }

            if (buffList[i].buffName == "Poisoned")
            {
            }

            if (buffList[i].buffName == "Sturned")
            {
            }

            if (buffList.Count == 0)
                break;

            buffSpriteRenderer.sprite = HeroGeneralManager.instance.heroObject.GetComponent<SpriteRenderer>().sprite;

            BuffTick(buffList[i]);
        }
    }

    public void BuffTick(Buff buff)
    {
        buff.Tick(Time.deltaTime);

        if (buff.buffTime - buff.buffDurationTime >= buffAnimationTime)
        {
            buffAnimationTime += 0.1f;
            buffAlpha += buffAlphaCalcuateTime;
            buffSpriteRenderer.color = new Color(0.7411765f, 0.1921569f, 0.0627451f, buffAlpha);

            print(buffAlpha);

            if (buffAlpha <= 0.0f || buffAlpha >= 0.5f)
            {
                buffAlphaCalcuateTime *= -1;
            }
        }

        if (buff.IsFinished)
        {
            buff.End();
            buffList.Remove(buff);

            buffSpriteRenderer.sprite = null;
            buffSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            buffAlpha = 0.0f;
            buffAlphaCalcuateTime = 0.1f;
            buffAnimationTime = 0.1f;
        }
    }
}

