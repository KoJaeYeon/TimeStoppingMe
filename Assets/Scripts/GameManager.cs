using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameState CurrentState { get; private set; }

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startStageSpawnPoint;
    [SerializeField] private List<GameObject> availableWeapons;
    [SerializeField] private Transform weaponDropPoint;
    [SerializeField] private List<Door> combatStageDoors;
    [SerializeField] private Door bossStageDoor;

    private GameObject player;
    private List<GameObject> droppedWeapons = new List<GameObject>();

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<GameManager>();
                singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }

    protected GameManager() { }

    private void Awake()
    {
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

    public void OnMonsterKilled()
    {
        if (AreAllMonstersKilled())
        {
            if (CurrentState == GameState.CombatStage)
            {
                OpenBossStageDoor();
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
    }

    private void CloseAllDoors()
    {
        foreach (var door in combatStageDoors)
        {
            door.Close();
        }
        bossStageDoor.Close();
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
        if (CurrentState == GameState.CombatStage)
        {
            // 전투 스테이지 진입 시 추가 로직이 필요한 경우 여기에 작성
        }
        else if (CurrentState == GameState.BossStage)
        {
            EnterBossStage();
        }
    }
}
