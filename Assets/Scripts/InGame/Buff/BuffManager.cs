using UnityEngine;
using System.Collections;

public class BuffManager : MonoBehaviour
{
    void Update()
    {
        foreach (Buff buff in HeroGeneralManager.instance.buffList)
        {
            Tick(buff);
        }
    }

    public void Tick(Buff m_Buff)
    {
        print(m_Buff.buffEffectDuration);
        m_Buff.buffEffectDuration -= Time.deltaTime;
        if (m_Buff.buffEffectDuration <= 0)
            End(m_Buff);
    }

    public void End(Buff m_Buff)
    {
        HeroGeneralManager.instance.buffList.Remove(m_Buff);
    }
}

