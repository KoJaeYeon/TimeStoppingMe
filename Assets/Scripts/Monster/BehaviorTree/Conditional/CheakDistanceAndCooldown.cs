using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Monster")]
public class CheakCooldown : Conditional
{
    public SharedFloat SkilCooldown;
    public SharedFloat SkillLastTime;
    public override TaskStatus OnUpdate()
    {
        float nowTime = Time.time;
        return (nowTime - SkillLastTime.Value >= SkilCooldown.Value)
            ? TaskStatus.Success
            : TaskStatus.Failure;
    }
}

[TaskCategory("Monster")]
public class CheakDistance : Conditional
{
    public SharedTransform TargetTrans;
    public SharedFloat SkillDistance;
    public override TaskStatus OnUpdate()
    {
        Transform targetTrans = TargetTrans.Value;
        Transform ownerTrans = Owner.transform;

        float distance = Vector3.Distance(ownerTrans.position, targetTrans.position);
        return (distance <= SkillDistance.Value)
            ? TaskStatus.Success
            : TaskStatus.Running;
    }
}