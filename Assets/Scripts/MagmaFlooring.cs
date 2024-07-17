using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagmaFlooring : MonoBehaviour
{
    public float PlayerDamage = 1;
    public float MonsterDamage = 15;
    Dictionary<IAttackable,float> _attackableDic = new Dictionary<IAttackable, float>();
    Dictionary<IAttackable, float> _PattackableDic = new Dictionary<IAttackable, float>();

    private void Update()
    {
        foreach (var key in _PattackableDic.Keys)
        {
            float lastTickTime = _PattackableDic[key];
            if (lastTickTime + 1 < Time.time)
            {
                if (key.IsUnityNull())
                {
                    _PattackableDic.Remove(key);
                    continue;
                }
                key.OnTakeDamaged(PlayerDamage);
                _PattackableDic[key] = Time.time;
            }
        }

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
            var player = other.GetComponent<Player>();
            if(player != null)
            {
                _PattackableDic.Add(IAttack, Time.time);
                IAttack.OnTakeDamaged(PlayerDamage);
            }
            else
            {
                _attackableDic.Add(IAttack, Time.time);
                IAttack.OnTakeDamaged(MonsterDamage);
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
            else if(_PattackableDic.Remove(IAttack))
            {
                _PattackableDic.Remove(IAttack);
            }
            
        }
        
    }
}
