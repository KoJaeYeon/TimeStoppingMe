using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAttackable
{
    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;
    [SerializeField] protected float moveSpeed;
    [SerializeField] private WeaponBase currentWeapon;
    [SerializeField] private bool isTimeStopped = false;

    [SerializeField] private InstallationBlueprint blueprintPrefab;
    public InstallationBlueprint currentBlueprint;
    public PlaceableItem currentPlaceableItem;
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
        // ����� ���� ������Ʈ
        foreach (var debuff in activeDebuffs)
        {
            debuff.ApplyEffect(gameObject);
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
            currentHP -= (int)(object)damage;
            Debug.Log("Player took damage: " + damage + " Current health: " + currentHP);
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
                activeDebuff.Duration = debuff.Duration; // ���� ȿ���� ���ӽð� ����
                break;
            }
        }

        if (!debuffExists)
        {
            activeDebuffs.Add(debuff);
            StartCoroutine(HandleDebuff(debuff));
        }
    }

    public void OnTakeBuffed<T>(BuffType bufftype, T buff)
    {
        // ���� ó�� ����
        Debug.Log("Player took buff: " + buff);
        // ���� ���� ���� �߰� ����
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
        // �÷��̾� ��� ó�� ����
        Debug.Log("Player died");
        // ���� ���� ó��
    }

    public void ReloadWeapon()
    {
        if (currentWeapon != null)
        {
            StartCoroutine(currentWeapon.Reload());
        }
    }

    public void TimeStop()
    {
        isTimeStopped = !isTimeStopped;
        Time.timeScale = isTimeStopped ? 0f : 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Debug.Log(isTimeStopped ? "Time stopped." : "Time resumed.");
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
            hotbar.RemoveAt(index); // ��� �� �ֹٿ��� ����
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
        // ���콺 ��ġ���� ����ĳ��Ʈ�Ͽ� û���� ��ġ ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return transform.position;
    }

    private Quaternion GetBlueprintRotation()
    {
        // ���콺 �ٷ� ȸ�� ���� ����
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
