using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{
    public float Duration { get; set; }
    public float TickInterval { get; set; }
    public int DamagePerTick {  get; set; }
    protected float lastTickTime;
    protected float endTime;

    public Debuff(float duration, float tickInterval, int damagePerTick)
    {
        Duration = duration;
        TickInterval = tickInterval;
        DamagePerTick = damagePerTick;
        lastTickTime = 0f;
        endTime = Time.time + duration;
    }

    public virtual void ApplyEffect(GameObject target) { }

    public bool IsEffectOver()
    {
        return Time.time >= endTime;
    }

    public bool ShouldTick()
    {
        return Time.time >= lastTickTime + TickInterval;
    }

    public void UpdateTickTime()
    {
        lastTickTime = Time.time;
    }
}

public class Debuff_Mind : Debuff
{
    public Debuff_Mind() : base(5f, 0, 0) { }
}
