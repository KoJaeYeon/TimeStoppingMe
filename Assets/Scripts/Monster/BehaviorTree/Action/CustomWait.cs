using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster")]
public class CustomWaitIdle : Action
{
    public SharedFloat waitTime;
    private float startTime;

    public SharedTransform TargetTrans;
    public SharedFloat TrackDistnace;

    public override void OnStart()
    {
        startTime = Time.time;
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - startTime >= waitTime.Value)
        {
            return TaskStatus.Success;
        }
        float distance = Vector3.Distance(Owner.gameObject.transform.position, TargetTrans.Value.position);
        if(distance <= TrackDistnace.Value)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Running;
    }
}