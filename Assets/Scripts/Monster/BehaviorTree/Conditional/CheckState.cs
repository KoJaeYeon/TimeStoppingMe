using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Monster/CheakState")]
public class CheakState : Conditional
{
    //public SharedUnitState UnitState;
    public MonsterState cheakState;
    public override TaskStatus OnUpdate()
    {
        //[TODO_�翬]
        return (cheakState == cheakState)
            ? TaskStatus.Success
            : TaskStatus.Failure;
    }
}
