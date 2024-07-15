using UnityEngine;

public class Projectile_HellFire : Projectile_Monster
{
    bool used;
    private void Awake()
    {
        damage = 1;
        used = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (used == true) return;
        if (other.CompareTag("Monster")) return;

        var IAttackable = other.GetComponent<IAttackable>();
        if (IAttackable != null)
        {
            IAttackable.OnTakeDamaged(damage);
            used = true;
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