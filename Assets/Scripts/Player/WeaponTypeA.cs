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
        projectileCount = 5;
        projectilePattern = ProjectilePattern.Parallel;

        Init();
    }

    public override void Fire(LayerMask enemyLayers)
    {
        base.Fire(enemyLayers);

        foreach(var bullet in bullets)
        {
            bullet.debuffType = DebuffType.Burn;
            if(Random.value <= burnChance)
            {
                bullet.debuff = new Debuff_Burn();
            }
        }
    }
}
