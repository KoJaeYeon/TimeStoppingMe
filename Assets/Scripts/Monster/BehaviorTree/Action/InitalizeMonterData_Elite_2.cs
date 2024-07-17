using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Monster/Initial")]
public class InitalizeMonterData_Elite_2 : Action
{
    public SharedTransform TargetTrans;
    public SharedMonster Monster;
    public SharedNavmeshAgent NavMeshAgent;

    public SharedFloat[] SkillCooldown;
    public SharedFloat[] SkillLastTiem;
    public override TaskStatus OnUpdate()
    {
        var targetObject = GameObject.FindGameObjectWithTag("Player");
        var monster = Owner.GetComponent<Monster>();
        var navmeshAgent = Owner.GetComponent<NavMeshAgent>();
        if (targetObject == null || monster == null || navmeshAgent == null)
        {
            return TaskStatus.Failure;
        }
        else
        {
            TargetTrans.Value = targetObject.transform;
            Monster.Value = monster;
            Monster.Value.TargetTrans = targetObject.transform;

            var monsterData = monster.monster_Data as Monster_Data_Elite_2;
            SkillCooldown[0].Value = monsterData.skill_bite_Cooldown;
            SkillCooldown[1].Value = monsterData.skill_footWalk_Cooldown;

            SkillLastTiem[0].Value = monsterData.skill_bite_Cooldown * -1;
            SkillLastTiem[1].Value = monsterData.skill_footWalk_Cooldown * -1;

            NavMeshAgent.Value = navmeshAgent;
            return TaskStatus.Success;
        }
    }
}