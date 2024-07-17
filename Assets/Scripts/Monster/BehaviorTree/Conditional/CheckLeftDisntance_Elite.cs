using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Monster")]
public class CheckLeftDisntance_Elite : Conditional
{
    public SharedMonster_Elite SharedMonster_Elite;
    public SharedFloat Skill2Distance;
    public override TaskStatus OnUpdate()
    {
        if(SharedMonster_Elite.Value.overDistance > Skill2Distance.Value)
        {
            SharedMonster_Elite.Value.overDistance = 0;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
