using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Monster_Melee : Monster
{   
    public Transform LaunchTrans;

    public GameObject AttackCollision;

    public override void Awake()
    {
        base.Awake();

        var sphereCollider = AttackCollision.GetComponent<SphereCollider>();
        sphereCollider.radius = monster_Data.projectile1_EndDistance;
        AttackCollision.SetActive(false);
    }

    public override void Attack0()
    {
        AttackCollision.SetActive(true);
    }

    public override void Attack0_End()
    {
        AttackCollision.SetActive(false);
    }
}
