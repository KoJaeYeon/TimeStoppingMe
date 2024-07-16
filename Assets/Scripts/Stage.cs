using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameState StageType; // Stage Ÿ�� (StartStage, CombatStage, BossStage)
    public Collider StageCollider; // �������� ������ �����ϴ� �ݶ��̴�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.OnPlayerEnterStage(this);
        }
    }
}
