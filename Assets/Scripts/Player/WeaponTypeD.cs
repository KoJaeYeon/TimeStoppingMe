using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypeD : WeaponBase
{
    private void Start()
    {
        baseDamage = 20;
        baseFireRate = 1f;
        baseRange = 50f;
        projectileSpeed = 30f;
        maxAmmoSize = 10;
        reloadTime = 3f;
        fireMode = FireMode.Single;
        projectileCount = 5; // ���÷� ��ź �߻�ü 5��
        projectilePattern = ProjectilePattern.Spread;
    }
}
