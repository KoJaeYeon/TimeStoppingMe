using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster")]
public class DetectIdle : Action
{
    public SharedMonsterState OwnerMonsterState;
    public SharedTransform TargetTrans;
    public SharedFloat LastTrackedTime;

    public SharedFloat Speed;
    public SharedFloat AnularSpeed;
    public SharedFloat AttackDistance;
    public SharedFloat TrackDistance;

    public override TaskStatus OnUpdate()
    {
        Vector3 ownerPos = Owner.transform.position;
        var targetTrans = TargetTrans.Value;
        Vector3 targetPos = targetTrans.position;

        float distance = Vector3.Distance(ownerPos, targetPos);
        if (distance <= TrackDistance.Value)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}
