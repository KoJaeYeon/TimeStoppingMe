using UnityEngine;

public class CollectableItem : Item
{
    public override void Use(Player player)
    {
        // 핫바에 저장되는 아이템의 로직을 여기서 구현합니다.
        // 예: 플레이어의 핫바에 아이템을 추가합니다.
        player.AddToHotbar(this);
        Debug.Log(itemName + " collected to hotbar.");
        gameObject.SetActive(false); // 아이템을 비활성화
    }
}
