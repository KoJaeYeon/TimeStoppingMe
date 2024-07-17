using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Monster")]
public class CheckLeftDisntance_Elite : Conditional
{
    public SharedMonster SharedMonster_Elite;
    public override TaskStatus OnUpdate()
    {        
        if(SharedMonster_Elite.Value is Monster_Elite)
        {
            var monster = SharedMonster_Elite.Value as Monster_Elite;
            var data = monster.monster_Data as Monster_Data_Elite;
            if (monster.overDistance > data.skill_footWalk_Distance)
            {
                monster.overDistance = 0;
                return TaskStatus.Success;
            }
        }
        else if (SharedMonster_Elite.Value is Monster_Elite_2)
        {
            var monster = SharedMonster_Elite.Value as Monster_Elite_2;
            var data = monster.monster_Data as Monster_Data_Elite_2;

            if (monster.overDistance > data.skill_footWalk_Distance)
            {
                monster.overDistance = 0;
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }
}
