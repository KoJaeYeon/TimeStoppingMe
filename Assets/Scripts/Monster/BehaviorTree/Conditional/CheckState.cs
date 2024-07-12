using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Monster/CheakState")]
public class CheakState : Conditional
{
    public SharedMonsterState MonsterState;
    public MonsterState cheakState;
    public override TaskStatus OnUpdate()
    {        
        return (MonsterState.Value == cheakState)
            ? TaskStatus.Success
            : TaskStatus.Failure;
    }
}
