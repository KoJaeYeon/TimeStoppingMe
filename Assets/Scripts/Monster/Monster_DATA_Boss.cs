using UnityEngine;

[CreateAssetMenu(fileName = "Monster_Data_Boss", menuName = "Scriptable/Monster/Melee")]
public class Monster_Data_Boss : Monster_DATA
{
    [Header("원거리 : 투사체 프리팹")]
    public GameObject loveshot_Prefab;
    public GameObject hellfire_Prefab;
    public GameObject mindFlooring_Prefab;

    [Header("스킬1 하트발사")]
    public float skill_loveshot_Cooldown;
    public float skill_loveshot_Distance;
    public float skill_loveshot_LaunchSpeed;
    [Header("스킬2 깨물기")]
    public float skill_bite_Cooldown;
    public float skill_bite_Distance;
    public float skiil_bite_Drain;
    [Header("스킬3 지옥불꽃")]
    public float skill_hellfire_Cooldown;
    public float skill_hellfire_Distance;
    public float fireFlooring_time;
    public float fiireFlooring_range;
    public int fireFlooring_number;
    public float fireFlooring_size;

    [Header("스킬4 정신조작")]
    public float skill_MindFlooring_Cooldown;
    public float skill_MindFlooring_Distance;
    public float mindFlooring_time;
    public float mindFlooring_range;
    public int mindFlooring_number;
    public float mindFlooring_size;


}