using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Burn : Debuff
{
    public IAttackable attackable;
    public Debuff_Burn() : base(5f, 1f, 10) { }

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
