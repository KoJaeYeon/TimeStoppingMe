using UnityEngine;

[CreateAssetMenu(fileName = "Debuff", menuName = "Scriptable/Debuff")]
public class DebuffScriptable : ScriptableObject
{
    [Header("화상")]
    public float Burn_Duration;
    public float Burn_TickInterval;
    public int Burn_DamagePerTick;

    [Header("중독")]
    public float Poision_Duration;
    public float Poision_TickInterval;
    public int Poision_DamagePerTick;

    [Header("정신지배")]
    public float Mind_Duration;


}
