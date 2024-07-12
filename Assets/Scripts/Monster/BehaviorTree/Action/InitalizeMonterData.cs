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
            NavMeshAgent.Value = navmeshAgent;
            return TaskStatus.Success;
        }
    }
}
