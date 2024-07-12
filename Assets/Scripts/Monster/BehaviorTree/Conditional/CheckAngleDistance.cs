using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Monster")]
public class CheckAngleDinstance : Conditional
{
    public SharedTransform TargetTrans;
    public SharedFloat AttackDistance;
    public SharedFloat MaxAngle;  // 최대 허용 각도
    public override TaskStatus OnUpdate()
    {
        var ownerTrans = Owner.gameObject.transform;
        float distance = Vector3.Distance(ownerTrans.position, TargetTrans.Value.position);

        if(distance > AttackDistance.Value)
        {
            return TaskStatus.Failure;
        }

        // 각도 계산
        Vector3 directionToTarget = (TargetTrans.Value.position - ownerTrans.position).normalized;
        float angle = Vector3.Angle(ownerTrans.forward, directionToTarget);
        // 각도가 MaxAngle보다 크면 실패 반환
        if (angle > MaxAngle.Value)
        {
            return TaskStatus.Failure;
        }

        // 거리와 각도가 모두 조건을 만족하면 성공 반환
        return TaskStatus.Success;
    }
}
