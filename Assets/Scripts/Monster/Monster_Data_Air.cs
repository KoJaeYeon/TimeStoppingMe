using UnityEngine;

[CreateAssetMenu(fileName = "Monster_Data_Air", menuName = "Scriptable/Monster/Air")]
public class Monster_Data_Air : Monster_DATA
{
    [Header("스킬1 공중잡기")]
    public float skill_grap_Cooldown;
    public float skill_grap_Distance;

    public float Grap_x;
    public float Grap_y;
    public float Grap_z;
}