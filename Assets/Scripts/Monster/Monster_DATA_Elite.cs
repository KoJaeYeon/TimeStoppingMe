using UnityEngine;

[CreateAssetMenu(fileName = "Monster_Data_Elite", menuName = "Scriptable/Monster/Elite")]
public class Monster_Data_Elite : Monster_DATA
{
    [Header("원거리 : 투사체 프리팹")]
    public GameObject scratch_Prefab;

    [Header("스킬1 손톱 날리기")]
    public float skill_scratch_Cooldown;
    public float skill_scratch_Distance;
    public float skill_scratch_LaunchSpeed;
    [Header("스킬2 깨물기")]
    public float skill_footWalk_Cooldown;
    public float skill_footWalk_Distance;
    public float skill_footWalk_Speed;
}