using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameState StageType; // Stage 타입 (StartStage, CombatStage, BossStage)
    public Collider StageCollider; // 스테이지 영역을 정의하는 콜라이더

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.OnPlayerEnterStage(this);
        }
    }
}
