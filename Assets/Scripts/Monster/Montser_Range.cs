using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Montser_Range : Monster
{
    public Transform TargetTrans;
    public Transform LaunchTrans;

    public Transform projectile_StartPos;
    public GameObject projectile_Prefab;

    public void SpawnProjectile()
    {
        var projecile = Instantiate(projectile_Prefab);
        var projectile_Range = projecile.GetComponent<Projectile_Range>();

        projectile_Range.Init(projectile_StartPos.position, TargetTrans.position);
    }

    public override void Attack0()
    {
        base.Attack0();
    }
}
