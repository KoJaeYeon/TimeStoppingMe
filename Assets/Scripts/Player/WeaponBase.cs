using System.Collections;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected int baseDamage;
    [SerializeField] protected float baseFireRate;
    [SerializeField] protected float baseRange;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected int projectileCount;
    [SerializeField] protected float parallelSpacing = 0.5f;
    [SerializeField] protected int maxAmmoSize;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected int piercingCount;

    [SerializeField] protected ProjectilePattern projectilePattern = ProjectilePattern.Parallel;
    [SerializeField] protected FireMode fireMode = FireMode.Single;

    [SerializeField] protected Bullet[] bullets;

    private int currentAmmoSize;
    private float nextFireTime = 0f;
    private bool isReloading = false;

    private bool isKnockbackActive = false;
    private float knockbackForce;
    private bool isPiercingActive = false;
    private int additionalPiercing;

    public void Init()
    {
        currentAmmoSize = maxAmmoSize;
        isReloading = false;
        nextFireTime = 0f;
    }

    public void SetBulletPrefabAndFirePoint(GameObject bulletPrefab, Transform firePoint)
    {
        this.bulletPrefab = bulletPrefab;
        this.firePoint = firePoint;
    }

    public void AddSkill(SkillItem skillItem)
    {
        if (skillItem.skillType == SkillType.Knockback)
        {
            isKnockbackActive = true;
            knockbackForce = skillItem.knockbackForce;
        }
        else if (skillItem.skillType == SkillType.Piercing)
        {
            isPiercingActive = true;
            additionalPiercing = skillItem.additionalPiercing;
        }
    }

    public virtual void Fire(LayerMask enemyLayers)
    {
        if (isReloading) return;

        if(currentAmmoSize <= 0 || currentAmmoSize < projectileCount)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Time.unscaledTime >= nextFireTime)
        {
            nextFireTime = Time.unscaledTime + 1f / baseFireRate;

            bullets = new Bullet[projectileCount];
            if (projectilePattern == ProjectilePattern.Parallel)
            {
                FireParallel(enemyLayers);
            }
            else if (projectilePattern == ProjectilePattern.Spread)
            {
                FireSpread(enemyLayers);
            }

            currentAmmoSize -= projectileCount;

            if(currentAmmoSize < 0)
            {
                StartCoroutine(Reload());
            }
        }
    }

    private void FireParallel(LayerMask enemyLayers)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 offset = firePoint.right * (i - projectileCount / 2f) * parallelSpacing;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position + offset, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(baseDamage, enemyLayers, projectileSpeed, piercingCount, 0);

            if (isKnockbackActive)
            {
                bulletScript.ActivateKnockbackSkill(knockbackForce);
            }

            if (isPiercingActive)
            {
                bulletScript.ActivatePiercingSkill(additionalPiercing);
            }
            bullets[i] = bulletScript;
        }
    }

    private void FireSpread(LayerMask enemyLayers)
    {
        float spreadAngle = 10f; // 각 투사체 사이의 각도
        float startAngle = -spreadAngle * (projectileCount - 1) / 2;

        for (int i = 0; i < projectileCount; i++)
        {
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, startAngle + i * spreadAngle, 0);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(baseDamage, enemyLayers, projectileSpeed, piercingCount, 0);

            if (isKnockbackActive)
            {
                bulletScript.ActivateKnockbackSkill(knockbackForce);
            }

            if (isPiercingActive)
            {
                bulletScript.ActivatePiercingSkill(additionalPiercing);
            }
            bullets[i] = bulletScript;
        }
    }

    public IEnumerator Reload()
    {
        if (isReloading) yield break;

        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmoSize = maxAmmoSize;
        isReloading = false;
        Debug.Log("Reloaded");
    }
}
