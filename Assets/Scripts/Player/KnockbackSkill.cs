using UnityEngine;

[System.Serializable]
public class KnockbackSkill : WeaponSkill
{
    public float knockbackForce;

    public KnockbackSkill(float force)
    {
        knockbackForce = force;
    }

    public override void ApplySkill(Bullet bullet)
    {
        bullet.knockbackForce = knockbackForce;
    }
}