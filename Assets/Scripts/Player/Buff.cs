using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    public float Duration { get; set; }
    protected float endTime;
    public bool IsTemporary { get; set; }

    public Buff(float duration)
    {
        Duration = duration;
        IsTemporary = duration > 0;
        endTime = Time.time + duration;
    }

    public virtual void ApplyEffect(Player target) { }
    public virtual void RemoveEffect(Player target) { }

    public bool IsEffectOver()
    {
        return IsTemporary && Time.time >= endTime;
    }
}
