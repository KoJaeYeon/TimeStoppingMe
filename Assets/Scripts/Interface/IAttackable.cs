using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IAttackable
{
    public void OnTakeDamaged<T>(T damage);
    public void OnTakeDebuffed<T>(DebuffType debuffType,T debuff) where T : Debuff;
    public void OnTakeBuffed<T>(BuffType buffType, T buff) where T : Buff;
    
}
