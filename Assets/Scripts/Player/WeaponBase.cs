using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] protected int maxAmmoSize; // 탄창 수
    [SerializeField] protected float reloadTime;

    [SerializeField] protected ProjectilePattern projectilePattern = ProjectilePattern.Parallel;
    [SerializeField] protected FireMode fireMode = FireMode.Single;

    private int currentAmmoSize;
    private float nextFireTime = 0f;
    private bool isReloading = false;

    public void Init()
    {
        currentAmmoSize = maxAmmoSize;
    }

    public virtual void Fire(LayerMask enemyLayers)
    {
        if (isReloading) return;

        if(currentAmmoSize <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / baseFireRate;

            if (projectilePattern == ProjectilePattern.Parallel)
            {
                FireParallel(enemyLayers);
            }
            else if (projectilePattern == ProjectilePattern.Spread)
            {
                FireSpread(enemyLayers);
            }

            currentAmmoSize--;

            if(currentAmmoSize <= 0)
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
            bulletScript.Initialize(baseDamage, enemyLayers, projectileSpeed);
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
            bulletScript.Initialize(baseDamage, enemyLayers, projectileSpeed);
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
