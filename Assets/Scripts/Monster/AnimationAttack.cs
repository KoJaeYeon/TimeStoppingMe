using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAttack : MonoBehaviour
{
    public void Attack0()
    {
        var monster = transform.parent.GetComponent<Monster>();
        monster.Attack0();
    }

    public void Attack0_End()
    {
        var monster = transform.parent.GetComponent<Monster>();
        monster.Attack0_End();
    }
}
