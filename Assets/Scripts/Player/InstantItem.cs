using UnityEngine;

public class InstantItem : Item
{
    public override void Use(Player player)
    {
        // ��� �ߵ��Ǵ� �������� ȿ���� ���⼭ �����մϴ�.
        // ��: �÷��̾��� ü���� ȸ���ϴ� ������
        player.OnUpdateStat(player.MaxHP, Mathf.Min(player.CurrentHP + 20, player.MaxHP), player.MoveSpeed);
        Debug.Log(itemName + " used.");
        Destroy(gameObject); // ��� �� ������ �ı�
    }
}
