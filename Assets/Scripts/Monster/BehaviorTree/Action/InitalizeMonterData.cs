using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Monster")]
public class InitalizeMonterData : Action
{
    public SharedTransform TargetTrans;
    public SharedMonster Monster;
    public SharedNavmeshAgent NavMeshAgent;
    public SharedFloat Speed;
    public SharedFloat AnuglarSpeed;
    public SharedFloat AttackDistance;
    public SharedFloat TrackDistance;
    public override TaskStatus OnUpdate()
    {
        var targetObject = GameObject.FindGameObjectWithTag("Player");
        var monster = Owner.GetComponent<Monster>();
        var navmeshAgent = Owner.GetComponent<NavMeshAgent>();
        if (targetObject == null || Monster == null || navmeshAgent == null)
        {
            return TaskStatus.Failure;
        }
        else
        {
            TargetTrans.Value = targetObject.transform;
            Monster.Value = monster;
            Monster.Value.TargetTrans = targetObject.transform;

            AttackDistance.Value = monster.AttackDistance;
            TrackDistance.Value = monster.TrackDistance;
            NavMeshAgent.Value = navmeshAgent;
            return TaskStatus.Success;
        }
    }
}
