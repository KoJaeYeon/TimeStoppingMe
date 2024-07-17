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
        // 목표 오브젝트의 방향을 계산
        Vector3 direction = (targetTrans.Value.position - transform.position).normalized;

        // 현재 방향과 목표 방향 사이의 회전각을 계산
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 일정한 속도로 회전
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed.Value * Time.deltaTime);
        return TaskStatus.Running;
    }
}
