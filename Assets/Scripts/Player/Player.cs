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
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private List<Debuff> activeDebuffs = new List<Debuff>();

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
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TimeStop();
        }

        if (isTimeStopped)
        {
            cinemachineBrain.ManualUpdate(); // ������ �ó׸ӽ� ī�޶� ������Ʈ
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

    public void OnTakeBuffed<T>(T buff)
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

    private void TimeStop()
    {
        isTimeStopped = !isTimeStopped;
        Debug.Log(isTimeStopped ? "Time stopped." : "Time resumed.");

        foreach (var rb in FindObjectsOfType<Rigidbody>())
        {
            if (rb.gameObject != gameObject)
            {
                rb.isKinematic = isTimeStopped;
            }
        }

        foreach (var mb in FindObjectsOfType<MonoBehaviour>())
        {
            if (mb.gameObject != gameObject && mb.GetType() != typeof(CinemachineBrain))
            {
                mb.enabled = !isTimeStopped;
            }
        }
    }

    public bool IsTimeStopped()
    {
        return isTimeStopped;
    }
}
