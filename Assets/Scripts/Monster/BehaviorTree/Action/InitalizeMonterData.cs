using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/Initalize")]
public class InitalizeMonterData : Action
{
    public SharedTransform TargetTrans;
    public override TaskStatus OnUpdate()
    {
        var targetObject = GameObject.FindGameObjectWithTag("Player");
        if (targetObject == null)
        {
            return TaskStatus.Failure;
        }
        else
        {
            TargetTrans.Value = targetObject.transform;
            return TaskStatus.Success;
        }
    }
}
