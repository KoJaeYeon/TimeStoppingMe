using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Monster_Range : Monster
{   
    public Transform LaunchTrans;

    public GameObject projectile_Prefab;

    public void SpawnProjectile()
    {
        var projecile = Instantiate(projectile_Prefab);
        var projectile_Range = projecile.GetComponent<Projectile_Range>();

        projectile_Range.Init(LaunchTrans.position, TargetTrans.position, monster_Data.projectile1_EndDistance, monster_Data.projectile1_EndHeight);
    }

    public override void Attack0()
    {
        SpawnProjectile();
    }
}
