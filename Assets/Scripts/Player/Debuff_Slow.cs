using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Slow : Debuff
{
    private float originalMoveSpeed;
    private float slowPercentage;
    private bool isApplied = false;


    public Debuff_Slow(float slowPercentage) : base(0.1f, 0, 0) // ���� �ð� ����
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
                if (monster is Monster_Elite_2)
                {
                    var monsterE = monster as Monster_Elite;
                    var data = monsterE.monster_Data as Monster_Data_Elite;
                    monsterE.moveSpeed = data.MoveSpeed / 2;
                }
                else
                {
                    monster.bt.SetVariableValue("Speed", monster.monster_Data.MoveSpeed * (1 - slowPercentage));
                    if (monster is Monster_Elite)
                    {
                        var monsterE = monster as Monster_Elite;
                        var data = monsterE.monster_Data as Monster_Data_Elite;
                        monsterE.bt.SetVariableValue("Skill2Speed", data.skill_footWalk_Speed * (1 - slowPercentage));
                    }
                }

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
                if (monster is Monster_Elite_2)
                {
                    var monsterE = monster as Monster_Elite;
                    var data = monsterE.monster_Data as Monster_Data_Elite;
                    monsterE.moveSpeed = data.MoveSpeed;
                }
                else
                {
                    monster.bt.SetVariableValue("Speed", monster.monster_Data.MoveSpeed);
                    if (monster is Monster_Elite)
                    {
                        var monsterE = monster as Monster_Elite;
                        var data = monsterE.monster_Data as Monster_Data_Elite;
                        monsterE.bt.SetVariableValue("Skill2Speed", data.skill_footWalk_Speed);
                    }
                }


                isApplied = false;
            }
        }
    }
}
