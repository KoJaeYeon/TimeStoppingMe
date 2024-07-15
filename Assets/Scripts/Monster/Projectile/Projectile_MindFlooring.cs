using UnityEngine;

public class Projectile_MindFlooring : Projectile_Monster
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster")) return;

        var IAttackable = other.GetComponent<IAttackable>();
        if (IAttackable != null)
        {
            IAttackable.OnTakeDebuffed(DebuffType.Mind,new Debuff_Mind());
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                Debug.LogAssertion("Player IAttack is null");
            }
        }

        Debug.Log("Collsion");
    }
}