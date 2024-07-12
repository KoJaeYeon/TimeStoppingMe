using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAttack : MonoBehaviour
{
    public void Attack()
    {
        transform.parent.GetComponent<Monster>();
    }
}
