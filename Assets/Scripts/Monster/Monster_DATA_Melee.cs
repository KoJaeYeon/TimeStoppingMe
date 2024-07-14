using UnityEngine;

[CreateAssetMenu(fileName = "Monster_Data_Melee", menuName = "Monster/Melee")]
public class Monster_Data_Melee : Monster_DATA
{
    [Header("근거리 : 공격 콜라이더 최대 거리설정")]
    public float attack_MaxDistance;
}