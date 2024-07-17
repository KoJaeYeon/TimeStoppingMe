using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster")]
public class ResetCooldown : Action
{
    public SharedFloat SkillLastTime;
    public override TaskStatus OnUpdate()
    {
        SkillLastTime.Value = Time.time;
        return TaskStatus.Success;
    }
}