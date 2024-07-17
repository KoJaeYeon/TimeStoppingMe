using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Monster")]
public class CheckCooldown : Conditional
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

[TaskCategory("Monster/Elite")]
public class CheckALLCooldown : Conditional
{
    public SharedFloat Skil1Cooldown;
    public SharedFloat Skill1LastTime;

    public SharedFloat Skil2Cooldown;
    public SharedFloat Skill2LastTime;
    public override TaskStatus OnUpdate()
    {
        float nowTime = Time.time;
        if(nowTime - Skill1LastTime.Value >= Skil1Cooldown.Value)
        {
            return TaskStatus.Success;
        }
        else if(nowTime - Skill2LastTime.Value >= Skil2Cooldown.Value)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }
}

[TaskCategory("Monster")]
public class CheckDistance : Conditional
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

[TaskCategory("Monster")]
public class CheckDistance_Fail : Conditional
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
            : TaskStatus.Failure;
    }
}