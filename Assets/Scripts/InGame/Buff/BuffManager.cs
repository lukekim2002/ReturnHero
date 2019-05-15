using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    #region PRIVATE
    private Color heroColor = new Color(1f, 1f, 1f, 1f);
    private Buff heroCurrentBuff;
    private float buffAnimationTime = 0.1f;
    private float buffAlpha = 1.0f;
    private float buffAlphaCalcuateTime = 0.1f;
    private float buffTime;
    private float buffDuration;
    private float[,] buffColorR = new float[6, 6];
    private float[,] buffColorG = new float[6, 6];
    private float[,] buffColorB = new float[6, 6];

    private SpriteRenderer heroSpriterenderer;
    #endregion

    #region PUBLIC
    public List<Buff> buffList = new List<Buff>();
    public bool isBleeded = false;
    public bool isBurned = false;
    public bool isFrosted = false;
    public bool isPoisoned = false;
    public bool isBlinded = false;
    public bool isSturned = false;
    #endregion

    private void Start()
    {   
        heroSpriterenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isBleeded)
        {
            BuffInit(new Color(0.7411765f, 0.1921569f, 0.0627451f, 1));
            heroCurrentBuff = buffList[buffList.Count - 1];
            isBleeded = !isBleeded;
        }
        else if (isBurned)
        {
            BuffInit(new Color(0.7411765f, 0.1921569f, 0.0627451f, 1));
            heroCurrentBuff = buffList[buffList.Count - 1];
            isBurned = !isBurned;
        }
        else if (isFrosted)
        {
            BuffInit(new Color(0.0627451f, 0.6627451f, 0.7411765f, 1));
            heroCurrentBuff = buffList[buffList.Count - 1];
            isFrosted = !isFrosted;
        }
        else if (isPoisoned)
        {
            BuffInit(new Color(0.5960785f, 0.3921569f, 0.6392157f, 1));
            heroCurrentBuff = buffList[buffList.Count - 1];
            isPoisoned = !isPoisoned;
        }
        else if (isBlinded)
        {
            heroCurrentBuff = buffList[buffList.Count - 1];
            isBlinded = !isBlinded;
        }
        else if (isSturned)
        {
            heroCurrentBuff = buffList[buffList.Count - 1];
            isSturned = !isSturned;
        }

        for (int i = buffList.Count - 1; i >= 0; i--)
        {
            if (buffList.Count == 0)
                break;

            BuffTick(buffList[i]);
        }
    }

    public void BuffTick(Buff buff)
    {
        buff.Tick(Time.deltaTime);

        BuffAnimation();

        if (buff.IsFinished)
        {
            buff.End();
            buffList.Remove(buff);

            if (buffList.Count == 0)
            {
                BuffInit(new Color(1f, 1f, 1f, 1f));
            }
        }
    }

    public void BuffInit(Color m_Color)
    {
        this.heroColor = m_Color;
        heroSpriterenderer.color = m_Color;
        buffAlpha = 1.0f;
        buffAlphaCalcuateTime = 0.1f;
        buffAnimationTime = 0.1f;
    }

    public void BuffAnimation()
    {
        if (heroCurrentBuff.buffTime - heroCurrentBuff.buffDurationTime >= buffAnimationTime)
        {
            buffAnimationTime += 0.1f;
            buffAlpha -= buffAlphaCalcuateTime;
            heroSpriterenderer.color = new Color(heroColor.r, heroColor.g, heroColor.b, buffAlpha);

            if (buffAlpha >= 1.0f || buffAlpha <= 0.5f)
            {
                buffAlphaCalcuateTime *= -1;
            }
        }
    }
}

