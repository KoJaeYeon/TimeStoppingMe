using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;
    private int damage;
    private LayerMask enemyLayers;
    private float remainingLifetime;
    public DebuffType debuffType;
    public Debuff debuff;
    public int piercingCount;
    public float knockbackForce;
    private HashSet<GameObject> hasPierced;

    private bool isKnockbackActive = false;
    private bool isPiercingActive = false;

    public void Initialize(int damageAmount, LayerMask layers, float projectileSpeed, int piercingCount, float knockbackForce)
    {
        damage = damageAmount;
        enemyLayers = layers;
        speed = projectileSpeed;
        remainingLifetime = lifetime;
        this.piercingCount = piercingCount;
        this.knockbackForce = knockbackForce;
        hasPierced = new HashSet<GameObject>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void ActivateKnockbackSkill(float force)
    {
        isKnockbackActive = true;
        knockbackForce += force;
    }

    public void ActivatePiercingSkill(int additionalPiercing)
    {
        isPiercingActive = true;
        piercingCount += additionalPiercing;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        remainingLifetime -= Time.deltaTime;

        if (remainingLifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayers) != 0)
        {
            if(hasPierced.Contains(other.gameObject))
            {
                return;
            }

            IAttackable attackable = other.GetComponent<IAttackable>();
            if (attackable != null)
            {
                attackable.OnTakeDamaged(damage);
                Debug.Log("Bullet hit " + other.name);

                if(debuff != null)
                {
                    attackable.OnTakeDebuffed(debuffType, debuff);
                }

                if (isKnockbackActive)
                {
                    Vector3 knockbackDirection = other.transform.position - transform.position;
                    knockbackDirection.y = 0; // Keep knockback horizontal
                    other.transform.position += knockbackDirection.normalized * knockbackForce;
                }

                hasPierced.Add(other.gameObject);

                if(!isPiercingActive || --piercingCount <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
