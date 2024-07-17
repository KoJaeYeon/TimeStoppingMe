using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


[TaskCategory("Monster/Elite2")]
public class Patrol2 : Action
{
    [UnityEngine.Tooltip(
        "The agent has arrived when the destination is less than the specified amount. This distance should be greater than or equal to the NavMeshAgent StoppingDistance.")]
    public SharedFloat arriveDistance = 0.2f;

    // Component references
    public SharedNavmeshAgent navMeshAgent;

    [UnityEngine.Tooltip("The GameObject that the agent is seeking")]
    public SharedVector3 destination;

    public SharedTransform TargetTrans;
    public SharedMonster SharedMonster;

    public override void OnStart()
    {
        
        navMeshAgent.Value.angularSpeed = SharedMonster.Value.monster_Data.AngularSpeed;
#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5
            navMeshAgent.Resume();
#else
        navMeshAgent.Value.isStopped = false;
#endif

        SetDestination(destination.Value);
    }

    public override TaskStatus OnUpdate()
    {
        navMeshAgent.Value.speed = SharedMonster.Value.moveSpeed;
        if (HasArrived())
            return TaskStatus.Success;

        float distance = Vector3.Distance(TargetTrans.Value.position, Owner.transform.position);
        if (distance <= SharedMonster.Value.monster_Data.TrackDistance)
        {
            return TaskStatus.Failure;
        }

        SetDestination(destination.Value);
        return TaskStatus.Running;
    }

    /// <summary>
    /// 길찾기 목적지 설정.
    /// </summary>
    /// <param name="destination">The destination to set.</param>
    /// <returns>목적지가 유효하면 True</returns>
    private bool SetDestination(Vector3 destination)
    {
#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5
            navMeshAgent.Resume();
#else
        navMeshAgent.Value.isStopped = false;
#endif
        return navMeshAgent.Value.SetDestination(destination);
    }

    /// <summary>
    /// 목적지에 도착했는지 확인
    /// </summary>
    /// <returns>목적지에 도착했다면 True</returns>
    private bool HasArrived()
    {
        // 경로가 보류 중인 경우 경로가 아직 계산되지 않았습니다.
        float remainingDistance = (navMeshAgent.Value.pathPending)
            ? float.PositiveInfinity
            : navMeshAgent.Value.remainingDistance;

        return remainingDistance <= arriveDistance.Value;
    }

    private void Stop()
    {
        if (navMeshAgent.Value.hasPath)
        {
#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5
                navMeshAgent.Stop();
#else
            navMeshAgent.Value.isStopped = true;
#endif
        }
    }

    public override void OnEnd()
    {
        Stop();
    }

    public override void OnBehaviorComplete()
    {
        Stop();
    }
}
