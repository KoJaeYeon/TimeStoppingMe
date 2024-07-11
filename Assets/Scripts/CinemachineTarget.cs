using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineTargetGroup))]
public class CinemachineTarget : MonoBehaviour
{
    private CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField] private Transform cursorTarget;

    private void Awake()
    {
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();

        if (cursorTarget == null)
        {
            Debug.LogError("Cursor target is not assigned in the Inspector.");
        }
    }

    private void Start()
    {
        SetCinemachineTargetGroup();
    }

    private void SetCinemachineTargetGroup()
    {
        Player player = GameManager.Instance.GetPlayer();
        if (player == null)
        {
            Debug.LogError("Player is not assigned or found in GameManager.");
            return;
        }

        CinemachineTargetGroup.Target cinemachineGroupTarget_Player = new CinemachineTargetGroup.Target
        {
            weight = 1f,
            radius = 2.5f,
            target = player.transform
        };

        CinemachineTargetGroup.Target cinemachineGroupTarget_Cursor = new CinemachineTargetGroup.Target
        {
            weight = 1f,
            radius = 1f,
            target = cursorTarget
        };

        CinemachineTargetGroup.Target[] cinemachineTargetArray = new CinemachineTargetGroup.Target[]
        {
            cinemachineGroupTarget_Player,
            cinemachineGroupTarget_Cursor
        };

        cinemachineTargetGroup.m_Targets = cinemachineTargetArray;
    }

    private void Update()
    {
        cursorTarget.position = Func.GetMouseWorldPosition();
    }
}
