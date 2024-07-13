using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour,IAttackable
{
    public Transform TargetTrans;

    public float health;

    public Monster_DATA monster_Data;

    private void Awake()
    {
        health = monster_Data.MaxHealth;
    }

    public virtual void Attack0()
    {

    }

    public void OnTakeBuffed<T>(T buffed)
    {
        throw new System.NotImplementedException();
    }

    public void OnTakeDamaged<T>(T damage)
    {
        float _dmg = (float)Convert.ChangeType(damage, typeof(float));

        health -= _dmg;
    }

    public void OnTakeDebuffed<T>(T debuff) where T : Debuff
    {
        throw new System.NotImplementedException();
    }
}
