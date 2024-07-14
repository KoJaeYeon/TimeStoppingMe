using UnityEngine;

[CreateAssetMenu(fileName = "Monster_Data_Boss", menuName = "Monster/Melee")]
public class Monster_Data_Boss : Monster_DATA
{
    [Header("원거리 : 투사체 프리팹")]
    public GameObject loveshot_Prefab;
    public GameObject hellfire_Prefab;
    public GameObject mindFlooring_Prefab;

    [Header("스킬1 하트발사")]
    public float sklii_loveshot_Cooldown;
    public float sklii_loveshot_Distnace;
    [Header("스킬2 깨물기")]
    public float sklii_bite_Cooldown;
    public float sklii_bite_Distnace;
    public float skiil_bite_Drain;
    [Header("스킬3 지옥불꽃")]
    public float sklii_hellfire_Cooldown;
    public float sklii_hellfire_Distnace;
    public float fireFlooring_time;
    public float fiireFlooring_range;
    public int fireFlooring_number;
    public float fireFlooring_size;

    [Header("스킬4 정신조작")]
    public float sklii_MindFlooring_Cooldown;
    public float sklii_MindFlooring_Distnace;
    public float mindFlooring_time;
    public float mindFlooring_range;
    public int mindFlooring_number;
    public float mindFlooring_size;


}