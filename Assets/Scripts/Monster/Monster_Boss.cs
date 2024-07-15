using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Boss : Monster
{
    public Transform[] LaunchTrans; // 여러 발사 위치를 가지는 배열

    public GameObject AttackCollision;

    GameObject projectile_Prefab;
    GameObject prefabRoot;

    public override void Awake()
    {
        base.Awake();

        var sphereCollider = AttackCollision.GetComponent<SphereCollider>();
        var monster_Data_Boss = monster_Data as Monster_Data_Boss;
        sphereCollider.radius = monster_Data_Boss.skill_bite_Distance;
        AttackCollision.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Attack1();
        }
        else if(Input.GetKeyDown(KeyCode.F3))
        {
            Attack3();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Attack3_End();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            Attack4();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Attack4_End();
        }
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

    public override void Attack3()
    {
        prefabRoot = new GameObject();
        var monster_Data_Boss = monster_Data as Monster_Data_Boss;

        Projectile_HellFire.used = false;
        for (int i = 0; i < monster_Data_Boss.fireFlooring_number; i++)
        {
            Vector3 newPos = GetRandomNavMeshPoint(transform.position, 0, monster_Data_Boss.fiireFlooring_range);
            var hellFire = Instantiate(monster_Data_Boss.hellfire_Prefab, newPos, Quaternion.identity);
            hellFire.transform.localScale = Vector3.one * monster_Data_Boss.fireFlooring_size;
            hellFire.transform.LookAt(transform.position);
            hellFire.transform.SetParent(prefabRoot.transform);
        }
    }

    public override void Attack3_End()
    {
        Destroy(prefabRoot);
    }

    public override void Attack4()
    {
        prefabRoot = new GameObject();
        var monster_Data_Boss = monster_Data as Monster_Data_Boss;
        for (int i = 0; i < monster_Data_Boss.mindFlooring_number; i++)
        {
            Vector3 newPos = GetRandomNavMeshPoint(transform.position, 0, monster_Data_Boss.mindFlooring_range);
            var mindFlooring = Instantiate(monster_Data_Boss.mindFlooring_Prefab, newPos, Quaternion.identity);
            mindFlooring.transform.localScale = Vector3.one * monster_Data_Boss.mindFlooring_size;
            mindFlooring.transform.LookAt(transform.position);
            mindFlooring.transform.SetParent(prefabRoot.transform);
        }
    }

    public override void Attack4_End()
    {
        Destroy(prefabRoot);
    }

    /// <summary>
    /// 중심에서 일정 반경 내에서 NavMesh 위의 무작위 위치를 반환합니다.
    /// </summary>
    /// <param name="center">중심 위치</param>
    /// <param name="minRadius">최소 반경</param>
    /// <param name="maxRadius">최대 반경</param>
    /// <returns>NavMesh 위의 무작위 위치</returns>
    private Vector3 GetRandomNavMeshPoint(Vector3 center, float minRadius, float maxRadius)
    {
        NavMeshHit hit;
        Vector3 randomPoint = Vector3.zero;

        // NavMesh에서 무작위로 위치를 선택합니다.
        for (int i = 0; i < 10; i++) // 최대 10번 시도
        {
            Vector3 potentialPoint = RandomNavMeshLocation(center, minRadius, maxRadius);
            if (NavMesh.SamplePosition(potentialPoint, out hit, maxRadius, NavMesh.AllAreas))
            {
                randomPoint = hit.position;
                break;
            }
        }

        float distance = Vector3.Distance(center, randomPoint);
        //Debug.Log("Selected Patrol Point Distance: " + distance);

        return randomPoint;
    }

    /// <summary>
    /// NavMesh 내의 무작위 위치를 반환합니다.
    /// </summary>
    /// <param name="center">중심 위치</param>
    /// <param name="minRadius">최소 반경</param>
    /// <param name="maxRadius">최대 반경</param>
    /// <returns>무작위 위치</returns>
    private Vector3 RandomNavMeshLocation(Vector3 center, float minRadius, float maxRadius)
    {
        Vector3 randomDirection = Vector3.zero;

        // 최소 반경과 최대 반경 사이의 거리를 생성
        float randomDistance = Random.Range(minRadius, maxRadius);

        // 무작위 방향으로 이동
        randomDirection = Random.insideUnitSphere * randomDistance + center;
        randomDirection.y = center.y; // y 축을 맞춰 평면 상에서만 이동하도록 합니다.

        return randomDirection;
    }
}
