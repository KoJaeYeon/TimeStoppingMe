using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_AttackCollision : MonoBehaviour
{
    int damage;
    private void Awake()
    {
        var Monster = transform.parent.GetComponent<Monster>();
        damage = Monster.monster_Data.Damage;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {            
            var IAttack = other.GetComponent<IAttackable>();
            if (IAttack != null)
            {
                IAttack.OnTakeDamaged(damage);
            }
            else
            {
                Debug.LogAssertion("Player IAttack is null");
            }
            gameObject.SetActive(false);
        }
    }
}
