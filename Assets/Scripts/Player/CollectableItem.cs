using UnityEngine;

public class CollectableItem : Item
{
    public override void Use(Player player)
    {
        // �ֹٿ� ����Ǵ� �������� ������ ���⼭ �����մϴ�.
        // ��: �÷��̾��� �ֹٿ� �������� �߰��մϴ�.
        player.AddToHotbar(this);
        Debug.Log(itemName + " collected to hotbar.");
        gameObject.SetActive(false); // �������� ��Ȱ��ȭ
    }
}
