using UnityEngine;
using System.Collections;

public class BuffManager : MonoBehaviour
{
    void Update()
    {
        foreach (Buff buff in HeroGeneralManager.instance.buffList)
        {
            if (buff.buffName == "Bleeded")
            {
                BuffDatabase.bleeded.Tick(Time.deltaTime);
                if (BuffDatabase.bleeded.IsFinished)
                {
                    BuffDatabase.bleeded.End();
                    HeroGeneralManager.instance.buffList.Remove(buff);
                }
            }
        }
    }
}

