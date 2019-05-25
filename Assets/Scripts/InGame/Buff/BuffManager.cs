using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    #region PRIVATE
    private Buff _heroCurrentBuff;
    private Color _heroColor = new Color(1f, 1f, 1f, 1f);
    private float _buffAnimationTime = 0.1f;
    private float _buffTime;
    private float _buffDuration;
    private int _buffColorIndex = 0;
    private int _buffColorFlag = 1;
    private List<Dictionary<string, object>> _buffSet;
    private string _currentBuffName;
    private SpriteRenderer _heroSpriterenderer;
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
        _heroSpriterenderer = GetComponent<SpriteRenderer>();
        _buffSet = CSVReader.Read("CSV/Condition_Abnormal");
    }

    private void Update()
    {
        if (isBleeded)
        {
            BuffInit();
            _heroCurrentBuff = buffList[buffList.Count - 1];
            _currentBuffName = "Bleeded";
            isBleeded = !isBleeded;
        }
        else if (isBurned)
        {
            BuffInit();
            _heroCurrentBuff = buffList[buffList.Count - 1];
            _currentBuffName = "Burned";
            isBurned = !isBurned;
        }
        else if (isFrosted)
        {
            BuffInit();
            _heroCurrentBuff = buffList[buffList.Count - 1];
            _currentBuffName = "Frosted";
            isFrosted = !isFrosted;
        }
        else if (isPoisoned)
        {
            BuffInit();
            _heroCurrentBuff = buffList[buffList.Count - 1];
            _currentBuffName = "Poisoned";
            isPoisoned = !isPoisoned;
        }
        else if (isBlinded)
        {
            _heroCurrentBuff = buffList[buffList.Count - 1];
            isBlinded = !isBlinded;
        }
        else if (isSturned)
        {
            _heroCurrentBuff = buffList[buffList.Count - 1];
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
                _heroSpriterenderer.color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void BuffInit()
    {
        _buffColorIndex = 0;
        _buffColorFlag = 1;
        _buffAnimationTime = 0.1f;
    }

    public void BuffAnimation()
    {
        if (_heroCurrentBuff.buffTime - _heroCurrentBuff.buffDurationTime >= _buffAnimationTime)
        {
            _buffAnimationTime += 0.1f;
            _heroSpriterenderer.color = new Color((float)_buffSet[_buffColorIndex * 3][_currentBuffName]
                , (float)_buffSet[_buffColorIndex * 3 + 1][_currentBuffName], (float)_buffSet[_buffColorIndex * 3 + 2][_currentBuffName], 0.5f);

            print(_heroSpriterenderer.color);
            _buffColorIndex += _buffColorFlag;

            if (_buffColorIndex == 0 || _buffColorIndex == 4)
            {
                _buffColorFlag *= -1;
            }
        }
    }
}

