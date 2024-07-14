using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Range : Projectile_Monster
{
    public Vector3 startPosition;
    public Vector3 targetPosition;

    public float maxHeight = 8f; // 최대 높이
    public float gravity = 9.81f;
    public float timeToTarget;
    public float maxDistance = 15f;

    public void Init(Vector3 startPos, Vector3 targetPos, float maxDistance, float maxHeight)
    {
        startPosition = startPos;
        this.maxDistance = maxDistance;
        this.maxHeight = maxHeight;
        // 목표 위치까지의 거리
        float distance = Vector3.Distance(startPosition, targetPos);

        // 목표 위치가 최대 거리를 초과하면 조정
        if (distance > maxDistance)
        {
            Vector3 direction = (targetPos - startPosition).normalized;
            targetPosition = startPosition + direction * maxDistance;
        }
        else
        {
            targetPosition = targetPos;
        }

        targetPosition.y = 0; // 목표 위치의 y축 고정

        // 발사 각도 계산
        float theta = Mathf.Atan((maxHeight * 2) / distance);

        // 초기 속도 계산
        launchSpeed = Mathf.Sqrt((gravity * distance * distance) / (2 * maxHeight * Mathf.Cos(theta) * Mathf.Cos(theta)));

        // 목표 위치까지의 시간 계산
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

            float currentHeight = maxHeight * 4 * t * (1 - t); // 포물선의 높이 계산

            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, t);
            currentPosition.y += currentHeight;

            transform.position = currentPosition;

            yield return null;
        }

        transform.position = targetPosition; // 최종 위치 설정
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster")) return;

        var IAttackable = collision.gameObject.GetComponent<IAttackable>();
        if (IAttackable != null)
        {
            IAttackable.OnTakeDamaged(damage);
        }

        Destroy(gameObject);
    }
}