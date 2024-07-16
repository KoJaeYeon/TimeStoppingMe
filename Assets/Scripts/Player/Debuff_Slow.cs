using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Slow : Debuff
{
    private float originalMoveSpeed;
    private float slowPercentage;

    public Debuff_Slow(float slowPercentage) : base(0, 0, 0) // 지속 시간 없음
    {
        this.slowPercentage = slowPercentage;
    }

    public override void ApplyEffect(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            originalMoveSpeed = player.MoveSpeed;
            player.MoveSpeed *= (1 - slowPercentage / 100f);
        }
    }

    public override void RemoveEffect(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if(player != null)
        {
            player.MoveSpeed = originalMoveSpeed;
        }
    }
}
