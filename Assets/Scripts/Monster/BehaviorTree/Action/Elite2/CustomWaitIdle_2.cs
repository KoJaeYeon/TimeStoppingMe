using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/Elite2")]
public class CustomWaitIdle_2 : Action
{
    public SharedFloat waitTime;
    private float startTime;

    public SharedTransform TargetTrans;
    public SharedMonster SharedMonster;

    float trackDistance;

    public override void OnStart()
    {
        startTime = Time.time;
        trackDistance = SharedMonster.Value.monster_Data.TrackDistance;
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - startTime >= waitTime.Value)
        {
            return TaskStatus.Success;
        }
        float distance = Vector3.Distance(Owner.gameObject.transform.position, TargetTrans.Value.position);
        if (distance <= trackDistance)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Running;
    }
}
