using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/Elite2")]
public class DecisionMonsterState : Action
{
    public SharedTransform TargetTrans;
    public SharedMonster SharedMonster;
    public SharedMonsterState SharedMonsterState;

    public override TaskStatus OnUpdate()
    {
        var monsterData = SharedMonster.Value.monster_Data;
        float distance = Vector3.Distance(TargetTrans.Value.position, Owner.transform.position);

        if(distance < monsterData.TrackDistance)
        {
            SharedMonsterState.Value = MonsterState.Tracking;
        }


        return TaskStatus.Running;
    }
}

[TaskCategory("Monster/Air")]
public class DecisionMonsterState_Air : Action
{
    public SharedTransform TargetTrans;
    public SharedMonster SharedMonster;
    public SharedMonsterState SharedMonsterState;
    public SharedNavmeshAgent SharedNavmeshAgent;

    public override TaskStatus OnUpdate()
    {
        var monsterData = SharedMonster.Value.monster_Data;
        float distance = Vector3.Distance(TargetTrans.Value.position, Owner.transform.position);

        if (distance < monsterData.TrackDistance)
        {
            SharedMonsterState.Value = MonsterState.Tracking;
            SharedNavmeshAgent.Value.enabled = false;
        }


        return TaskStatus.Running;
    }
}
