using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Monster/Initial")]
public class InitalizeMonterData : Action
{
    public SharedTransform TargetTrans;
    public SharedMonster Monster;
    public SharedNavmeshAgent NavMeshAgent;
    public SharedFloat Speed;
    public SharedFloat AnuglarSpeed;
    public SharedFloat AttackDistance;
    public SharedFloat TrackDistance;
    public SharedFloat SearchAngle;
    public SharedFloat IdleRange_Min;
    public SharedFloat IdleRange_Max;
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

            Speed.Value = monster.monster_Data.MoveSpeed;
            AnuglarSpeed.Value = monster.monster_Data.AngularSpeed;
            AttackDistance.Value = monster.monster_Data.AttackDistance;
            TrackDistance.Value = monster.monster_Data.TrackDistance;
            SearchAngle.Value = monster.monster_Data.Search_Range;
            IdleRange_Min.Value = monster.monster_Data.IdleRange_Min;
            IdleRange_Max.Value = monster.monster_Data.IdleRange_Max;

            NavMeshAgent.Value = navmeshAgent;
            return TaskStatus.Success;
        }
    }
}

[TaskCategory("Monster/Initial")]
public class InitalizeMonterData_Boss : Action
{
    public SharedTransform TargetTrans;
    public SharedMonster Monster;    
    public SharedNavmeshAgent NavMeshAgent;
    public SharedFloat Speed;
    public SharedFloat AnuglarSpeed;
    public SharedFloat AttackDistance;
    public SharedFloat TrackDistance;
    public SharedFloat SearchAngle;
    public SharedFloat IdleRange_Min;
    public SharedFloat IdleRange_Max;

    public SharedFloat Skiil1CoolDown;
    public SharedFloat Skiil2CoolDown;
    public SharedFloat Skiil3CoolDown;
    public SharedFloat Skiil4CoolDown;

    public SharedFloat Skiil1LastTime;
    public SharedFloat Skiil2LastTime;
    public SharedFloat Skiil3LastTime;
    public SharedFloat Skiil4LastTime;

    public SharedFloat Skill1Distance;
    public SharedFloat Skill2Distance;


    public override TaskStatus OnUpdate()
    {
        var targetObject = GameObject.FindGameObjectWithTag("Player");
        var monster = Owner.GetComponent<Monster_Boss>();
        var navmeshAgent = Owner.GetComponent<NavMeshAgent>();
        if (targetObject == null || Monster == null || navmeshAgent == null)
        {
            return TaskStatus.Failure;
        }
        else
        {
            TargetTrans.Value = targetObject.transform;
            Monster.Value = monster;
            Monster.Value.TargetTrans = targetObject.transform;

            
            Speed.Value = monster.monster_Data.MoveSpeed;
            AnuglarSpeed.Value = monster.monster_Data.AngularSpeed;
            AttackDistance.Value = monster.monster_Data.AttackDistance;
            TrackDistance.Value = monster.monster_Data.TrackDistance;
            SearchAngle.Value = monster.monster_Data.Search_Range;
            IdleRange_Min.Value = monster.monster_Data.IdleRange_Min;
            IdleRange_Max.Value = monster.monster_Data.IdleRange_Max;

            var monsterData_Boss = monster.monster_Data as Monster_Data_Boss;
            Skiil1CoolDown.Value = monsterData_Boss.skill_loveshot_Cooldown;
            Skiil2CoolDown.Value = monsterData_Boss.skill_bite_Cooldown;
            Skiil3CoolDown.Value = monsterData_Boss.skill_hellfire_Cooldown;
            Skiil4CoolDown.Value = monsterData_Boss.skill_MindFlooring_Cooldown;

            Skill1Distance.Value = monsterData_Boss.skill_loveshot_Distance;
            Skill2Distance.Value = monsterData_Boss.skill_bite_Distance;

            NavMeshAgent.Value = navmeshAgent;
            return TaskStatus.Success;
        }
    }
}

[TaskCategory("Monster/Initial")]
public class InitalizeMonterData_Elite : Action
{
    public SharedTransform TargetTrans;
    public SharedMonster Monster;
    public SharedMonster_Elite SharedMonster_Elite;
    public SharedNavmeshAgent NavMeshAgent;
    public SharedFloat Speed;
    public SharedFloat AnuglarSpeed;
    public SharedFloat AttackDistance;
    public SharedFloat TrackDistance;
    public SharedFloat SearchAngle;
    public SharedFloat IdleRange_Min;
    public SharedFloat IdleRange_Max;

    public SharedFloat Skiil1LastTime;
    public SharedFloat Skiil2LastTime;

    public SharedFloat Skiil1CoolDown;
    public SharedFloat Skill2CoolDown;

    public SharedFloat Skill1Distance;
    public SharedFloat skill2Distnace;

    public SharedFloat skill2Speed;


    public override TaskStatus OnUpdate()
    {
        var targetObject = GameObject.FindGameObjectWithTag("Player");
        var monster = Owner.GetComponent<Monster_Elite>();
        var navmeshAgent = Owner.GetComponent<NavMeshAgent>();
        if (targetObject == null || Monster == null || navmeshAgent == null)
        {
            return TaskStatus.Failure;
        }
        else
        {
            TargetTrans.Value = targetObject.transform;
            Monster.Value = monster;
            Monster.Value.TargetTrans = targetObject.transform;

            SharedMonster_Elite.Value = monster;

            Speed.Value = monster.monster_Data.MoveSpeed;
            AnuglarSpeed.Value = monster.monster_Data.AngularSpeed;
            AttackDistance.Value = monster.monster_Data.AttackDistance;
            TrackDistance.Value = monster.monster_Data.TrackDistance;
            SearchAngle.Value = monster.monster_Data.Search_Range;
            IdleRange_Min.Value = monster.monster_Data.IdleRange_Min;
            IdleRange_Max.Value = monster.monster_Data.IdleRange_Max;

            var monsterData_Elite = monster.monster_Data as Monster_Data_Elite;
            Skiil1CoolDown.Value = monsterData_Elite.skill_scratch_Cooldown;
            Skill1Distance.Value = monsterData_Elite.skill_scratch_Distance;

            Skill2CoolDown.Value = monsterData_Elite.skill_footWalk_Cooldown;
            skill2Distnace.Value = monsterData_Elite.skill_footWalk_Distance;
            skill2Speed.Value = monsterData_Elite.skill_footWalk_Speed;


            NavMeshAgent.Value = navmeshAgent;
            return TaskStatus.Success;
        }
    }
}