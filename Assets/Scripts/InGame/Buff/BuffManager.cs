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

