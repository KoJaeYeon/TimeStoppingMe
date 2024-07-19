using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAttackable
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] private GameObject dummyWeapon;
    [SerializeField] private bool isTimeStopped = false;

    [SerializeField] private int maxTimeGauge = 50;
    [SerializeField] private int currentTimeGauge;

    [SerializeField] private InstallationBlueprint blueprintPrefab;
    public InstallationBlueprint currentBlueprint;
    public PlaceableItem currentPlaceableItem;
    private Coroutine installationCoroutine;

    [SerializeField] private List<Item> hotbar = new List<Item> ();
    [SerializeField] private List<Debuff> activeDebuffs = new List<Debuff>();
    [SerializeField] private List<Buff> activeBuffs = new List<Buff>();

    public int MaxHP { get { return maxHP; } }
    public int CurrentHP { get { return currentHP; } set { currentHP = value; } }
    public bool IsCharmed { get; set; } = false;
    public bool IsSuppressed { get; set; } = false;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    public int CurrentTimeGauge { get { return currentTimeGauge; } }
    public WeaponBase CurrentWeapon { get; private set; }

    public Material PlayerMaterial;
    bool NoDamageMode = false;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        InitializePlayer();
        dummyWeapon.SetActive(false);
        currentTimeGauge = maxTimeGauge;
        UIManager.inst.UpdateHearts(currentHP);
    }

    private void InitializePlayer()
    {
        maxHP = playerData.maxHP;
        CurrentHP = maxHP;
        moveSpeed = playerData.moveSpeed;
        rotationSpeed = playerData.rotationSpeed;
        UIManager.inst.UpdatePlayerToolTip(moveSpeed);
    }

    public void EquipWeapon<T>() where T : WeaponBase
    {
        if (dummyWeapon.TryGetComponent(out WeaponBase existingWeapon))
        {
            Destroy(existingWeapon);
        }
        WeaponBase newWeapon = dummyWeapon.AddComponent<T>();
        newWeapon.SetBulletPrefabAndFirePoint(bulletPrefab, firePoint);
        newWeapon.Init();
        CurrentWeapon = newWeapon;
        dummyWeapon.SetActive(true);
    }

    public void AddSkillToWeapon(SkillItem skillItem)
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.AddSkill(skillItem);
        }
    }
    public void OnUpdateStatToWeapon(int baseDamage, float baseFireRate,float baseRange,float projectileSpeed,int maxAmmoSize,float reloadTime, int projectileCount)
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.OnUpdateStatToWeapon( baseDamage,  baseFireRate,  baseRange,  projectileSpeed,  maxAmmoSize,  reloadTime,  projectileCount);
        }
    }

    public void OnUpdateStat(int maxHP,  int currentHP, float moveSpeed)
    {
        this.maxHP = maxHP;
        this.currentHP = currentHP;
        this.moveSpeed = moveSpeed;
        UIManager.inst.UpdatePlayerToolTip(moveSpeed);
        UIManager.inst.UpdateHearts(currentHP);
    }

    private void Update()
    {
        // 디버프 상태 업데이트
        for (int i = activeDebuffs.Count - 1; i >= 0; i--)
        {
            if (activeDebuffs[i].ShouldTick())
            {
                activeDebuffs[i].ApplyEffect(gameObject);
                activeDebuffs[i].UpdateTickTime();
            }

            if (activeDebuffs[i].IsEffectOver())
            {
                activeDebuffs[i].RemoveEffect(gameObject);
                activeDebuffs.RemoveAt(i);
            }
        }

        // 버프 상태 업데이트
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            if (activeBuffs[i].IsTemporary && activeBuffs[i].IsEffectOver())
            {
                activeBuffs[i].RemoveEffect(this);
                activeBuffs.RemoveAt(i);
            }
        }

        if (currentBlueprint != null)
        {
            HandleBlueprintPlacement();
        }
    }

    public void OnTakeDamaged<T>(T damage)
    {
        if (damage is int)
        {
            if (NoDamageMode == true) return;

            StartCoroutine(DamagedEffect());

            currentHP -= (int)(object)damage;
            Debug.Log("Player took damage: " + damage + " Current health: " + currentHP);
            UIManager.inst.UpdateHearts(CurrentHP);
            if (currentHP <= 0)
            {
                Die();
            }
        }
    }

    public void OnTakeDebuffed<T>(DebuffType debuffType, T debuff) where T : Debuff
    {
        bool debuffExists = false;
        foreach (var activeDebuff in activeDebuffs)
        {
            if (activeDebuff.GetType() == debuff.GetType())
            {
                debuffExists = true;
                activeDebuff.RefreshDuration(); // 기존 효과의 지속시간 갱신
                break;
            }
        }

        if (!debuffExists)
        {
            activeDebuffs.Add(debuff);
            debuff.ApplyEffect(gameObject);
            StartCoroutine(HandleDebuff(debuff));
        }
    }

    public void OnTakeBuffed<T>(BuffType buffType, T buff) where T : Buff
    {
        activeBuffs.Add(buff);
        buff.ApplyEffect(this);
        if (buff.IsTemporary)
        {
            StartCoroutine(HandleBuff(buff));
        }
    }

    private IEnumerator HandleDebuff(Debuff debuff)
    {
        while (!debuff.IsEffectOver())
        {
            if (debuff.ShouldTick())
            {
                debuff.ApplyEffect(gameObject);
                debuff.UpdateTickTime();
            }
            yield return null;
        }

        debuff.RemoveEffect(gameObject);
        activeDebuffs.Remove(debuff);
    }

    private IEnumerator HandleBuff(Buff buff)
    {
        while (!buff.IsEffectOver())
        {
            yield return null;
        }

        buff.RemoveEffect(this);
        activeBuffs.Remove(buff);
    }

    private IEnumerator DamagedEffect()
    {
        NoDamageMode = true;
        Color color = PlayerMaterial.color;

        PlayerMaterial.color = new Color(0.5f, 0.5f, 0);
        yield return new WaitForSeconds(0.2f);
        PlayerMaterial.color = color;
        yield return new WaitForSeconds(0.2f);
        PlayerMaterial.color = new Color(0.5f, 0.5f,0);
        yield return new WaitForSeconds(0.2f);
        PlayerMaterial.color = color;
        yield return new WaitForSeconds(0.2f);
        PlayerMaterial.color = new Color(0.5f, 0.5f, 0);
        yield return new WaitForSeconds(0.2f);
        PlayerMaterial.color = color;

        NoDamageMode = false;
    }

    void Die()
    {
        Debug.Log("Player died");
        GameManager.Instance.HandlePlayerDeath();
    }

    public void Respawn(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        InitializePlayer();
    }

    public void ReloadWeapon()
    {
        if (CurrentWeapon != null)
        {
            StartCoroutine(CurrentWeapon.Reload());
        }
    }

    public void TimeStop()
    {
        if (currentTimeGauge <= 0 && !isTimeStopped) return;

        isTimeStopped = !isTimeStopped;
        Time.timeScale = isTimeStopped ? 0.001f : 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Debug.Log(isTimeStopped ? "Time stopped." : "Time resumed.");

        if (isTimeStopped)
        {
            StartCoroutine(TimeGaugeConsumption());
        }
    }

    private IEnumerator TimeGaugeConsumption()
    {
        while (isTimeStopped && currentTimeGauge > 0)
        {
            UIManager.inst.UpdateTimeGauge(currentTimeGauge, maxTimeGauge);
            yield return new WaitForSecondsRealtime(1f);
            currentTimeGauge -= 1;
            if (currentTimeGauge <= 0) TimeStop();
            UIManager.inst.UpdateTimeGauge(currentTimeGauge, maxTimeGauge);
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

    public void StartInstallation()
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

    public void CancelInstallation()
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
