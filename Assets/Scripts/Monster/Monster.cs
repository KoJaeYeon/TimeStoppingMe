using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Monster : MonoBehaviour, IAttackable
{
    public Transform TargetTrans;
    public float health;
    public Monster_DATA monster_Data;
    private List<Debuff> activeDebuffs = new List<Debuff>();
    public BehaviorTree bt;


    public bool IsBurned { get; set; } = false;
    public bool IsPosioned { get; set; } = false;
    [Header("MonsterBt_Version2")]
    public float moveSpeed;

    public virtual void Awake()
    {
        health = monster_Data.MaxHealth;
        moveSpeed = monster_Data.MoveSpeed;
    }

    private void Update()
    {
        CalculateDebuff();
        var variable = bt.GetVariable("Speed");
        Debug.Log(variable);
    }

    private void Start()
    {
        
    }

    #region Skill
    public virtual void Attack1() { }
    public virtual void Attack1_End() { }
    public virtual void Attack2() { }
    public virtual void Attack2_End() { }
    public virtual void Attack3() { }
    public virtual void Attack3_End() { }
    public virtual void Attack4() { }
    public virtual void Attack4_End() { }
    #endregion

    public void CalculateDebuff()
    {
        for (int i = activeDebuffs.Count - 1; i >= 0; i--)
        {
            if (activeDebuffs[i].IsEffectOver())
            {
                activeDebuffs[i].RemoveEffect(gameObject);
                activeDebuffs.RemoveAt(i);
            }
        }
    }

    public void OnTakeBuffed<T>(BuffType buffType, T buffed) where T : Buff
    {
        // 버프 처리 로직
    }

    public void OnTakeDamaged<T>(T damage)
    {
        if (damage is int)
        {
            int _dmg = (int)Convert.ChangeType(damage, typeof(int));
            health -= (float)_dmg;
        }
        if (damage is float)
        {
            float _dmg = (float)Convert.ChangeType(damage, typeof(float));
            health -= _dmg;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTakeDebuffed<T>(DebuffType debuffType, T debuff) where T : Debuff
    {
        bool debuffExists = false;
        foreach (var activeDebuff in activeDebuffs)
        {
            if (activeDebuff.GetType() == debuff.GetType())
            {
                debuffExists = true;
                activeDebuff.endTime = Time.time + debuff.Duration; // 기존 효과의 지속시간 갱신
                break;
            }
        }

        if (!debuffExists)
        {
            activeDebuffs.Add(debuff);
            debuff.ApplyEffect(gameObject);
            StartCoroutine(HandleDebuff(debuff));
        }
    }

    private IEnumerator HandleDebuff(Debuff debuff)
    {
        while (!debuff.IsEffectOver())
        {
            debuff.ApplyEffect(gameObject);
            yield return new WaitForSeconds(debuff.TickInterval);
        }

        debuff.RemoveEffect(gameObject);
        activeDebuffs.Remove(debuff);
    }

    private void OnDrawGizmos()
    {
        DrawArc();
    }

    public virtual void DrawArc()
    {
        Handles.color = new Color(1, 1, 1, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, monster_Data.Search_Range / 2, monster_Data.AttackDistance);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -monster_Data.Search_Range / 2, monster_Data.AttackDistance);
    }
}
