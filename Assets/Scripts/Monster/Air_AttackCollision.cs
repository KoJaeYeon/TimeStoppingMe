using UnityEngine;

public class Air_AttackCollision : MonoBehaviour
{
    int damage;
    Monster_Air monster;
    private void Awake()
    {
        monster = transform.parent.GetComponent<Monster_Air>();
        damage = monster.monster_Data.Damage;
    }

    private void OnEnable()
    {
        monster.isgraped = false;

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            monster.isgraped = true;
        }
    }
}