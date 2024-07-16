using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스를 저장할 정적 변수
    private static GameManager instance;

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
    private List<GameObject> droppedWeapons = new List<GameObject>();

    // 인스턴스에 접근할 수 있는 정적 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없으면 새로 생성
            if (instance == null)
            {
                // 새로운 GameObject를 생성하고 GameManager 컴포넌트를 추가
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<GameManager>();
                singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";

                // 씬이 바뀌어도 파괴되지 않도록 설정
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }

    // 다른 스크립트가 이 클래스를 직접 생성하지 못하도록 private 생성자
    protected GameManager() { }

    // 필요한 초기화 코드
    private void Awake()
    {
        // 이미 인스턴스가 존재하는 경우 새로운 인스턴스를 파괴
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
            GameObject droppedWeapon = Instantiate(weapon, weaponDropPoint.position + Random.insideUnitSphere * 2, Quaternion.identity);
            droppedWeapons.Add(droppedWeapon);
        }
    }

    public void OnWeaponSelected(GameObject selectedWeapon)
    {
        Player playerScript = player.GetComponent<Player>();
        playerScript.SetWeapon(selectedWeapon.GetComponent<WeaponBase>());
        RemoveOtherWeapons(selectedWeapon);
        OpenCombatStageDoors();
    }

    private void RemoveOtherWeapons(GameObject selectedWeapon)
    {
        foreach (var weapon in droppedWeapons)
        {
            if (weapon != selectedWeapon)
            {
                Destroy(weapon);
            }
        }
        droppedWeapons.Clear();
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

    public void OnPlayerEnterStage(Stage stage)
    {
        CurrentState = stage.StageType;
        Debug.Log("Player entered " + stage.StageType);
        if (CurrentState == GameState.CombatStage && !allCombatStagesCleared)
        {
            EnterCombatStage();
        }
        else if (CurrentState == GameState.BossStage)
        {
            EnterBossStage();
        }
    }
}
