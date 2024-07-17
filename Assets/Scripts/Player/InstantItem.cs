using UnityEngine;
using UnityEngine.Rendering;

public class InstantItem : Item
{
    public int maxHp;
    public int currentHP;
    public int moveSpeed;
    public override void Use(Player player)
    {
        // 즉시 발동되는 아이템의 효과를 여기서 구현합니다.
        // 예: 플레이어의 체력을 회복하는 아이템
        player.OnUpdateStat(player.MaxHP+ maxHp, Mathf.Min(player.CurrentHP + currentHP, player.MaxHP+maxHp), player.MoveSpeed+ moveSpeed);
        Debug.Log(itemName + " used.");
        Destroy(gameObject); // 사용 후 아이템 파괴
    }
}
