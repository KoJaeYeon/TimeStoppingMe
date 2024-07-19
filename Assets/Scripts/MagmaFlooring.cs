using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagmaFlooring : MonoBehaviour
{
    public float PlayerDamage = 1;
    public float MonsterDamage = 15;
    Dictionary<IAttackable,float> _attackableDic = new Dictionary<IAttackable, float>();

    public static float playerLastMagmaTime;

    private void Update()
    {
        foreach (var key in _attackableDic.Keys)
        {
            float lastTickTime = _attackableDic[key];
            if(lastTickTime + 1 < Time.time)
            {
                if (key.IsUnityNull())
                {
                    _attackableDic.Remove(key);
                    continue;
                }
                    
                key.OnTakeDamaged(MonsterDamage);
                _attackableDic[key] = Time.time;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var IAttack = other.GetComponent<IAttackable>();
        if (IAttack != null)
        {
            if(other.GetComponent<Player>())
            {

            }
            else
            {
                _attackableDic.Add(IAttack, Time.time);
                IAttack.OnTakeDamaged(MonsterDamage);
            }
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Player>();
        if(player != null)
        {
            if(playerLastMagmaTime + 1 <= Time.time)
            {
                player.OnTakeDamaged(1);
                playerLastMagmaTime = Time.time;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var IAttack = other.GetComponent<IAttackable>();
        if (IAttack != null)
        {
            if(_attackableDic.ContainsKey(IAttack))
            {
                _attackableDic.Remove(IAttack);
            }            
        }        
    }
}
