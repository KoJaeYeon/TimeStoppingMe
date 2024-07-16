using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Monster")]
public class SetPatrolPosition : Action
{
    public SharedVector3 PatrolPosition; // 순찰 위치
    public SharedFloat PatrolRadius_Min; // 최소 반경
    public SharedFloat PatrolRadius; // 최대 반경

    public override TaskStatus OnUpdate()
    {
        PatrolPosition.Value = GetRandomNavMeshPoint(transform.position, PatrolRadius_Min.Value, PatrolRadius.Value);
        return TaskStatus.Success;
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

        // HardWalkable 영역을 제외한 마스크 생성 (HardWalkable이 9999로 설정됨)
        int hardWalkableArea = 9999;
        int walkableMask = NavMesh.AllAreas & ~(1 << hardWalkableArea); // HardWalkable 비트 제거

        // NavMesh에서 무작위로 위치를 선택합니다.
        for (int i = 0; i < 10; i++) // 최대 10번 시도
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
