using UnityEngine;

[CreateAssetMenu(fileName = "Monster_Data_Elite_2", menuName = "Scriptable/Monster/Elite_2")]
public class Monster_Data_Elite_2 : Monster_DATA
{
    [Header("스킬1 깨물기")]
    public float skill_bite_Cooldown;
    public float skill_bite_Distance;
    [Header("스킬2 재빠른 발놀림")]
    public float skill_footWalk_Cooldown;
    public float skill_footWalk_Distance;
}