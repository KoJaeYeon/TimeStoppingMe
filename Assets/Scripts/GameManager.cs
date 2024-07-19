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

    public void OnBossKilled()
    {
        Debug.Log("Boss defeated! You win!");
        UIManager.inst.SendGameClearMessage();
    }
}
