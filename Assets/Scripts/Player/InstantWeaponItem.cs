using UnityEngine;
using UnityEngine.Rendering;

public class InstantWeaponItem : Item
{
    public int baseDamage;
    public float baseFireRate;
    public float baseRange;
    public float projectileSpeed;
    public int maxAmmoSize;
    public float reloadTime;
    public int projectileCount;
    public SkillItem skillItem;
    public override void Use(Player player)
    {
        if (skillItem != null)
        {
            player.AddSkillToWeapon(skillItem);
        }
        else
        {
            player.OnUpdateStatToWeapon(baseDamage, baseFireRate, baseRange, projectileSpeed, maxAmmoSize, reloadTime, projectileCount);
        }
        Debug.Log("use");
        Destroy(gameObject);
    }
}