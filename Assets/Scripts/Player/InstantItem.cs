using UnityEngine;

public class InstantItem : Item
{
    public override void Use(Player player)
    {
        // 즉시 발동되는 아이템의 효과를 여기서 구현합니다.
        // 예: 플레이어의 체력을 회복하는 아이템
        player.OnUpdateStat(player.MaxHP, Mathf.Min(player.CurrentHP + 20, player.MaxHP), player.MoveSpeed);
        Debug.Log(itemName + " used.");
        Destroy(gameObject); // 사용 후 아이템 파괴
    }
}
