namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedMonster : SharedVariable<Monster>
    {
        public static implicit operator SharedMonster(Monster value) { return new SharedMonster { Value = value }; }
    }

    [System.Serializable]
    public class SharedMonster_Elite : SharedVariable<Monster_Elite>
    {
        public static implicit operator SharedMonster_Elite(Monster_Elite value) { return new SharedMonster_Elite { Value = value }; }
    }
}