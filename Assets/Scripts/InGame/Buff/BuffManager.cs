using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    #region PRIVATE
    private Color heroColor = new Color(1f, 1f, 1f, 1f);
    private float buffAnimationTime = 0.1f;
    private float buffAlpha = 1.0f;
    private float buffAlphaCalcuateTime = 0.1f;
    private SpriteRenderer heroSpriterenderer;
    #endregion

    #region PUBLIC
    public List<Buff> buffList = new List<Buff>();
    public bool isBleeded = false;
    public bool isBurned = false;
    public bool isFrosted = false;
    public bool isPoisoned = false;
    #endregion

    private void Start()
    {
        heroSpriterenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isBleeded)
        {
            BuffTimeInit(new Color(0.7411765f, 0.1921569f, 0.0627451f, 1));
            isBleeded = !isBleeded;
        }
        else if (isBurned)
        {
            BuffTimeInit(new Color(0.7411765f, 0.1921569f, 0.0627451f, 1));
            isBurned = !isBurned;
        }
        else if (isFrosted)
        {
            BuffTimeInit(new Color(0.0627451f, 0.6627451f, 0.7411765f, 1));
            isFrosted = !isFrosted;
        }
        else if (isPoisoned)
        {
            BuffTimeInit(new Color(0.4235294f, 0.1294118f, 0.4823529f, 1));
            isPoisoned = !isPoisoned;
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

            BuffTick(buffList[i]);
        }
    }

    //TODO : 최근의 디버프 색 표현 제대로 하기.
    public void BuffTick(Buff buff)
    {
        buff.Tick(Time.deltaTime);

        if (buff.buffTime - buff.buffDurationTime >= buffAnimationTime)
        {
            buffAnimationTime += 0.1f;
            buffAlpha -= buffAlphaCalcuateTime;
            heroSpriterenderer.color = new Color(heroColor.r, heroColor.g, heroColor.b, buffAlpha);
            print(buffAlpha);

            if (buffAlpha >= 1.0f || buffAlpha <= 0.5f)
            {
                buffAlphaCalcuateTime *= -1;
            }
        }

        if (buff.IsFinished)
        {
            buff.End();
            buffList.Remove(buff);

            if (buffList.Count == 0)
            {
                BuffTimeInit(new Color(1f, 1f, 1f, 1f));
            }
        }
    }

    public void BuffTimeInit(Color heroColor)
    {
        this.heroColor = heroColor;
        heroSpriterenderer.color = heroColor;
        buffAlpha = 1.0f;
        buffAlphaCalcuateTime = 0.1f;
        buffAnimationTime = 0.1f;
    }
}

