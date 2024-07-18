using UnityEngine;
using UnityEngine.Rendering;

public class InstantItem : Item
{
    public int maxHp;
    public int currentHP;
    public int moveSpeed;
    public override void Use(Player player)
    {
        // ��� �ߵ��Ǵ� �������� ȿ���� ���⼭ �����մϴ�.
        // ��: �÷��̾��� ü���� ȸ���ϴ� ������
        player.OnUpdateStat(player.MaxHP+ maxHp, Mathf.Min(player.CurrentHP + currentHP, player.MaxHP+maxHp), player.MoveSpeed+ moveSpeed);
        Debug.Log(itemName + " used.");
        Destroy(gameObject); // ��� �� ������ �ı�
    }
}
