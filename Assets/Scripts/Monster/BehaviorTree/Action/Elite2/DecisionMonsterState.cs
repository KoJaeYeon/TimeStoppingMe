using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Monster/Elite2")]
public class DecisionMonsterState : Action
{
    public SharedTransform TargetTrans;
    public SharedMonster SharedMonster;
    public SharedMonsterState SharedMonsterState;

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

            return TaskStatus.Running;
        }
    }
}
