using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypeA : WeaponBase
{
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
}
