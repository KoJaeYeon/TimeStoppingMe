using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Boss : Monster
{
    public Transform[] LaunchTrans; // 여러 발사 위치를 가지는 배열

    public GameObject AttackCollision;

    GameObject projectile_Prefab;

    public override void Awake()
    {
        base.Awake();

        var sphereCollider = AttackCollision.GetComponent<SphereCollider>();
        var monster_Data_Boss = monster_Data as Monster_Data_Boss;
        sphereCollider.radius = monster_Data_Boss.skill_bite_Distance;
        AttackCollision.SetActive(false);
    }
    public void SpawnProjectile_loveshot()
    {
        var monster_Data_Boss = monster_Data as Monster_Data_Boss;
        projectile_Prefab = monster_Data_Boss.loveshot_Prefab;

        // 첫 번째 발사 위치: 타겟을 직접 향함
        SpawnProjectile(LaunchTrans[0], TargetTrans.position, monster_Data_Boss);

        // 두 번째 발사 위치: 타겟을 향해 왼쪽 30도 각도로
        Vector3 leftDirection = Quaternion.Euler(0, -30, 0) * (TargetTrans.position - LaunchTrans[1].position);
        SpawnProjectile(LaunchTrans[1], LaunchTrans[1].position + leftDirection, monster_Data_Boss);

        // 세 번째 발사 위치: 타겟을 향해 오른쪽 30도 각도로
        Vector3 rightDirection = Quaternion.Euler(0, 30, 0) * (TargetTrans.position - LaunchTrans[2].position);
        SpawnProjectile(LaunchTrans[2], LaunchTrans[2].position + rightDirection, monster_Data_Boss);
    }

    private void SpawnProjectile(Transform launchTransform, Vector3 targetPosition, Monster_Data_Boss monsterData)
    {
        var projecile = Instantiate(projectile_Prefab, launchTransform.position, Quaternion.identity);
        var projectile_Loveshot = projecile.GetComponent<Projectile_LoveShot>();

        // 타겟 방향 계산
        Vector3 direction = (targetPosition - launchTransform.position).normalized;

        // 적절한 값으로 초기화
        projectile_Loveshot.Init(launchTransform.position, direction, monsterData.skill_loveshot_Distance, monsterData.skill_loveshot_LaunchSpeed);
    }

    public void RestoreHealth()
    {
        var monster_Data_Boss = monster_Data as Monster_Data_Boss;
        health += monster_Data_Boss.MaxHealth * monster_Data_Boss.skiil_bite_Drain * 0.01f;
        if(health >= monster_Data_Boss.MaxHealth)
        {
            health = monster_Data_Boss.MaxHealth;
        }
    }


    public override void Attack1()
    {
        SpawnProjectile_loveshot();
    }

    public override void Attack2()
    {
        AttackCollision.SetActive(true);
    }

    public override void Attack2_End()
    {
        AttackCollision.SetActive(false);
    }
}
