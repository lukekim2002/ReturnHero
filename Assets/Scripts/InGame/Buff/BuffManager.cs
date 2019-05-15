using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    #region PRIVATE
    private Color heroColor = new Color(1f, 1f, 1f, 1f);
    private Buff heroCurrentBuff;
    private int buffColorIndex = 0;
    private int buffColorFlag = 1;
    private float buffAnimationTime = 0.1f;
    private float buffTime;
    private float buffDuration;
    private float[] buffColorR = new float[5] { 0.5960785f, 0.509804f, 0.509804f, 0.4705882f, 0.4235294f };
    private float[] buffColorG = new float[5] { 0.3921569f, 0.3294118f, 0.2588235f, 0.1960784f, 0.1294118f };
    private float[] buffColorB = new float[5] { 0.6392157f, 0.6039216f, 0.5607843f, 0.5215687f, 0.4823529f };

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
            BuffInit();
            heroCurrentBuff = buffList[buffList.Count - 1];
            isBleeded = !isBleeded;
        }
        else if (isBurned)
        {
            BuffInit();
            heroCurrentBuff = buffList[buffList.Count - 1];
            isBurned = !isBurned;
        }
        else if (isFrosted)
        {
            BuffInit();
            heroCurrentBuff = buffList[buffList.Count - 1];
            isFrosted = !isFrosted;
        }
        else if (isPoisoned)
        {
            BuffInit();
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
                BuffInit();
            }
        }
    }

    public void BuffInit()
    {
        this.heroColor = new Color(1, 1, 1, 1);
        heroSpriterenderer.color = new Color(1, 1, 1, 1);
        buffColorIndex = 0;
        buffColorFlag = 1;
        buffAnimationTime = 0.1f;
    }

    public void BuffAnimation()
    {
        if (heroCurrentBuff.buffTime - heroCurrentBuff.buffDurationTime >= buffAnimationTime)
        {
            print(buffColorIndex);
            buffAnimationTime += 0.1f;
            buffColorIndex += buffColorFlag;
            heroSpriterenderer.color = new Color(buffColorR[buffColorIndex], buffColorG[buffColorIndex], buffColorB[buffColorIndex], 0.5f);

            if (buffColorIndex == 0 || buffColorIndex == 4)
            {
                buffColorFlag *= -1;
            }
        }
    }
}

