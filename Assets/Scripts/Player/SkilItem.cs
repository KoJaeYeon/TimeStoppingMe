using UnityEngine;

[CreateAssetMenu(fileName = "SkillItem", menuName = "Items/SkillItem")]
public class SkillItem : ScriptableObject
{
    public SkillType skillType;
    public float knockbackForce;
    public int additionalPiercing;
}