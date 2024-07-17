using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Slow : Debuff
{
    private float originalMoveSpeed;
    private float slowPercentage;
    private bool isApplied = false;

    public Debuff_Slow(float slowPercentage) : base(0.1f, 0, 0) // 지속 시간 없음
    {
        this.slowPercentage = slowPercentage;
    }

    public override void ApplyEffect(GameObject target)
    {
        if (isApplied) return;

        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            originalMoveSpeed = player.MoveSpeed;
            player.MoveSpeed *= (1 - slowPercentage);
            isApplied = true;
        }
        else
        {
            Monster monster = target.GetComponent<Monster>();
            if (monster != null)
            {
                monster.bt.SetVariableValue("Speed", monster.monster_Data.MoveSpeed/2);
                isApplied = true;
            }
        }
    }

    public override void RemoveEffect(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if(player != null)
        {
            Debug.Log(player.MoveSpeed);
            player.MoveSpeed = originalMoveSpeed;
            isApplied = false;
        }
        else
        {
            Monster monster = target.GetComponent<Monster>();
            if (monster != null)
            {
                monster.bt.SetVariableValue("Speed", monster.monster_Data.MoveSpeed);
                Debug.Log("Remove");
                isApplied = false;
            }
        }
    }
}
