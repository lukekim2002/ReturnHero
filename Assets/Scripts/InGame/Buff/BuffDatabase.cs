using System;
using UnityEngine;

[Serializable]
public class Buff
{
    public string buffName;
    public bool isBuffType; // true : Buff, false : Debuff
    public float buffTime;
    public float buffDurationTime;
    public float buffValue;
    public float buffMoveSpeedValue;

    public Buff(string buffName, bool isBuffType, float buffTime, float buffValue, float buffMoveSpeedValue)
    {
        this.buffName = buffName;
        this.isBuffType = isBuffType;
        this.buffTime = buffTime;
        this.buffDurationTime = buffTime;
        this.buffValue = buffValue;
        this.buffMoveSpeedValue = buffMoveSpeedValue;
    }

    public void Tick(float m_DeltaTime)
    {
        this.buffDurationTime -= m_DeltaTime;
    }

    public bool IsFinished
    {
        get { return buffDurationTime <= 0 ? true : false; }
    }

    public void End()
    {
        this.buffDurationTime = this.buffTime;
    }
}

public class BuffDatabase
{
    public Buff sturned = new Buff("Struned", false, 2.0f, 0.0f, 1.0f);
    public Buff poisoned = new Buff("Poisoned", false, 7.0f, 0.03f, 0.2f);
    public Buff bleeded = new Buff("Bleeded", false, 7.0f, 0.01f, 0.0f);
    public Buff burned = new Buff("Burned", false, 5.0f, 0.05f, 0.0f);
    public Buff frosted = new Buff("Frosted", false, 7.0f, 0.0f, 0.5f);
    public Buff blinded = new Buff("Blinded", false, 10.0f, 0.0f, 0.0f);
}
