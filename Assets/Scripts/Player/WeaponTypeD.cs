using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypeD : WeaponBase
{
    private float poisonChance = 0.30f;

    private void Start()
    {
        baseDamage = 20;
        baseFireRate = 1f;
        baseRange = 50f;
        projectileSpeed = 30f;
        maxAmmoSize = 10;
        reloadTime = 3f;
        fireMode = FireMode.Single;
        projectileCount = 5; // 예시로 산탄 발사체 5개
        projectilePattern = ProjectilePattern.Spread;

        Init();
    }

    public override void Fire(LayerMask enemyLayers)
    {
        base.Fire(enemyLayers);

        foreach(var bullet in bullets)
        {
            bullet.debuffType = DebuffType.Poison;
            if(Random.value <= poisonChance)
            {
                bullet.debuff = new Debuff_Poison();
            }
        }
    }
}
