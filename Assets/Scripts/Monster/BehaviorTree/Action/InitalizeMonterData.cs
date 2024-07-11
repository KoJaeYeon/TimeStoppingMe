using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/Action/Initalize")]
public class InitalizeMonterData : Action
{
    public override TaskStatus OnUpdate()
    {
        Debug.Log("InitalizeMonterData_OnUpdate");
        return TaskStatus.Success;
    }
}
