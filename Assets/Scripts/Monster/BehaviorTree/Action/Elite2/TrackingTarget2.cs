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

[TaskCategory("Monster/Elite2")]
public class TrackingTarget2_Running : Action

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
}
[TaskCategory("Monster/Air")]
public class TrackingTarget2_WithoutNav : Action
{
    public SharedMonster SharedMonster;

    [UnityEngine.Tooltip("The GameObject that the agent is seeking")]
    public SharedTransform TargetTrans;
    private Transform monsterTransform;
    private float angularSpeed;

    public override void OnStart()
    {
        monsterTransform = SharedMonster.Value.transform;
        angularSpeed = SharedMonster.Value.monster_Data.AngularSpeed;
    }

    public override TaskStatus OnUpdate()
    {
        Vector3 direction = (TargetTrans.Value.position - monsterTransform.position).normalized;
        direction.y = 0; // Keep the y component zero to only rotate on the xz plane

        // Rotate towards the target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation.x = 0; // Keep the x component zero to only rotate on the xz plane
        targetRotation.z = 0; // Keep the z component zero to only rotate on the xz plane
        monsterTransform.rotation = Quaternion.RotateTowards(monsterTransform.rotation, targetRotation, angularSpeed * Time.deltaTime);

        // Move towards the target
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.z);
        monsterTransform.position += moveDirection * SharedMonster.Value.moveSpeed * Time.deltaTime;

        return TaskStatus.Success;
    }
}

[TaskCategory("Monster/Air")]
public class TrackingTarget2_Running_WithoutNav : Action
{
    public SharedMonster SharedMonster;

    [UnityEngine.Tooltip("The GameObject that the agent is seeking")]
    public SharedTransform TargetTrans;
    private Transform monsterTransform;
    private float angularSpeed;

    public override void OnStart()
    {
        monsterTransform = SharedMonster.Value.transform;
        angularSpeed = SharedMonster.Value.monster_Data.AngularSpeed;
    }

    public override TaskStatus OnUpdate()
    {
        Vector3 direction = (TargetTrans.Value.position - monsterTransform.position).normalized;
        direction.y = 0; // Keep the y component zero to only rotate on the xz plane

        // Rotate towards the target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation.x = 0; // Keep the x component zero to only rotate on the xz plane
        targetRotation.z = 0; // Keep the z component zero to only rotate on the xz plane
        monsterTransform.rotation = Quaternion.RotateTowards(monsterTransform.rotation, targetRotation, angularSpeed * Time.deltaTime);

        // Move towards the target
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.z);
        monsterTransform.position += moveDirection * SharedMonster.Value.moveSpeed * Time.deltaTime;

        return TaskStatus.Running;
    }
}