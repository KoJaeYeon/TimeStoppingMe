using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/Air")]
public class CheckGrap : Conditional
{
    public SharedMonster SharedMonster;
    public override TaskStatus OnUpdate()
    {
        var monster = SharedMonster.Value as Monster_Air;
        return (monster.isgraped == true)
            ? TaskStatus.Success
            : TaskStatus.Failure;
    }
}
