using UnityEngine;

public class PlaceableItem : Item
{
    public float installationTime;

    public override void Use(Player player)
    {
        player.StartPlacingItem(this);
    }
}
