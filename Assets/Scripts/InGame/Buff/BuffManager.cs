using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    #region PRIVATE

    #endregion

    #region PUBLIC
    public List<Buff> buffList = new List<Buff>();
    #endregion

    public void Update()
    {
        for (int i = buffList.Count - 1; i >= 0; i--)
        {
            if (buffList[i].buffName == "Bleeded")
            {
                BuffTick(buffList[i]);
            }

            if (buffList.Count == 0)
                break;

            if (buffList[i].buffName == "Burned")
            {
                BuffTick(buffList[i]);
            }
        }
    }

    public void BuffTick(Buff buff)
    {
        buff.Tick(Time.deltaTime);
        print(buff.buffName + " : " + buff.buffDurationTime);

        if (buff.IsFinished)
        {
            buff.End();
            buffList.Remove(buff);
        }
    }
}

