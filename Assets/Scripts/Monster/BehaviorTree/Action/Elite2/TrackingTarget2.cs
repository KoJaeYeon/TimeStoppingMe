using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/Elite2")]
public class TrackingTarget2 : Action
{
    public SharedMonster SharedMonster;
    // Component references
    public SharedNavmeshAgent navMeshAgent;

    [UnityEngine.Tooltip("The GameObject that the agent is seeking")]
    public SharedTransform TargetTrans;
    public override void OnStart()
    {
        navMeshAgent.Value.angularSpeed = SharedMonster.Value.monster_Data.AngularSpeed;
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
        navMeshAgent.Value.speed = SharedMonster.Value.moveSpeed;
        SetDestination(TargetTrans.Value.position);
        return TaskStatus.Success;
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
}