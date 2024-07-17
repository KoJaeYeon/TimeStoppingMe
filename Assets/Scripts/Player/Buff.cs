public abstract class  Buff
{
    public float Duration { get; set; }
    public float EffectValue { get; set; }

    public Buff(float duration, float effectValue)
    {
        Duration = duration;
        EffectValue = effectValue;
    }

    public virtual void ApplyEffect(Player player) { }
}
