using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Poison : Debuff
{
    public Debuff_Poison() : base(10f, 1, 5) { }

    public override void ApplyEffect(GameObject target)
    {
        if (ShouldTick())
        {
            IAttackable attackable = target.GetComponent<IAttackable>();
            if(attackable != null)
            {
                attackable.OnTakeDamaged(DamagePerTick);
            }
            UpdateTickTime();
        }
    }
}
