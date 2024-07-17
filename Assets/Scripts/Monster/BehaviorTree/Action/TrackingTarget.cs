using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster")]
public class TrackingTarget : Action
{
    [UnityEngine.Tooltip("The speed of the agent")]
    public SharedFloat speed = 10;

    [UnityEngine.Tooltip("The angular speed of the agent")]
    public SharedFloat angularSpeed;

    // Component references
    public SharedNavmeshAgent navMeshAgent;

    [UnityEngine.Tooltip("The GameObject that the agent is seeking")]
    public SharedTransform TargetTrans;
    [UnityEngine.Tooltip(
     "The agent has arrived when the destination is less than the specified amount. This distance should be greater than or equal to the NavMeshAgent StoppingDistance.")]
    public SharedFloat AttackDistance;
    public SharedFloat TrackDistance;
    public SharedFloat LastTrackedTime;
    /// <summary>
    /// Allow pathfinding to resume.
    /// </summary>
    public override void OnStart()
    {
        navMeshAgent.Value.speed = speed.Value;
        navMeshAgent.Value.angularSpeed = angularSpeed.Value;
#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5
            navMeshAgent.Resume();
#else
        navMeshAgent.Value.isStopped = false;
#endif
        SetDestination(TargetTrans.Value.position);
    }

    // Seek the destination. Return success once the agent has reached the destination.
    // Return running if the agent hasn't reached the destination yet
    public override TaskStatus OnUpdate()
    {

        if (HasArrived())
        {
            Debug.Log("HasArrived체크");
            return TaskStatus.Success;
        }
        if(navMeshAgent.Value.remainingDistance > TrackDistance.Value)
        {
            if (Time.time - LastTrackedTime.Value > 10000)
            {
                return TaskStatus.Failure;
            }           
        }
        else
        {
            LastTrackedTime.Value = Time.time;
        }

        SetDestination(TargetTrans.Value.position);
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
        float distance = Vector3.Distance(Owner.transform.position, TargetTrans.Value.position);

        return distance <= AttackDistance.Value;
    }

    /// <summary>
    /// 길찾기 중지.
    /// </summary>
    private void Stop()
    {
        if (navMeshAgent.Value.hasPath)
        {
#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5
                navMeshAgent.Value.Stop();
#else
            navMeshAgent.Value.isStopped = true;
#endif
        }
    }

    /// <summary>
    /// The task has ended. Stop moving.
    /// </summary>
    public override void OnEnd()
    {
        Stop();
    }

    /// <summary>
    /// The behavior tree has ended. Stop moving.
    /// </summary>
    public override void OnBehaviorComplete()
    {
        Stop();
    }
}
