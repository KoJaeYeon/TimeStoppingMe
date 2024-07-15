using UnityEngine;

[CreateAssetMenu(fileName = "Debuff", menuName = "Scriptable/Debuff")]
public class DebuffScriptable : ScriptableObject
{
    [Header("ȭ��")]
    public float Burn_Duration;
    public float Burn_TickInterval;
    public int Burn_DamagePerTick;

    [Header("�ߵ�")]
    public float Poision_Duration;
    public float Poision_TickInterval;
    public int Poision_DamagePerTick;

    [Header("��������")]
    public float Mind_Duration;


}
