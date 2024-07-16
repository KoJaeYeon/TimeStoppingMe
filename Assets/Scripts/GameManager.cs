using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startStageSpawnPoint;
    [SerializeField] private List<GameObject> availableWeapons;
    [SerializeField] private Transform weaponDropPoint;
    [SerializeField] private List<Transform> combatStageSpawnPoints;
    [SerializeField] private Transform bossStageSpawnPoint;
    [SerializeField] private List<GameObject> combatStageMonsters;
    [SerializeField] private GameObject bossMonsterPrefab;
    [SerializeField] private List<Door> combatStageDoors;
    [SerializeField] private Door bossStageDoor;

    private GameObject player;
    private int currentCombatStage = 0;
    private bool allCombatStagesCleared = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        CurrentState = GameState.StartStage;
        SpawnPlayer(startStageSpawnPoint.position);
        DropWeapons();
    }

    private void SpawnPlayer(Vector3 spawnPoint)
    {
        player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
    }

    private void DropWeapons()
    {
        foreach (var weapon in availableWeapons)
        {
            Instantiate(weapon, weaponDropPoint.position + Random.insideUnitSphere * 2, Quaternion.identity);
        }
    }

    public void OnWeaponSelected(GameObject selectedWeapon)
    {
        Player playerScript = player.GetComponent<Player>();
        playerScript.SetWeapon(selectedWeapon.GetComponent<WeaponBase>());
        OpenCombatStageDoors();
    }

    private void OpenCombatStageDoors()
    {
        foreach (var door in combatStageDoors)
        {
            door.Open();
        }
    }

    public void EnterCombatStage()
    {
        if (currentCombatStage < combatStageSpawnPoints.Count)
        {
            CurrentState = GameState.CombatStage;
            CloseAllDoors();
            SpawnMonsters();
        }
        else
        {
            OpenBossStageDoor();
        }
    }

    private void CloseAllDoors()
    {
        foreach (var door in combatStageDoors)
        {
            door.Close();
        }
        bossStageDoor.Close();
    }

    private void SpawnMonsters()
    {
        foreach (var monsterPrefab in combatStageMonsters)
        {
            Instantiate(monsterPrefab, combatStageSpawnPoints[currentCombatStage].position + Random.insideUnitSphere * 5, Quaternion.identity);
        }
    }

    public void OnMonsterKilled()
    {
        if (AreAllMonstersKilled())
        {
            currentCombatStage++;
            if (currentCombatStage >= combatStageSpawnPoints.Count)
            {
                allCombatStagesCleared = true;
                OpenBossStageDoor();
            }
            else
            {
                OpenCombatStageDoors();
            }
        }
    }

    private bool AreAllMonstersKilled()
    {
        return GameObject.FindGameObjectsWithTag("Monster").Length == 0;
    }

    private void OpenBossStageDoor()
    {
        Debug.Log("Boss stage door is now open!");
        bossStageDoor.Open();
    }

    public void EnterBossStage()
    {
        CurrentState = GameState.BossStage;
        CloseAllDoors();
        SpawnBossMonster();
    }

    private void SpawnBossMonster()
    {
        Instantiate(bossMonsterPrefab, bossStageSpawnPoint.position, Quaternion.identity);
    }

    public void OnBossKilled()
    {
        Debug.Log("Boss defeated! You win!");
        // 게임 승리 로직을 구현합니다.
    }
}
