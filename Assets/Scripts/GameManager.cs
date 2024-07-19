using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameState CurrentState { get; private set; }

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startStageSpawnPoint;
    [SerializeField] private Transform weaponDropPoint;
    [SerializeField] private List<Door> combatStageDoors;
    [SerializeField] private Door bossStageDoor;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private GameObject player;

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
    }

    private void SpawnPlayer(Vector3 spawnPoint)
    {
        player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        player.GetComponent<Player>().Respawn(spawnPoint); // 초기 위치 설정
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            virtualCamera.Follow = player.transform;
        }
    }

    public void RespawnPlayer()
    {
        player.GetComponent<Player>().Respawn(startStageSpawnPoint.position);
    }

    public void HandlePlayerDeath()
    {
        UIManager.inst.SendPlayerDeathMessage();
        StartCoroutine(RespawnPlayerWithDelay(3f));
    }

    private IEnumerator RespawnPlayerWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        RespawnPlayer();
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
        UIManager.inst.SendGameClearMessage();
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
