using UnityEngine;

public class Projectile_LoveShot : Projectile_Monster
{
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public Vector3 direction;

    public float maxDistance = 15f;
    private float distanceTravelled = 0f;

    public void Init(Vector3 startPos, Vector3 direction, float maxDistance, float speed)
    {
        startPosition = startPos;
        this.direction = direction.normalized;
        this.maxDistance = maxDistance;
        this.launchSpeed = speed;
        transform.position = startPosition;
    }

    private void Update()
    {
        float step = launchSpeed * Time.deltaTime;
        transform.position += direction * step;
        distanceTravelled += step;

        if (distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster")) return;

        var IAttackable = collision.gameObject.GetComponent<IAttackable>();
        if (IAttackable != null)
        {
            IAttackable.OnTakeDamaged(damage);
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.LogAssertion("Player IAttack is null");
            }
        }

        //Debug.Log($"Collsion : {collision.gameObject.name}");
        Destroy(gameObject);
    }
}