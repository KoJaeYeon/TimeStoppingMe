using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Montser_Range : Monster
{   
    public Transform LaunchTrans;

    public GameObject projectile_Prefab;

    public void SpawnProjectile()
    {
        var projecile = Instantiate(projectile_Prefab);
        var projectile_Range = projecile.GetComponent<Projectile_Range>();

        projectile_Range.Init(LaunchTrans.position, TargetTrans.position, monster_Data.projectile1_EndDistance);
    }

    public override void Attack0()
    {
        SpawnProjectile();
    }
}
