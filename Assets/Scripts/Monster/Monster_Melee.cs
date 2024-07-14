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
        var monster_Data_Melee = monster_Data as Monster_Data_Melee;
        sphereCollider.radius = monster_Data_Melee.attack_MaxDistance;
        AttackCollision.SetActive(false);
    }

    public override void Attack1()
    {
        AttackCollision.SetActive(true);
    }

    public override void Attack1_End()
    {
        AttackCollision.SetActive(false);
    }
}
