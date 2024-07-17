using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Monster/Initial")]
public class InitalizeMonterData_Elite_2 : Action
{
    public SharedTransform TargetTrans;
    public SharedMonster Monster;
    public SharedNavmeshAgent NavMeshAgent;
    public override TaskStatus OnUpdate()
    {
        var targetObject = GameObject.FindGameObjectWithTag("Player");
        var monster = Owner.GetComponent<Monster>();
        var navmeshAgent = Owner.GetComponent<NavMeshAgent>();
        if (targetObject == null || monster == null || navmeshAgent == null)
        {
            return TaskStatus.Failure;
        }
        else
        {
            TargetTrans.Value = targetObject.transform;
            Monster.Value = monster;
            Monster.Value.TargetTrans = targetObject.transform;

            NavMeshAgent.Value = navmeshAgent;
            return TaskStatus.Success;
        }
    }
}