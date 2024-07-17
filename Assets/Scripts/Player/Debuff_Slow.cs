using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Slow : Debuff
{
    private float originalMoveSpeed;
    private float slowPercentage;

    public Debuff_Slow(float slowPercentage) : base(1f, 0, 0) // 지속 시간 없음
    {
        this.slowPercentage = slowPercentage;
    }

    public override void ApplyEffect(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            originalMoveSpeed = player.MoveSpeed;
            player.MoveSpeed *= (1 - slowPercentage);
        }
        else
        {
            Monster monster = target.GetComponent<Monster>();
            if (monster != null)
            {
                monster.bt.SetVariableValue("Speed", monster.monster_Data.MoveSpeed * (1 - slowPercentage));
                if (monster is Monster_Elite)
                {
                    var monsterE = monster as Monster_Elite;
                    var data = monsterE.monster_Data as Monster_Data_Elite;
                    monsterE.bt.SetVariableValue("Skill2Speed", data.skill_footWalk_Speed * (1 - slowPercentage));
                }
            }
        }
    }

    public override void RemoveEffect(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if(player != null)
        {
            player.MoveSpeed = originalMoveSpeed;
        }
        else
        {
            Monster monster = target.GetComponent<Monster>();
            if (monster != null)
            {
                monster.bt.SetVariableValue("Speed", monster.monster_Data.MoveSpeed);
                if(monster is Monster_Elite)
                {
                    var monsterE = monster as Monster_Elite;
                    var data = monsterE.monster_Data as Monster_Data_Elite;
                    monsterE.bt.SetVariableValue("Skill2Speed", data.skill_footWalk_Speed);
                }
            }
        }
    }
}
