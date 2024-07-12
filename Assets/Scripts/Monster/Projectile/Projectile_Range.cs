using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile_Range : Projectile_Monster
{
    public Vector3 startPosition;
    public Vector3 targetPosition;

    public float maxHeight = 8f; // �ִ� ����
    public float gravity = 9.81f;
    public float timeToTarget;

    public void Init(Vector3 startPos, Vector3 targetPos)
    {
        startPosition = transform.position;
        targetPosition = target;

        // ��ǥ ��ġ������ �Ÿ�
        float distance = Vector3.Distance(startPosition, targetPosition);

        // �߻� ���� ���
        float theta = Mathf.Atan((maxHeight * 2) / distance);

        // �ʱ� �ӵ� ���
        launchSpeed = Mathf.Sqrt((gravity * distance * distance) / (2 * maxHeight * Mathf.Cos(theta) * Mathf.Cos(theta)));

        // ��ǥ ��ġ������ �ð� ���
        timeToTarget = distance / (launchSpeed * Mathf.Cos(theta));

        StartCoroutine(MoveInParabola());
    }

    IEnumerator MoveInParabola()
    {
        float elapsedTime = 0;
        while (elapsedTime < timeToTarget)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / timeToTarget;

            float currentHeight = maxHeight * 4 * t * (1 - t); // �������� ���� ���

            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, t);
            currentPosition.y += currentHeight;

            transform.position = currentPosition;

            yield return null;
        }

        transform.position = targetPosition; // ���� ��ġ ����
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster")) return;

        var IAttackable = collision.gameObject.GetComponent<IAttackable>();
        if(IAttackable != null )
        {
            IAttackable.OnTakeDamaged(damage);
        }

        Destroy(gameObject);
    }
}
