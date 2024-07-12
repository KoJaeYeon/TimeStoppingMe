namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedMonsterState : SharedVariable<MonsterState>
    {
        public static implicit operator SharedMonsterState(MonsterState value) { return new SharedMonsterState { Value = value }; }
    }
}