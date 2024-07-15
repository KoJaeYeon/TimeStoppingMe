using UnityEngine;


[CreateAssetMenu(fileName = "Monster_DATA_Range", menuName = "Monster/Range")]
public class Monster_DATA_Range : Monster_DATA
{
    [Header("원거리 : 투사체 프리팹")]
    public GameObject projectile1_Prefab;

    [Header("원거리 : 투사체 최대 거리")]
    public float projectile1_EndDistance;
    [Header("원거리 : 곡사 투사체 최대 높이")]
    public float projectile1_EndHeight;
}