using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Monster")]
public class CheckAngleDinstance : Conditional
{
    public SharedTransform TargetTrans;
    public SharedFloat MaxAngle;  // �ִ� ��� ����
    public override TaskStatus OnUpdate()
    {
        var ownerTrans = Owner.gameObject.transform;

        // ���� ���
        Vector3 directionToTarget = (TargetTrans.Value.position - ownerTrans.position).normalized;
        float angle = Vector3.Angle(ownerTrans.forward, directionToTarget);
        // ������ MaxAngle���� ũ�� ���� ��ȯ
        if (angle > MaxAngle.Value/2)
        {
            return TaskStatus.Failure;
        }

        // �Ÿ��� ������ ��� ������ �����ϸ� ���� ��ȯ
        return TaskStatus.Success;
    }
}
