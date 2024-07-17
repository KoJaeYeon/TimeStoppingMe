using UnityEngine;

public class WaterRoad : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var IAttackable = other.GetComponent<IAttackable>();
        if (IAttackable != null)
        {
            IAttackable.OnTakeDebuffed(DebuffType.Slow, new Debuff_Slow(0.5f));
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