namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedMonster : SharedVariable<Monster>
    {
        public static implicit operator SharedMonster(Monster value) { return new SharedMonster { Value = value }; }
    }
}