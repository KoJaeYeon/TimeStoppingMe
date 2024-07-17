using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Monster/Elite2")]
public class Skill_FootWalk : Action
{
    public SharedMonster SharedMonster;
    public SharedNavmeshAgent navMeshAgent;

    public override TaskStatus OnUpdate()
    {
        navMeshAgent.Value.speed = SharedMonster.Value.moveSpeed * 2;
        return TaskStatus.Running;
    }
}
