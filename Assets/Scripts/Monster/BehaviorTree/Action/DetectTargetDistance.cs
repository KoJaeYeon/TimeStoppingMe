using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster")]
public class DetectTargetDistance : Action
{
    public SharedMonsterState OwnerMonsterState;
    public SharedTransform TargetTrans;
    public SharedFloat LastTrackedTime;
    //[TODO_J] 몬스터 정보 받아오는 스크립트 만들어야됨
    public SharedMonster Monster;

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

        var monster = Monster.Value;

        if(distance <= monster.AttackDistance)
        {
            OwnerMonsterState.Value = MonsterState.Attack;
            LastTrackedTime.Value = Time.time;
        }
        else if(distance <= monster.TrackDistance)
        {
            OwnerMonsterState.Value = MonsterState.Tracking;
            LastTrackedTime.Value = Time.time;
        }
        else
        {
            if (Time.time - LastTrackedTime.Value <= 2 )
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
