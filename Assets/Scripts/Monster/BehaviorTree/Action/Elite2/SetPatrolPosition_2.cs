using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Monster/Elite2")]
public class SetPatrolPosition_2 : Action
{
    public SharedMonster SharedMonster;
    public SharedFloat PatrolRadius; // �ִ� �ݰ�
    public SharedVector3 PatrolPosition;

    private float PatrolRadius_Min;
    private float PatrolRadius_Max;

    public override void OnStart()
    {
        PatrolRadius_Min = SharedMonster.Value.monster_Data.IdleRange_Min;
        PatrolRadius_Max = SharedMonster.Value.monster_Data.IdleRange_Max;
    }


    public override TaskStatus OnUpdate()
    {
        PatrolPosition.Value = GetRandomNavMeshPoint(transform.position, PatrolRadius_Min, PatrolRadius.Value);
        return TaskStatus.Success;
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

        // HardWalkable ������ ������ ����ũ ���� (HardWalkable�� 9999�� ������)
        int hardWalkableArea = 9999;
        int walkableMask = NavMesh.AllAreas & ~(1 << hardWalkableArea); // HardWalkable ��Ʈ ����

        // NavMesh���� �������� ��ġ�� �����մϴ�.
        for (int i = 0; i < 10; i++) // �ִ� 10�� �õ�
        {
            Vector3 potentialPoint = RandomNavMeshLocation(center, minRadius, maxRadius);
            if (NavMesh.SamplePosition(potentialPoint, out hit, maxRadius, walkableMask))
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
