using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public int maxHP = 100;
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
}
