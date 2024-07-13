using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public void OnTakeDamaged<T>(T damage);
    public void OnTakeDebuffed<T>(T debuff) where T : Debuff;
    public void OnTakebuffed<T>(T buffed);
    
}
