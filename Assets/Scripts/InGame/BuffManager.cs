using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Buff
{
    public string buffName;
    public bool isBuffType; // true : Buff, false : Debuff
    public bool isBuffEffectFrame; // true : immediately, false : Continuously
    public float buffEffectDuration;
    public float buffValue;
}

public class BuffManager : MonoBehaviour
{
    Buff sturned = new Buff();

    private void Start()
    {
        #region Sturned
        sturned.buffName = "Sturned";
        sturned.isBuffType = false;
        sturned.isBuffEffectFrame = true;
        sturned.buffEffectDuration = 2.0f;
        sturned.buffValue = 0.0f;
        #endregion
    }
}
