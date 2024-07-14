using UnityEngine;

public class Boss_AttackCollision : MonoBehaviour
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
            // 각도 계산
            Vector3 directionToTarget = (other.transform.position - ownerTrans.position).normalized;
            float angle = Vector3.Angle(ownerTrans.forward, directionToTarget);

            // 각도가 MaxAngle보다 크면 실패 반환
            if (angle > maxAngle / 2)
            {
                return;
            }

            var IAttack = other.GetComponent<IAttackable>();
            if (IAttack != null)
            {
                IAttack.OnTakeDamaged(damage);

                //보스 체력회복
                var Monster = transform.parent.GetComponent<Monster_Boss>();
                Monster.RestoreHealth();
            }
            else
            {
                Debug.LogAssertion("Player IAttack is null");
            }
            gameObject.SetActive(false);
        }
    }
}