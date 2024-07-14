using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Melee_AttackCollision : MonoBehaviour
{
    int damage;
    float maxAngle;
    private void Awake()
    {
        var Monster = transform.parent.GetComponent<Monster>();
        damage = Monster.monster_Data.Damage;
        maxAngle = Monster.monster_Data.Search_Range;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var ownerTrans = transform;
            // ���� ���
            Vector3 directionToTarget = (other.transform.position - ownerTrans.position).normalized;
            float angle = Vector3.Angle(ownerTrans.forward, directionToTarget);

            // ������ MaxAngle���� ũ�� ���� ��ȯ
            if (angle > maxAngle / 2)
            {
                return;
            }

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