using UnityEngine;

public class BuffTrigger
{
    public void OnBleeding()
    {
        foreach (Buff buff in HeroGeneralManager.instance.buffList)
        {
            if (buff.buffName == BuffDatabase.bleeded.buffName)
            {
                HeroGeneralManager.instance.buffList.Remove(BuffDatabase.bleeded);
            }
        }
        HeroGeneralManager.instance.buffList.Add(BuffDatabase.bleeded);
    }
}
