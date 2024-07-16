using UnityEngine;

public class Debuff_Poison : Debuff
{
    public IAttackable attackable;
    public Debuff_Poison() : base(10f, 1, 5) { }

    public override void ApplyEffect(GameObject target)
    {
        if (attackable == null)
        {
            attackable = target.GetComponent<IAttackable>();
        }
        if (attackable != null)
        {
            attackable.OnTakeDamaged(DamagePerTick);
        }
        else
        {
            Debug.LogAssertion("Iattack Is Null");
        }
    }
}
