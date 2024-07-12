using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable_Range", menuName ="Monster")]
public class Monster_DATA : ScriptableObject
{
    public float AttackDistance;
    public float TrackDistance;

    public float MaxHealth;
    public int Damage;

    public float MoveSpeed;
    public float AngularSpeed;
    //부채꼴
    public float Search_Range;

    public float IdleRange_Min;
    public float IdleRange_Max;

    public float projectile1_EndDistance;
    public float projectile1_Speed;
}
