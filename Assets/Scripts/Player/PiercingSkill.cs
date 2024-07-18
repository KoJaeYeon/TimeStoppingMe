[System.Serializable]
public class PiercingSkill : WeaponSkill
{
    public int additionalPiercing;

    public PiercingSkill(int piercing)
    {
        additionalPiercing = piercing;
    }

    public override void ApplySkill(Bullet bullet)
    {
        bullet.piercingCount += additionalPiercing;
    }
}