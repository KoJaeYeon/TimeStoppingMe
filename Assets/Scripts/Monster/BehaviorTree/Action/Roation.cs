using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster")]
public class Roation : Action
{
    public SharedTransform targetTrans;
    public SharedFloat angularSpeed;
    public override TaskStatus OnUpdate()
    {
        // ��ǥ ������Ʈ�� ������ ���
        Vector3 direction = (targetTrans.Value.position - transform.position).normalized;

        // ���� ����� ��ǥ ���� ������ ȸ������ ���
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // ������ �ӵ��� ȸ��
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed.Value * Time.deltaTime);
        return TaskStatus.Running;
    }
}
