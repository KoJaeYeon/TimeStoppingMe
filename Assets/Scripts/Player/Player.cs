using BehaviorDesigner.Runtime.Tasks.Unity.Timeline;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IAttackable
{
    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;
    [SerializeField] protected float moveSpeed;
    [SerializeField] private WeaponBase currentWeapon;
    [SerializeField] private bool isTimeStopped = false;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    [SerializeField] private InstallationBlueprint blueprintPrefab;
    private InstallationBlueprint currentBlueprint;
    private PlaceableItem currentPlaceableItem;
    private Coroutine installationCoroutine;

    [SerializeField] private List<Item> hotbar = new List<Item> ();
    [SerializeField] private List<Debuff> activeDebuffs = new List<Debuff>();

    public int MaxHP { get { return maxHP; } }
    public int CurrentHP { get { return  currentHP; } }
    public float MoveSpeed { get {  return moveSpeed; } }
    public WeaponBase CurrentWeapon { get { return currentWeapon; } }

    

    private void Start()
    {
        if(currentWeapon != null)
        {
            currentWeapon.Init();
        }
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public void SetWeapon(WeaponBase newWeapon)
    {
        currentWeapon = newWeapon;
        currentWeapon.Init();
    }

    public void OnUpdateStat(int maxHP,  int currentHP, float moveSpeed)
    {
        this.maxHP = maxHP;
        this.currentHP = currentHP;
        this.moveSpeed = moveSpeed;
    }

    private void Update()
    {
        // 디버프 상태 업데이트
        foreach (var debuff in activeDebuffs)
        {
            debuff.ApplyEffect(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TimeStop();
        }

        if (isTimeStopped)
        {
            cinemachineVirtualCamera.enabled = true;
        }

        if (currentBlueprint != null)
        {
            HandleBlueprintPlacement();
        }

        if (Input.GetKeyDown(KeyCode.F) && currentPlaceableItem != null)
        {
            if (currentBlueprint != null)
            {
                StartInstallation();
            }
        }
    }

    public void OnTakeDamaged<T>(T damage)
    {
        if (damage is int)
        {
            currentHP -= (int)(object)damage;
            Debug.Log("Player took damage: " + damage + " Current health: " + currentHP);
            if (currentHP <= 0)
            {
                Die();
            }
        }
    }

    public void OnTakeDebuffed<T>(T debuff) where T : Debuff
    {
        bool debuffExists = false;
        foreach (var activeDebuff in activeDebuffs)
        {
            if (activeDebuff.GetType() == debuff.GetType())
            {
                debuffExists = true;
                activeDebuff.Duration = debuff.Duration; // 기존 효과의 지속시간 갱신
                break;
            }
        }

        if (!debuffExists)
        {
            activeDebuffs.Add(debuff);
            StartCoroutine(HandleDebuff(debuff));
        }
    }

    public void OnTakeBuffed<T>(T buff)
    {
        // 버프 처리 로직
        Debug.Log("Player took buff: " + buff);
        // 버프 적용 로직 추가 가능
    }

    private IEnumerator HandleDebuff(Debuff debuff)
    {
        while (!debuff.IsEffectOver())
        {
            debuff.ApplyEffect(gameObject);
            yield return new WaitForSeconds(debuff.TickInterval);
        }

        activeDebuffs.Remove(debuff);
    }

    void Die()
    {
        // 플레이어 사망 처리 로직
        Debug.Log("Player died");
        // 게임 오버 처리
    }

    public void ReloadWeapon()
    {
        if (currentWeapon != null)
        {
            StartCoroutine(currentWeapon.Reload());
        }
    }

    private void TimeStop()
    {
        isTimeStopped = !isTimeStopped;
        Time.timeScale = isTimeStopped ? 0f : 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Debug.Log(isTimeStopped ? "Time stopped." : "Time resumed.");

        if (cinemachineVirtualCamera != null)
        {
            cinemachineVirtualCamera.enabled = true;
        }
    }

    public bool IsTimeStopped()
    {
        return isTimeStopped;
    }

    public void AddToHotbar(Item item)
    {
        hotbar.Add(item);
        Debug.Log(item.itemName + " added to hotbar.");
    }

    public void UseHotbarItem(int index)
    {
        if (index >= 0 && index < hotbar.Count)
        {
            Item item = hotbar[index];
            item.Use(this);
            hotbar.RemoveAt(index); // 사용 후 핫바에서 제거
        }
    }

    private void HandleBlueprintPlacement()
    {
        Vector3 position = GetBlueprintPosition();
        Quaternion rotation = GetBlueprintRotation();
        currentBlueprint.SetPositionAndRotation(position, rotation);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelInstallation();
        }
    }

    private Vector3 GetBlueprintPosition()
    {
        // 마우스 위치에서 레이캐스트하여 청사진 위치 결정
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return transform.position;
    }

    private Quaternion GetBlueprintRotation()
    {
        // 마우스 휠로 회전 각도 결정
        float rotationY = Mathf.Round(Input.GetAxis("Mouse ScrollWheel") * 8) * 45f;
        return Quaternion.Euler(0, rotationY, 0);
    }

    public void StartPlacingItem(PlaceableItem placeableItem)
    {
        currentPlaceableItem = placeableItem;
        currentBlueprint = Instantiate(blueprintPrefab);
        currentBlueprint.SetVisibility(true);
    }

    private void StartInstallation()
    {
        if (installationCoroutine != null)
        {
            StopCoroutine(installationCoroutine);
        }
        installationCoroutine = StartCoroutine(InstallItemCoroutine());
    }

    private IEnumerator InstallItemCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < currentPlaceableItem.installationTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CancelInstallation();
                yield break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        CompleteInstallation();
    }

    private void CancelInstallation()
    {
        if (installationCoroutine != null)
        {
            StopCoroutine(installationCoroutine);
        }
        if (currentBlueprint != null)
        {
            Destroy(currentBlueprint.gameObject);
            currentBlueprint = null;
        }
        currentPlaceableItem = null;
    }

    private void CompleteInstallation()
    {
        Instantiate(currentPlaceableItem, currentBlueprint.transform.position, currentBlueprint.transform.rotation);
        currentPlaceableItem.Use(this);
        CancelInstallation();
    }
}
