using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster")]
public class DetectTargetDistance : Action
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

        //플레이어가 은신이면 몬스터의 상태를 Idle로 바꾼다
        //은신조건 if()

        if(distance <= AttackDistance.Value)
        {
            OwnerMonsterState.Value = MonsterState.Attack;
            LastTrackedTime.Value = Time.time;
        }
        else if(distance <= TrackDistance.Value)
        {
            OwnerMonsterState.Value = MonsterState.Tracking;
            LastTrackedTime.Value = Time.time;
        }
        else
        {
            //if (Time.time - LastTrackedTime.Value <= 2 )
            //{
            //    OwnerMonsterState.Value = MonsterState.Tracking;
            //}
            if(OwnerMonsterState.Value == MonsterState.Attack || OwnerMonsterState.Value == MonsterState.Tracking)
            {
                OwnerMonsterState.Value = MonsterState.Tracking;
            }
            else
            {
                OwnerMonsterState.Value = MonsterState.Idle;
            }

        }
        return TaskStatus.Success;
    }
}
