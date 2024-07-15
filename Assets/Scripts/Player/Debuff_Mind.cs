using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Mind : Debuff
{
    private Player player;

    public Debuff_Mind() : base(5f, 0, 0) { }

    public override void ApplyEffect(GameObject target)
    {
        if(player == null)
        {
            player = target.GetComponent<Player>();
        }
        if(player != null && !player.IsCharmed)
        {
            player.StartCoroutine(ApplyCharm(player));
        }
    }

    private IEnumerator ApplyCharm(Player player)
    {
        player.IsCharmed = true;
        float startTime = Time.time;
        while(Time.time < startTime + Duration)
        {
            yield return null;
        }
        player.IsCharmed = false;
    }

    public override void RemoveEffect(GameObject target)
    {
        if(player == null)
        {
            player = target.GetComponent<Player>();
        }
        if(player != null)
        {
            player.IsCharmed = false;
        }
    }
}
