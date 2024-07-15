using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Boss : Monster
{
    public Transform[] LaunchTrans; // ���� �߻� ��ġ�� ������ �迭

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

        // ù ��° �߻� ��ġ: Ÿ���� ���� ����
        SpawnProjectile(LaunchTrans[0], TargetTrans.position, monster_Data_Boss);

        // �� ��° �߻� ��ġ: Ÿ���� ���� ���� 30�� ������
        Vector3 leftDirection = Quaternion.Euler(0, -30, 0) * (TargetTrans.position - LaunchTrans[1].position);
        SpawnProjectile(LaunchTrans[1], LaunchTrans[1].position + leftDirection, monster_Data_Boss);

        // �� ��° �߻� ��ġ: Ÿ���� ���� ������ 30�� ������
        Vector3 rightDirection = Quaternion.Euler(0, 30, 0) * (TargetTrans.position - LaunchTrans[2].position);
        SpawnProjectile(LaunchTrans[2], LaunchTrans[2].position + rightDirection, monster_Data_Boss);
    }

    private void SpawnProjectile(Transform launchTransform, Vector3 targetPosition, Monster_Data_Boss monsterData)
    {
        var projecile = Instantiate(projectile_Prefab, launchTransform.position, Quaternion.identity);
        var projectile_Loveshot = projecile.GetComponent<Projectile_LoveShot>();

        // Ÿ�� ���� ���
        Vector3 direction = (targetPosition - launchTransform.position).normalized;

        // ������ ������ �ʱ�ȭ
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
    /// �߽ɿ��� ���� �ݰ� ������ NavMesh ���� ������ ��ġ�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="center">�߽� ��ġ</param>
    /// <param name="minRadius">�ּ� �ݰ�</param>
    /// <param name="maxRadius">�ִ� �ݰ�</param>
    /// <returns>NavMesh ���� ������ ��ġ</returns>
    private Vector3 GetRandomNavMeshPoint(Vector3 center, float minRadius, float maxRadius)
    {
        NavMeshHit hit;
        Vector3 randomPoint = Vector3.zero;

        // NavMesh���� �������� ��ġ�� �����մϴ�.
        for (int i = 0; i < 10; i++) // �ִ� 10�� �õ�
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
    /// NavMesh ���� ������ ��ġ�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="center">�߽� ��ġ</param>
    /// <param name="minRadius">�ּ� �ݰ�</param>
    /// <param name="maxRadius">�ִ� �ݰ�</param>
    /// <returns>������ ��ġ</returns>
    private Vector3 RandomNavMeshLocation(Vector3 center, float minRadius, float maxRadius)
    {
        Vector3 randomDirection = Vector3.zero;

        // �ּ� �ݰ�� �ִ� �ݰ� ������ �Ÿ��� ����
        float randomDistance = Random.Range(minRadius, maxRadius);

        // ������ �������� �̵�
        randomDirection = Random.insideUnitSphere * randomDistance + center;
        randomDirection.y = center.y; // y ���� ���� ��� �󿡼��� �̵��ϵ��� �մϴ�.

        return randomDirection;
    }
}
