using UnityEngine;
using UnityEngine.Rendering;

public class InstantWeaponItem : Item
{

    public SkillItem skillItem;
    public override void Use(Player player)
    {
        player.AddSkillToWeapon(skillItem);
        Debug.Log("use");
        Destroy(gameObject);
    }
}
