using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Suppress : Debuff
{
    public Debuff_Suppress() : base(5f, 0, 0) { }

    public override void ApplyEffect(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            player.IsSuppressed = true;
            Debug.Log("Player is suppressed.");
        }
    }

    public override void RemoveEffect(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            player.IsSuppressed = false;
            Debug.Log("Player is no longer suppressed.");
        }
    }
}
