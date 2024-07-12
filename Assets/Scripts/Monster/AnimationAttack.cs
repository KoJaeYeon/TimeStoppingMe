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
}
