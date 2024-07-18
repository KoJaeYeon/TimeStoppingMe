using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Boom : MonoBehaviour
{
    public float PlayerDamage = 1;
    public float MonsterDamage = 100;
    Dictionary<IAttackable, float> _attackableDic = new Dictionary<IAttackable, float>();
    Dictionary<IAttackable, float> _PattackableDic = new Dictionary<IAttackable, float>();

    public float checkRadius = 500f; // OverlapSphere�� �ݰ�
    public LayerMask layerMask= -1; // OverlapSphere�� �浹�� ������ ���̾�
    public GameObject FX;

    private void Start()
    {
        // OverlapSphere�� ����Ͽ� �ݶ��̴� ����
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, layerMask);
        Debug.Log("playingBoom");
        foreach (var collider in hitColliders)
        {
            var IAttack = collider.GetComponent<IAttackable>();
            if (IAttack != null)
            {
                var player = collider.GetComponent<Player>();
                if (player != null)
                {
                    if (!_PattackableDic.ContainsKey(IAttack))
                    {
                        _PattackableDic.Add(IAttack, Time.time);
                        IAttack.OnTakeDamaged(PlayerDamage);
                    }
                }
                else
                {
                    if (!_attackableDic.ContainsKey(IAttack))
                    {
                        _attackableDic.Add(IAttack, Time.time);
                        IAttack.OnTakeDamaged(MonsterDamage);
                    }
                }
            }
        }

        // ���� ó��
        foreach (var key in new List<IAttackable>(_PattackableDic.Keys))
        {
            float lastTickTime = _PattackableDic[key];
            if (lastTickTime + 1 < Time.unscaledTime)
            {
                if (key.IsUnityNull())
                {
                    _PattackableDic.Remove(key);
                    continue;
                }
                key.OnTakeDamaged(PlayerDamage);
                _PattackableDic[key] = Time.unscaledTime;
            }
        }

        foreach (var key in new List<IAttackable>(_attackableDic.Keys))
        {
            float lastTickTime = _attackableDic[key];
            if (lastTickTime + 1 < Time.unscaledTime)
            {
                if (key.IsUnityNull())
                {
                    _attackableDic.Remove(key);
                    continue;
                }
                key.OnTakeDamaged(MonsterDamage);
                _attackableDic[key] = Time.unscaledTime;
            }
        }
        GameObject Fx = Instantiate(FX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}