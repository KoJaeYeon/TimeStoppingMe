using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypeA : WeaponBase
{
    private float burnChance = 0.15f;

    private void Start()
    {
        baseDamage = 15;
        baseFireRate = 1.5f;
        baseRange = 60f;
        projectileSpeed = 25f;
        maxAmmoSize = 20;
        reloadTime = 2f;
        fireMode = FireMode.Burst;
        projectileCount = 2;
        projectilePattern = ProjectilePattern.Parallel;
    }

    public override void Fire(LayerMask enemyLayers)
    {
        base.Fire(enemyLayers);

        if(Random.value < burnChance)
        {
            ApplyBurnEffect(enemyLayers);
        }
    }

    private void ApplyBurnEffect(LayerMask enemyLayers)
    {
        Collider[] hitColliders = Physics.OverlapSphere(firePoint.position, baseRange, enemyLayers);
        foreach (Collider hitCollider in hitColliders)
        {
            IAttackable attackable = hitCollider.GetComponent<IAttackable>();
            if (attackable != null)
            {
                attackable.ApplyStatusEffect(new Debuff_Burn());
            }
        }
    }
}
