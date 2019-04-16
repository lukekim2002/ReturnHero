using UnityEngine;

public class BuffTrigger : BuffDatabase
{
    public bool isBleeded = false;

    public void OnBleeding()
    {
        HeroGeneralManager.instance.buffList.Add(bleeded);
    }
}
