using BehaviorDesigner.Runtime;
using UnityEngine.AI;

[System.Serializable]
public class SharedNavmeshAgent : SharedVariable<NavMeshAgent>
{
    public static implicit operator SharedNavmeshAgent(NavMeshAgent value) { return new SharedNavmeshAgent { Value = value }; }
}
