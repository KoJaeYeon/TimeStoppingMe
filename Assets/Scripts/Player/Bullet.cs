using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;
    private int damage;
    private LayerMask enemyLayers;
    private float remainingLifetime;

    public void Initialize(int damageAmount, LayerMask layers, float projectileSpeed)
    {
        damage = damageAmount;
        enemyLayers = layers;
        speed = projectileSpeed;
        remainingLifetime = lifetime;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
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
            IAttackable attackable = other.GetComponent<IAttackable>();
            if (attackable != null)
            {
                attackable.OnTakeDamaged(damage);
                Debug.Log("Bullet hit " + other.name);
                Destroy(gameObject);
            }
        }
    }
}
