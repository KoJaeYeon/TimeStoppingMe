using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour,IAttackable
{
    public Transform TargetTrans;

    public float health;

    public Monster_DATA monster_Data;

    public virtual void Awake()
    {
        health = monster_Data.MaxHealth;
    }

    #region Skill
    public virtual void Attack1()
    {

    }

    public virtual void Attack1_End()
    {

    }
    public virtual void Attack2()
    {

    }

    public virtual void Attack2_End()
    {

    }
    public virtual void Attack3()
    {

    }

    public virtual void Attack3_End()
    {

    }
    public virtual void Attack4()
    {

    }

    public virtual void Attack4_End()
    {

    }
    #endregion
    public void OnTakeBuffed<T>(T buffed)
    {
        throw new System.NotImplementedException();
    }

    public void OnTakeDamaged<T>(T damage)
    {
        if(damage is float)
        {
            float _dmg = (float)Convert.ChangeType(damage, typeof(float));

            health -= _dmg;
        }
    }

    public void OnTakeDebuffed<T>(T debuff) where T : Debuff
    {
        throw new System.NotImplementedException();
    }
}
